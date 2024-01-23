using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicToonColor : MonoBehaviour{
    public Color[] colorOptions;

    Material thisMat;
    // Start is called before the first frame update
    void Start(){
        thisMat = GetComponent<Renderer>().material;
        thisMat.color = colorOptions[0];
        
    }

    // Update is called once per frame
    void Update(){
        NextColor();        
    }

    void NextColor() {
        thisMat.color = Color.Lerp(colorOptions[0], colorOptions[1], Mathf.PingPong(Time.time, 1));
    }
}
