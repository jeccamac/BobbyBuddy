using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DentistActions : MonoBehaviour
{
    [SerializeField] private GameObject xrayObject;
    [SerializeField] private TextDisplay textDisplay;
    [SerializeField] private string[] speech = {};
    private bool xrayEnabled = false;
    public void ToggleXRay()
    {
        //gameobj.setactive
        if (xrayEnabled == false) 
        { 
            xrayObject.SetActive(true); 
            AudioManager.Instance.PlaySFX("XRay On");
            xrayEnabled = true;
        }
        else if (xrayEnabled == true)
        { 
            xrayObject.SetActive(false);
            AudioManager.Instance.PlaySFX("XRay Off");
            xrayEnabled = false;
        }
    }

    public void Button2() //rename, temp function to test
    {
        if (xrayEnabled == true)
        {
            xrayObject.SetActive(false);
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
}
