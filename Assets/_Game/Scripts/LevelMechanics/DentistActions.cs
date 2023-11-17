using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DentistActions : MonoBehaviour
{
    [Header("Display Settings")]
    //[SerializeField] private ActionCountTimer actionTimer = null;
    [SerializeField] private TextDisplay textDisplay;

    [Header("Dentist Settings")]
    //[SerializeField] private GameObject xrayObject;
    [SerializeField] private GameObject[] dentistObjects = {};
    [SerializeField] private string[] speech = {};
    private Vector3[] startPos;
    private bool xrayEnabled = false;

    private void Start() 
    {
        //save start positions of all dentist office objects
        startPos = new Vector3[dentistObjects.Length];
        for (int i=0; i<dentistObjects.Length; i++)
        {
            startPos[i] = dentistObjects[i].transform.position;
        }
    }
    public void ToggleXRay()
    {
        //gameobj.setactive
        if (xrayEnabled == false) 
        { 
            //xrayObject.SetActive(true); 
            dentistObjects[0].SetActive(true);
            AudioManager.Instance.PlaySFX("XRay On");
            xrayEnabled = true;
        }
        else if (xrayEnabled == true)
        { 
            //xrayObject.SetActive(false);
            dentistObjects[0].SetActive(false);
            AudioManager.Instance.PlaySFX("XRay Off");
            xrayEnabled = false;
        }
    }

    public void Button2() //rename, temp function to test
    {
        if (xrayEnabled == true)
        {
            //xrayObject.SetActive(false);
            dentistObjects[0].SetActive(false);
            AudioManager.Instance.PlaySFX("XRay Off");
            xrayEnabled = false;
        }

        CallSpeech(0);
        DataManager.Instance.UpHealth();
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
