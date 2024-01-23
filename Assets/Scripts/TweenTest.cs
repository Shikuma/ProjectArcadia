using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenTest : MonoBehaviour{

    public Vector3 dest;

    // Start is called before the first frame update
    void Start(){
        //transform.DOMove(dest, 2).SetEase(Ease.OutQuint).SetLoops(4).OnComplete(OnTweenComplete);

        //defaultCube.transform.DOMove


    }

    // Update is called once per frame
    void Update(){
        
    }

    void OnTweenComplete() {
        Debug.Log("Finished tween");
    }
}
