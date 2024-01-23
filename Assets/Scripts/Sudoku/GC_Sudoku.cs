using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GC_Sudoku : MonoBehaviour {

    public GameObject parentCanvas, panelPreGame, panelMainGame, boardParent, sizeDigit0, sizeDigit1, difficultyDigit0;
    public GameObject[,] tiles;
    public GameObject selectedTile;
    public Vector2 selectedTileIndex;
    public GameObject GMO;
    public int boardSize = 9, minBoardSize = 3, maxBoardSize = 15;
    public int difficulty = 1, minDifficulty = 1, maxDifficulty = 3;
    public struct Difficulty{


    }
    public int[,] boardComplete;
    public Camera cam;
    bool boardSizeLimit, difficultyLimit;

    public Button testButton;
    public Image tileImg;
    public Color selectColor;
    private Sprite[] numbersSheet;
    Image sizeDigit0Img, sizeDigit1Img, difficultyImg;
    float flashCounter;

    Dictionary<Vector2, int> playerSubmissions;


    // Start is called before the first frame update
    void Start(){
        GMO = GameObject.FindGameObjectWithTag("GameManager");
        sizeDigit0Img = sizeDigit0.GetComponent<Image>();
        sizeDigit1Img = sizeDigit1.GetComponent<Image>();
        difficultyImg = difficultyDigit0.GetComponent<Image>();

        playerSubmissions = new Dictionary<Vector2, int>();

        if (GMO) {
            GM gmScript = GMO.GetComponent<GM>();
            gmScript.CheckEventSystems();
            SetResources(gmScript);
            
        } else {
            LoadResourcesIndependently();
        }
    }

    // Update is called once per frame
    void Update(){

    }

    /*
     * BOARD HANDLERS
     */

    int[,] GenerateRandomBoard(int[,] newBoard) {
        for (int i = 0; i < newBoard.GetLength(0); i++){
            
            int[] tempArr = Enumerable.Range(1, boardSize).ToArray();
            
            for(int j = 0; j < newBoard.GetLength(1); j++) {
                //Set board random value
                int rnd = Random.Range(j, tempArr.Length);
                newBoard[i, j] = tempArr[rnd];

                //Swap random element with j so it doesn't get reused
                int tempHolder = tempArr[j];
                tempArr[j] = tempArr[rnd];
                tempArr[rnd] = tempHolder;

                //Debug.Log(newBoard[i, j]);
            }
        }

        return newBoard;
    }

    int[,] SortBoard(int[,] newBoard) {
        for (int i = 1; i < newBoard.GetLength(0); i++) {
            for(int j = 0; j < newBoard.GetLength(1); j++) {

                bool match = false;
                int ci = 0;
                int sj = j+1;

                for(; ; ) {
                    if (sj >= newBoard.GetLength(1)) sj = 0;
                    if (ci >= i ) {
                        //check if swap will work
                        if (match) {
                            int tmp = newBoard[i, j];
                            newBoard[i, j] = newBoard[i, sj];
                            newBoard[i, sj] = tmp;
                        }
                        
                        break;
                    }else if(newBoard[i,j] == newBoard[ci, j] && !match) {
                        match = true;
                        ci = 0;
                        continue;
                    }else if (newBoard[ci,j] == newBoard[i,sj]) {
                        ci = 0;
                        sj++; 
                        continue;
                    }
                    ci++;
                }
            }
        }

        return newBoard;
    }

    public int[] GetRow(int[,] matrix, int rowNumber) {
        return Enumerable.Range(0, matrix.GetLength(1))
                .Select(x => matrix[rowNumber, x])
                .ToArray();
    }

    void RenderBoard() {
        //Prepare board
        tiles = new GameObject[boardSize, boardSize];
        GridLayoutGroup grid = boardParent.GetComponent<GridLayoutGroup>();

        //Set grid constraints
        //grid.spacing = new Vector2(10f, 10f);
        //grid.constraintCount = boardSize;
        float width = boardParent.gameObject.GetComponent<RectTransform>().rect.width;
        float height = boardParent.gameObject.GetComponent<RectTransform>().rect.height;
        Vector2 newSize = new Vector2((width / boardSize)-10f, (width / boardSize)-20f);
        grid.cellSize = newSize;


        //Generate tiles with dynamic names to make callbacks later
        for (int i = 0; i < tiles.GetLength(0); i++) {
            for(int j = 0; j < tiles.GetLength(1); j++) {
                
                GameObject tmp = new GameObject("Tile" + i + j);//GameObject.Instantiate(new GameObject, parent);
                tmp.transform.parent = boardParent.gameObject.transform;
                tiles[i, j] = tmp;
                
                Button thisBtn = tmp.AddComponent<Button>();
                Vector2 tileIndex = new Vector2(i, j);
                thisBtn.onClick.AddListener(() => SelectTile(tileIndex));

                ColorBlock cb = thisBtn.colors;
                //cb.selectedColor = selectColor;
                //thisBtn.colors = cb;

                //Set image for highlighted tile, disable until highlighted
                Image tileImg = tmp.AddComponent<Image>();
                tileImg.sprite = numbersSheet[boardComplete[i, j]];
                tileImg.preserveAspect = true;
                
                
                //Calculate % rendered based on difficulty
                float difficultyRange = 33.33f;
                if(difficulty == 2) {
                    difficultyRange = 50f;
                }else if(difficulty == 3) {
                    difficultyRange = 66.66f;
                }

                //Hide tiles based on difficulty
                //Set alpha back to 1 with input later
                if (Random.Range(0f, 100f) < difficultyRange) {
                    tileImg.color = new Color(1, 1, 1, 0);
                    
                }
            }
        }
    }


    //Only called if the GM can't be used to access resources
    private void LoadResourcesIndependently() {
        numbersSheet = Resources.LoadAll<Sprite>("Numbers");
    }

    private void SetResources(GM gmScript) {
        gmScript.numbersSheet.CopyTo(numbersSheet = new Sprite[gmScript.numbersSheet.Length], 0);
    }

    public void SelectTile(Vector2 tile) {
        Debug.Log("Selected tile: " + tile);
        selectedTile = tiles[(int)tile.x, (int)tile.y];
        selectedTileIndex = tile;
    }


    /* TODO:
     * set input onclick values
     */
    public void UpdateTile(int value) {
        Image tileImg = selectedTile.GetComponent<Image>();
        if (value > 0) {
            tileImg.color = new Color(1, 1, 1, 1);
            tileImg.sprite = numbersSheet[value];
        } else {
            tileImg.color = new Color(1, 1, 1, 0);
        }

        if (!playerSubmissions.TryAdd(selectedTileIndex, value)) {
            playerSubmissions[selectedTileIndex] = value;
        }

        //If difficulty is easy
        //set tile color, red or green (right or wrong)

                //Game complete?
        if (CheckGameStatus()) {
            //if win
            //if one or more is wrong
            Debug.Log("YOU WON");
        } else {
            Debug.Log("Uh oh, something went wrong...");
        }
    }

    bool CheckGameStatus() {
        bool won = true;

        foreach(KeyValuePair<Vector2, int> answer in playerSubmissions) {
            if (answer.Value != boardComplete[(int)answer.Key.x, (int)answer.Key.y] || answer.Value == 0) {
                won = false;
                break;
            }
        }

        return won;
    }



}
