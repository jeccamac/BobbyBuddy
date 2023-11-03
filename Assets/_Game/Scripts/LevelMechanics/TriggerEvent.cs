using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TriggerEvent : MonoBehaviour
{
    [Header("Object trigger as a GameObject")]
    [SerializeField] public GameObject objTrigger;

    [Header("OR Object trigger on a specific Layer")]
    [SerializeField] private LayerMask layerType;

    [Header("On Trigger Enter")]
    [SerializeField] public UnityEvent invokeMethod;

    [Header("On Trigger Exit")]
    [SerializeField] public UnityEvent invokeExit;

    //call an event or function when trigger collision happens
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == objTrigger)
        {
            invokeMethod.Invoke();
            Debug.Log("obj entered trigger");
            //Destroy(other.gameObject, 1f);
        }

        else if (other.gameObject.layer == layerType)
        {
            invokeMethod.Invoke();
            //Destroy(other.gameObject, 1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (invokeExit != null)
        {
            invokeExit.Invoke();
        }
    }

    //call an event from anywhere
    public void TriggerInvoke()
    {
        invokeMethod.Invoke(); //invoke event that is defined in objTrigger
    }

}
