using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIController : MonoBehaviour{

    [SerializeField]
    GameObject titlePagesParent;
    GameObject[] titlePages;
    RectTransform titleParentRect;

    [SerializeField]
    GameObject modesContentParent;


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
    }

    // Update is called once per frame
    void Update(){

    }

    #region game modes inits
        public void SelectGameMode(string modeName) {
            GamePrefs.GameModeName = modeName;
        //Debug.Log("mode name: " + modeName);
        //Debug.Log("Label: " + modeLabel);
        foreach(Component comp in modeLabel.GetComponents(typeof(Component))) {
            Debug.Log("Component: " + comp);
        }
        //Debug.Log("component: " + modeLabel.GetComponents(typeof(Component)));
        //Debug.Log("text: " + modeLabel.GetComponent<TextMeshPro>().text);
        //foreach(Component comp in modeLabel.GetComponents)
            modeLabel.GetComponent<TextMeshProUGUI>().text = modeName;
            foreach (Transform child in modesContentParent.transform) {
                if (child.name == modeName && !child.gameObject.activeSelf) child.gameObject.SetActive(true);
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
            Debug.Log("Setting board size: " + inc);
    }

        public void SudokuPrefs_DifficultySelect(int inc) {
            int min = 0;
            int max = System.Enum.GetNames(typeof(GamePrefs.SudokuPrefs.Difficulties)).Length;
            int val = (int)Mathf.Clamp((float)(GamePrefs.SudokuPrefs.difficulty += inc),
                    min, max);

            GamePrefs.SudokuPrefs.difficulty = (GamePrefs.SudokuPrefs.Difficulties)val;
            sudoku_Difficulty.GetComponent<TextMeshPro>().text = GamePrefs.SudokuPrefs.difficulty.ToString();

            Debug.Log("Setting difficulty: " + inc);
        }

        //Game2
    #endregion

    

}
