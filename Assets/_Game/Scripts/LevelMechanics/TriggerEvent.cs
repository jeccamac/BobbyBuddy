using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TriggerEvent : MonoBehaviour
{
    [Tooltip("Object needed to trigger")]
    [SerializeField] public GameObject objTrigger;
    [SerializeField] public UnityEvent invokeMethod;

    //call an event or function when trigger collision happens
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == objTrigger)
        {
            invokeMethod.Invoke();
            Destroy(other.gameObject, 1f);
        }
    }

    //call an event from anywhere
    public void TriggerInvoke()
    {
        invokeMethod.Invoke();
    }

}
