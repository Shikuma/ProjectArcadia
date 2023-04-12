using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour{

    [SerializeField]
    GameObject[] titlePages;

    int currPage = 0;
    int nextPage = 0;

    Vector2 centerPagePos;

    public float swipeSpeed = 1;


    // Start is called before the first frame update
    void Start(){
        centerPagePos = new Vector2(Screen.width/2, Screen.height/2);
        Debug.Log(titlePages[0].GetComponent<RectTransform>().position);
    }

    // Update is called once per frame
    void Update(){
        
        
        if(currPage != nextPage) {

            titlePages[currPage].GetComponent<RectTransform>().position = new Vector2(
                titlePages[currPage].GetComponent<RectTransform>().position.x - swipeSpeed, 
                centerPagePos.y);

            titlePages[nextPage].GetComponent<RectTransform>().position = new Vector2(
                titlePages[nextPage].GetComponent<RectTransform>().position.x - swipeSpeed, 
                centerPagePos.y);

            if(titlePages[currPage].GetComponent<RectTransform>().position.x <= centerPagePos.x - Screen.width &&
                titlePages[currPage].GetComponent<RectTransform>().position.x <= centerPagePos.x) {

                titlePages[nextPage].GetComponent<RectTransform>().position = new Vector2(centerPagePos.x, centerPagePos.y);
                currPage = nextPage;
            }

            //SwipePageLeft(titlePages[currPage]);
            //SwipePageLeft(titlePages[nextPage]);

            //currPage = nextPage;
        }
    }

    public void FlipPage(int index) {
        //Prep next page before animating
        titlePages[index].GetComponent<RectTransform>().position = new Vector2(centerPagePos.x + Screen.width, centerPagePos.y);
        
        nextPage = index;
        
        
        //currPage = index;

    }

    public void SwipePageLeft(GameObject parentPanel) {

    }
}
