using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCube : MonoBehaviour{

    public float rotationSpeed;
    public Vector3 targetRotation;

    // Start is called before the first frame update
    void Start(){

    }

    // Update is called once per frame
    void Update(){
        transform.Rotate(targetRotation * (rotationSpeed * Time.deltaTime));
    }
}
