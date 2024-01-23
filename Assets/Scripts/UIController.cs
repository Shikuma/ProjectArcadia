using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIController : MonoBehaviour{

    [SerializeField]
    GameObject titlePagesParent;
    GameObject[] titlePages;
    RectTransform titleParentRect;
    Vector2 pageTransStart, pageTransEnd;

    [SerializeField]
    GameObject modesContentParent;

    int currPage = 0, nextPage = 0;

    Vector2 centerPagePos;

    public float swipeSpeed = 1;
    float startTime, journeyLength;

    GM gm;

    //Game Pref UI elements (game_pref)
    [SerializeField]
    GameObject modeLabel;

    //Sudoku
    [SerializeField]
    GameObject sudoku_Difficulty, sudoku_BoardSize;

    //Game2


    void Start(){
        gm = GetComponent<GM>();
        centerPagePos = new Vector2(Screen.width/2, Screen.height/2);
        //Debug.Log(titlePages[0].GetComponent<RectTransform>().position);
        titlePages = new GameObject[titlePagesParent.transform.childCount];
        for(int i = 0; i < titlePages.Length; i++) {
            titlePages[i] = titlePagesParent.transform.GetChild(i).gameObject;
        }
        titleParentRect = titlePagesParent.GetComponent<RectTransform>();
        Debug.Log(titleParentRect.position); //540,960
    }

    // Update is called once per frame
    void Update(){
        if(currPage != nextPage) {
            Debug.Log("moving");
            float journey = (Time.time - startTime)*swipeSpeed;
            journey /= journeyLength;
            Vector2 interPos = Vector2.Lerp(pageTransStart, pageTransEnd, journey);

            Debug.Log("inter pos: " + interPos);
            

            titleParentRect.position = interPos;
            journey += Time.deltaTime;
            if (journey >= 1) currPage = nextPage;

        }
    }

    public void FlipPage(int index) {
        Debug.Log("Flipping page +" + index);
        nextPage += index;
        bool forward = nextPage > currPage;

        pageTransStart = titleParentRect.position;
        pageTransEnd = pageTransStart;
        pageTransEnd.x += (nextPage * Screen.width)*(forward?-1:1);
        startTime = Time.time;
        journeyLength = Vector2.Distance(pageTransStart, pageTransEnd);
        Debug.Log("distance: " + journeyLength);

        Debug.Log("trans start: " + pageTransStart);
        //   w/2,h/2
        Debug.Log("trans end: "+pageTransEnd);
        //   -540*n
        //540, -540, -1640
        //journey = -(pos-540*currPage)/1080
    }

    #region game modes inits
        public void SelectGameMode(string modeName) {
            GamePrefs.GameModeName = modeName;
            modeLabel.GetComponent<TextMeshPro>().text = modeName;
            foreach (Transform child in modesContentParent.transform) {
                if (child.name == modeName) child.gameObject.SetActive(true);
                else child.gameObject.SetActive(false);
            }

        }
    #endregion

    #region GAMEPREFS
        //Sudoku
        public void SudokuPrefs_BoardSizeSelect(int inc) {
            int min = 0;
            int max = System.Enum.GetNames(typeof(GamePrefs.SudokuPrefs.BoardSizes)).Length;
            int val = (int)Mathf.Clamp((float)(GamePrefs.SudokuPrefs.boardSize += inc),
                    min, max);

            GamePrefs.SudokuPrefs.boardSize = (GamePrefs.SudokuPrefs.BoardSizes)val;
            sudoku_BoardSize.GetComponent<TextMeshPro>().text = GamePrefs.SudokuPrefs.boardSize.ToString();
        }

        public void SudokuPrefs_DifficultySelect(int inc) {
            int min = 0;
            int max = System.Enum.GetNames(typeof(GamePrefs.SudokuPrefs.Difficulties)).Length;
            int val = (int)Mathf.Clamp((float)(GamePrefs.SudokuPrefs.difficulty += inc),
                    min, max);

            GamePrefs.SudokuPrefs.difficulty = (GamePrefs.SudokuPrefs.Difficulties)val;
            sudoku_Difficulty.GetComponent<TextMeshPro>().text = GamePrefs.SudokuPrefs.difficulty.ToString();
        }

        //Game2
    #endregion

    

}
