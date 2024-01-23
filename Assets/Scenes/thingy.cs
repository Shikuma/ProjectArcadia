using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thingy : MonoBehaviour
{
    public int n = 10;
    public GameObject pref;
    GameObject[,] nodes;

    // Start is called before the first frame update
    void Start(){
        nodes = new GameObject[n,n];
        for (int i = 0; i < nodes.Length; i++){
            

            Quaternion rot = transform.rotation;
            rot.x += 180*(i%2);
            
            int x = i / n;
            int y = i % n;
            int z = i / n * n;
            Vector3 pos = new Vector3(x,y,z);
            GameObject tmp = Instantiate(pref, pos, rot);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
