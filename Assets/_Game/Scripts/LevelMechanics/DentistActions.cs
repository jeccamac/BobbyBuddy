using System.Collections;
using System.Collections.Generic;
//using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class DentistActions : MonoBehaviour
{
    [Header("Display Settings")]
    [SerializeField] private ActionCountTimer actionTimer = null;
    [SerializeField] private TextDisplay textDisplay;

    [Header("Dentist Settings")]
    //[SerializeField] private GameObject xrayObject;
    [SerializeField] private GameObject[] dentistObjects = {};
    [SerializeField] private GameObject _cleaningAction = null;
    [SerializeField] private string[] speech = {};
    private Vector3[] startPos;
    private bool xrayEnabled = false;
    private bool hasCleaned = false;

    private void Start() 
    {
        //save start positions of all dentist office objects
        startPos = new Vector3[dentistObjects.Length];
        for (int i=0; i<dentistObjects.Length; i++)
        {
            startPos[i] = dentistObjects[i].transform.position;
        }

        _cleaningAction.SetActive(false);
    }

    private void Update() 
    {
        HasCleaned();
        UpdateDental();
        ClampXray();
    }

    public void ToggleXRay()
    {
        _cleaningAction.SetActive(false);

        //gameobj.setactive
        if (xrayEnabled == false) 
        { 
            //xrayObject.SetActive(true); 
            dentistObjects[0].SetActive(true);
            AudioManager.Instance.PlaySFX("XRay On");
            xrayEnabled = true;
            CallSpeech(0);
        }
        else if (xrayEnabled == true)
        { 
            //xrayObject.SetActive(false);
            dentistObjects[0].SetActive(false);
            AudioManager.Instance.PlaySFX("XRay Off");
            xrayEnabled = false;
        }
    }
    private void ClampXray()
    {
        //get transform position
        float xpos = dentistObjects[0].transform.position.x;
        var ypos = dentistObjects[0].transform.position.y;
        var zpos = dentistObjects[0].transform.position.z;

        //clamp xray position
        xpos = Mathf.Clamp(xpos, -0.3f, 0.3f);
        ypos = Mathf.Clamp(ypos, 1.72f, 2.72f);
        zpos = Mathf.Clamp(zpos, -0.8f, -0.8f);

        //apply clamp
        var clampPos = new Vector3(xpos, ypos, zpos);
        dentistObjects[0].transform.position = clampPos;
    }

    public void SelectScaler()
    {
        if (xrayEnabled == true)
        {
            //xrayObject.SetActive(false);
            dentistObjects[0].SetActive(false);
            AudioManager.Instance.PlaySFX("XRay Off");
            xrayEnabled = false;
        }

        _cleaningAction.SetActive(true);

        CallSpeech(1);
    }

    public void CleanTeeth()
    {
        actionTimer.StartTimer(5);
    }

    public void CancelClean()
    {
        if (actionTimer.timeRemaining > 0 && !actionTimer.timerEnded)
        {
            actionTimer.CancelTimer();
            hasCleaned = false;
        }
    }

    private void HasCleaned()
    {
        if (actionTimer.timeRemaining <= 0 && actionTimer.timerEnded && actionTimer.timerComplete)
        {
            hasCleaned = true;
            actionTimer.timerComplete = false;
        }
    }

    private void UpdateDental()
    {
        if (hasCleaned)
        {
            DataManager.Instance.UpHealth();
            hasCleaned = false;
            Debug.Log("cleaning teeth increased dental health state by 1");
        }
    }

    public void CallSpeech(int speechLine)
    {
        string speak = speech[speechLine];
        textDisplay.ShowText(speak, 3f);
    }

    public void CallRandomSpeech()
    {
        string speak = speech[Random.Range(0, speech.Length)];
        textDisplay.ShowText(speak, 3f);
    }

    public void ObjectReset()
    {
        for (int i=0; i<dentistObjects.Length; i++)
        {
            dentistObjects[i].transform.position = startPos[i];
        }
    }
}
