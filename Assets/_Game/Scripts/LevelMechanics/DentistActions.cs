using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DentistActions : MonoBehaviour
{
    [SerializeField] public Button action1, action2, action3, action4;
    [SerializeField] private GameObject xrayObject;
    private bool xrayEnabled = false;

    public void ToggleXRay()
    {
        //gameobj.setactive
        if (xrayEnabled == false) 
        { 
            xrayObject.SetActive(true); 
            AudioManager.Instance.PlaySFX("XRay On");
            xrayEnabled = true;
            Debug.Log("xray on");
        }
        else if (xrayEnabled == true)
        { 
            xrayObject.SetActive(false);
            AudioManager.Instance.PlaySFX("XRay Off");
            xrayEnabled = false;
            Debug.Log("xray off");
        }
    }

    public void Button2() //rename, temp function
    {
        if (xrayEnabled == true)
        {
            xrayObject.SetActive(false);
            AudioManager.Instance.PlaySFX("XRay Off");
            xrayEnabled = false;
        }
    }
}
