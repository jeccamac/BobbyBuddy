using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeLookAtManager : MonoBehaviour
{
    public GameObject objEyeL;
    public GameObject objEyeR;

    private Renderer renderEyeL, renderEyeR;
    public Transform objPivotEyeL;
    public Transform objPivotEyeR;
    public Transform objLookAtL;
    public Transform objLookAtR;

    private void Start() 
    {
        renderEyeL = objEyeL.GetComponent<Renderer>();
        renderEyeR = objEyeR.GetComponent<Renderer>();
    }
    private void Update() 
    {
        // objPivotEyeL.LookAt(objLookAtL);
        // objPivotEyeR.LookAt(objLookAtR);

        //Debug.Log("PIVOT " + objPivotEyeL.position.x + " LOOK AT " + objLookAtL.position.x);

        // //get local eye rotation
        // var eyeLPivotX = objPivotEyeL.localRotation.x;
        // var eyeLPivotY = objPivotEyeL.localRotation.y * 3;
        // Vector2 tempEyeLRot = new Vector2(eyeLPivotY, eyeLPivotX);

        // var eyeRPivotX = objPivotEyeR.localRotation.x;
        // var eyeRPivotY = objPivotEyeR.localRotation.y * -2;
        // Vector2 tempEyeRRot = new Vector2(eyeRPivotY, eyeRPivotX);

        //update texture offset
        // renderEyeL.material.SetTextureOffset("_MainTex", tempEyeRot);
        // renderEyeR.material.SetTextureOffset("_MainTex", tempEyeRot);
        // renderEyeL.material.mainTextureOffset = tempEyeLRot;
        // renderEyeR.material.mainTextureOffset = tempEyeRRot;

        var eyeLPivotX = objLookAtL.position.x / 4;
        var eyeLPivotY = objLookAtL.position.y / -2;
        Vector2 eyeLLook = new Vector2(eyeLPivotX, eyeLPivotY);

        var eyeRPivotX = objLookAtR.position.x / -4;
        var eyeRPivotY = objLookAtR.position.y / -2;
        Vector2 eyeRLook = new Vector2(eyeRPivotX, eyeRPivotY);

        renderEyeL.material.mainTextureOffset = eyeLLook;
        renderEyeR.material.mainTextureOffset = eyeRLook;
    }


}
