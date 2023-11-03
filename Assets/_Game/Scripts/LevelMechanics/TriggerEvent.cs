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
    [SerializeField] private LayerMask foodType;

    [Header("Event to trigger")]
    [SerializeField] public UnityEvent invokeMethod;

    //call an event or function when trigger collision happens
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == objTrigger)
        {
            invokeMethod.Invoke();
            Destroy(other.gameObject, 1f);
        }

        if (other.gameObject.layer == foodType)
        {
            invokeMethod.Invoke();
            Destroy(other.gameObject, 1f);
        }
    }

    //call an event from anywhere
    public void TriggerInvoke()
    {
        invokeMethod.Invoke(); //invoke event that is defined in objTrigger
    }

}
