using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PatioActions : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Transform camOrigin, camR, camL;
    [SerializeField] public float panSpeed, panTime;
    private Camera camMain;
    public int moveRight, moveLeft;
    private Vector3 velocity = Vector3.zero;

    [Header("Bobby Settings")]
    [SerializeField] public Animator _bobbyAnim = null;
    [SerializeField] public Transform bobbyOrigin, bobbyR, bobbyRSit, bobbyL;
    
    private void Start()
    {
        camMain = Camera.main;
        _bobbyAnim = GameObject.FindWithTag("Player").GetComponent<Animator>();

        camMain.transform.position = camOrigin.position;
    }

    private void Update() 
    {
        MoveCamera();
    }
    public void MoveRight()
    {
        if (moveRight != 1) 
        { 
            moveRight++;
            moveLeft -= 1;
            Debug.Log("move left " + moveLeft.ToString());
            Debug.Log("move right " + moveRight.ToString());
        }
        
    }

    public void MoveLeft()
    {
        if (moveLeft != 1)
        {
            moveLeft++;
            moveRight -= 1;
            Debug.Log("move left " + moveLeft.ToString());
            Debug.Log("move right " + moveRight.ToString());
        }
    }

    private void MoveCamera()
    {
        if (moveRight == 1)
        {
            camMain.transform.position = Vector3.SmoothDamp(camMain.transform.position, camR.position, ref velocity, panSpeed * Time.deltaTime);
        }

        if (moveRight == 0 && moveLeft == 0)
        {
            camMain.transform.position = Vector3.SmoothDamp(camMain.transform.position, camOrigin.position, ref velocity, panSpeed * Time.deltaTime);
        }

        if (moveLeft == 1)
        {
            camMain.transform.position = Vector3.SmoothDamp(camMain.transform.position, camL.position, ref velocity, panSpeed * Time.deltaTime);
        }
    }
}
