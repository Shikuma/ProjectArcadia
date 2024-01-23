using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TweenController : MonoBehaviour{

    public GameObject[] cubes;
    public Vector3 dest;
    int tweenCount = 0;
    Camera mainCam;

    // Start is called before the first frame update
    void Start(){
        Debug.Log(cubes.Length);
        if (cubes.Length >= 1)
            //Tween tween1 = new Tween();
            cubes[0].transform.DOMove(dest + cubes[0].transform.position, 2).SetEase(Ease.InQuint).SetLoops(1).OnComplete(OnTweenComplete);
        if (cubes.Length >= 2)
            cubes[1].transform.DOMove(dest + cubes[1].transform.position, 2).SetEase(Ease.InCirc).SetLoops(10).OnComplete(OnTweenComplete);
        if (cubes.Length >= 3)
            _ = cubes[2].transform.DOMove(dest + cubes[2].transform.position, 2).SetEase(Ease.InCubic).SetLoops(10).OnComplete(OnTweenComplete);
        if (cubes.Length >= 4)
            cubes[3].transform.DOMove(dest + cubes[3].transform.position, 2).SetEase(Ease.InElastic).SetLoops(1).OnComplete(OnTweenComplete);

        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("init shaking");
            mainCam.DOShakePosition(.5f, 2f, 50, 90f, true, ShakeRandomnessMode.Harmonic);
        }
    }

    void OnTweenComplete() {
        Debug.Log("completed tween#" + tweenCount);
        tweenCount++;
    }
}

