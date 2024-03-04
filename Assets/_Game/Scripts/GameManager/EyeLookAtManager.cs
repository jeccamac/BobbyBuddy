using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeLookAtManager : MonoBehaviour
{
    public GameObject objEyeL;
    public GameObject objEyeR;

    private Renderer renderEyeL, renderEyeR;
    // public Transform objPivotEyeL;
    // public Transform objPivotEyeR;
    public Transform objLookAtL;
    public Transform objLookAtR;

    [SerializeField] float offsetSpeedX;
    [SerializeField] float offsetSpeedY;

    private void Start() 
    {
        renderEyeL = objEyeL.GetComponent<Renderer>();
        renderEyeR = objEyeR.GetComponent<Renderer>();
    }
    private void Update() 
    {
        var eyeLPivotX = objLookAtL.position.x / offsetSpeedX;
        var eyeLPivotY = objLookAtL.position.y / -offsetSpeedY;
        Vector2 eyeLLook = new Vector2(eyeLPivotX, eyeLPivotY);

        var eyeRPivotX = objLookAtR.position.x / -offsetSpeedX;
        var eyeRPivotY = objLookAtR.position.y / -offsetSpeedY;
        Vector2 eyeRLook = new Vector2(eyeRPivotX, eyeRPivotY);

        renderEyeL.material.mainTextureOffset = eyeLLook;
        renderEyeR.material.mainTextureOffset = eyeRLook;
    }
}
