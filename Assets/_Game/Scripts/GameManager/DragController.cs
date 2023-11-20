using System.Collections;
using System.Collections.Generic;
using UnityEditor;

//using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragController : MonoBehaviour
{
    private Camera mainCam;
    private Transform objToDrag;
    private Touch touch;
    private Vector3 offset;
    private float initDist;    
    private bool isDragging = false;
    [SerializeField] private TriggerEvent triggerEvent;

    private void Awake() 
    {
        mainCam = Camera.main;
    }

    private void Update() 
    {
        DragObject();
    }

    private void DragObject()
    {
        if (Input.touchCount != 1) //detect touch
        {
            isDragging = false;
            return;
        }
        
        //get touch input
        touch = Input.touches[0]; //first touch
        Vector3 tpos = touch.position; //register touch position into a variable

        Vector3 v3; //local variable

        if (touch.phase == UnityEngine.TouchPhase.Began) //if began touching screen
        {
            Ray ray = mainCam.ScreenPointToRay(tpos);
            RaycastHit hit;

            //if touched any object, detected collision
            if (Physics.Raycast(ray, out hit))
            {
                //get touch position and object to drag position info
                if (hit.collider.tag == "Draggable")
                {
                    objToDrag = hit.transform;
                    initDist = hit.transform.position.z - mainCam.transform.position.z; //dont move on z axis, keep same distance from camera
                    v3 = new Vector3(tpos.x, tpos.y, initDist); //get new vector with touch position
                    v3 = mainCam.ScreenToWorldPoint(v3); //match to screen
                    offset = objToDrag.position - v3; //move on this offset
                    isDragging = true;

                    if (hit.collider.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
                    {
                        rigidbody.useGravity = false; //testing disable rigidbody
                    }
                }
            }

            //if touch tap hit trigger box, invoke event
            if (hit.collider.tag == "Trigger")
            {
                triggerEvent.TriggerInvoke();
            }
        } //END if began touching screen

        //conditions for dragging object
        if (isDragging && touch.phase == UnityEngine.TouchPhase.Moved)
        {
            v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, initDist);
            v3 = Camera.main.ScreenToWorldPoint(v3);
            objToDrag.position = v3 + offset; //move obj to new position
        } //END if conditions for dragging object

        if (isDragging && (touch.phase == UnityEngine.TouchPhase.Ended || touch.phase == UnityEngine.TouchPhase.Canceled))
        {
            isDragging = false;
        }//END if touch is cancelled
    }
}

