using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pager : MonoBehaviour{

    private void Update(){
        
    }

    public static void FlipPage(GameObject parent, bool direction) {

        RectTransform parentRect = parent.GetComponent<RectTransform>();
        float currX = parentRect.position.x;
        float currY = parentRect.position.y;
        int pageCount = parent.transform.childCount;
        float pagePosLimit = pageCount * Screen.width;

        //Move pages to the left if we can, otherwise move righ
        if (direction && currX <= 0f) {

        } else if(!direction && currX >= pagePosLimit) {

        }

    }

}
