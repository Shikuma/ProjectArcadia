using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingCanvas : MonoBehaviour{

    public bool shouldUnload = false;
    public float fadeSpeed = .01f;
    GameObject childPanel;
    Color currAlpha;

    // Start is called before the first frame update
    void Start(){
        childPanel = transform.GetChild(0).gameObject;
        currAlpha = childPanel.GetComponent<Image>().color;

    }

    // Update is called once per frame
    void Update(){

        //Fade in
        if (gameObject.activeSelf) {
            if (currAlpha.a < 1f) {
                currAlpha.a += fadeSpeed;
            }else if (shouldUnload) {
                currAlpha.a -= fadeSpeed;
                if (currAlpha.a <= 0) {
                    shouldUnload = false;
                    gameObject.SetActive(false);
                }
            }

            childPanel.GetComponent<Image>().color = currAlpha;
        }
    }
}
