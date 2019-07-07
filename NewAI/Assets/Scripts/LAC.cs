using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LAC : MonoBehaviour
{  
    public bool invert;
    [Header("Freeze rotation")]
    public bool x;
    public bool y;
    public bool z;

    private float xRot, yRot, zRot;

    void Start (){
        xRot = transform.eulerAngles.x;
        yRot = transform.eulerAngles.y;
        zRot = transform.eulerAngles.z;
    }
    void Update () {
        if(invert){
            transform.LookAt(2 * transform.position - Camera.main.transform.position);
        } else{
            transform.LookAt(Camera.main.transform.position);
        }
        

        if(x){
            transform.eulerAngles = new Vector3(xRot,transform.eulerAngles.y,transform.eulerAngles.z);
        }
        if(y){
            transform.eulerAngles = new Vector3(transform.eulerAngles.x,yRot,transform.eulerAngles.z);
        }
        if(z){
            transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y,zRot);
        }
    }
}
