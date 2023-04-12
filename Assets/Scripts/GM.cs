using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour { 

    string currScene = "MainMenu";
    public GameObject loadingCanvas, loadingSpinner, titleCanvas;
    public int loadSpeed = 5;
    public Sprite[] numbersSheet;

    [SerializeField]
    Material[] skyboxes;

    UIController uic;

    // Start is called before the first frame update
    void Start(){
        uic = GetComponent<UIController>();
        LoadResources();
        RandomizeSkyBox();
    }

    // Update is called once per frame
    void Update(){

    }


    /*TODO:
     * More robust async actions
     */
    public void LoadSceneAdd(string sceneName) {
        StartCoroutine(StartLoadScene(sceneName, true));
    }

    public void LoadSceneSingle(string sceneName) {
        StartCoroutine(StartLoadScene(sceneName, false));
    }

    IEnumerator StartLoadScene(string sceneName, bool add) {
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName, add ? LoadSceneMode.Additive : LoadSceneMode.Single);
        BeginLoading();

        yield return ao;
        currScene = sceneName;
        EndLoading();
        titleCanvas.SetActive(false);
    }

    public void UnloadCurrScene() {
        StartCoroutine(StartUnloadCurrScene());
    }

    IEnumerator StartUnloadCurrScene() {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(currScene);
        BeginLoading();
        
        yield return ao;

        EndLoading();
        currScene = "MainMenu";
        titleCanvas.SetActive(true);
    }

    void BeginLoading() {
        loadingCanvas.gameObject.SetActive(true);
        Cursor.visible = false;
    }

    void EndLoading() {
        loadingCanvas.GetComponent<LoadingCanvas>().shouldUnload = true;
        Cursor.visible = true;
    }

    public void CheckEventSystems() {
        //Removes any duplicate event systems or light sources
        if (GameObject.FindGameObjectWithTag("MainCamera")) {
            GameObject.FindGameObjectWithTag("TempCamera").SetActive(false);
        }
        if (GameObject.FindGameObjectWithTag("MainEvent")) {
            GameObject.FindGameObjectWithTag("TempEvent").SetActive(false);
        }
        if (GameObject.FindGameObjectWithTag("MainLight")) {
            GameObject.FindGameObjectWithTag("TempLight").SetActive(false);
        }
    }

    private void LoadResources() {
        //StartCoroutine(StartLoadResources());
        numbersSheet = Resources.LoadAll<Sprite>("Numbers");
    }

    private IEnumerator StartLoadResources() {
        AsyncOperation ao = Resources.LoadAsync<Sprite>("Numbers");
        yield return ao;
    }

    public void RandomizeSkyBox() {
        RenderSettings.skybox = skyboxes[(int)Random.Range(0f, skyboxes.Length - 1)];
        DynamicGI.UpdateEnvironment();
    }

    public void QuitGame() {
        Application.Quit();
    }

}
