using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DentistActions : MonoBehaviour
{
    [SerializeField] public Button action1, action2, action3, action4;

    public void XRayEnable() 
    {
        if (action1.enabled)
        {
            AudioManager.Instance.PlaySFX("XRay On");
        }
    }
}
