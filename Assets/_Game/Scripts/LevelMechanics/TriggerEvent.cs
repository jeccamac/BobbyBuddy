using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TriggerEvent : MonoBehaviour
{
    [Header("Object trigger as a GameObject")]
    [SerializeField] public GameObject objTrigger;

    [Header("OR Object trigger with a Tag")]
    [SerializeField] private string objTag;

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
        }

        else if (other.gameObject.tag == objTag)
        {
            invokeMethod.Invoke();
        }

        Food objFood = other.gameObject.GetComponent<Food>();
        if (objFood != null)
        {
            objFood.GetFoodType();
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
        Debug.Log("trigger with touch");
    }

}
