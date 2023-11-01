using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DentistActions : MonoBehaviour
{
    //[SerializeField] private Button[] _actionButtons; //CURRENTLY NOT IN USE
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
    }
}
