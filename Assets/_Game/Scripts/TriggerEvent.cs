using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField] public UnityEvent invokeMethod;

    //call an event or function when trigger collision happens
    private void OnCollisionEnter(Collision other) 
    {
        invokeMethod.Invoke();
    }
}
