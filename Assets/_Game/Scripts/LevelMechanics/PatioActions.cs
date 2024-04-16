using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PatioActions : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Transform[] camPos = {}; //camOrigin, camR, camL
    [SerializeField] public float panSpeed;
    private Camera camMain;
    public int moveRight, moveLeft;
    private Vector3 velocity = Vector3.zero;

    [Header("Bobby Settings")]
    [SerializeField] public GameObject bobby = null;
    [SerializeField] public Animator bobbyAnim = null;
    [SerializeField] public Transform[] bobbyPos = {}; //bobbyOrigin, bobbyR, bobbyRSit, bobbyL
    [SerializeField] public GameObject eyeLook = null;
    [SerializeField] public Transform[] eyeLookPos = {}; //origin, R, L, RSit
    
    private void Start()
    {
        camMain = Camera.main;
        bobbyAnim = GameObject.FindWithTag("Player").GetComponent<Animator>();

        camMain.transform.position = camPos[0].position;
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
            // Debug.Log("move left " + moveLeft.ToString());
            // Debug.Log("move right " + moveRight.ToString());
        }
        
    }

    public void MoveLeft()
    {
        if (moveLeft != 1)
        {
            moveLeft++;
            moveRight -= 1;
            // Debug.Log("move left " + moveLeft.ToString());
            // Debug.Log("move right " + moveRight.ToString());
        }
    }

    private void MoveCamera()
    {
        if (moveRight == 1)
        {
            camMain.transform.position = Vector3.SmoothDamp(camMain.transform.position, camPos[1].position, ref velocity, panSpeed * Time.deltaTime);
            bobby.transform.position = bobbyPos[3].position;
            bobby.transform.rotation = bobbyPos[3].rotation;
            bobbyAnim.Play("IdleSitting");
            eyeLook.transform.position = eyeLookPos[3].position;
        }

        if (moveRight == 0 && moveLeft == 0)
        {
            camMain.transform.position = Vector3.SmoothDamp(camMain.transform.position, camPos[0].position, ref velocity, panSpeed * Time.deltaTime);
            bobby.transform.position = bobbyPos[0].position;
            bobby.transform.rotation = bobbyPos[0].rotation;
            bobbyAnim.Play("Idle");
            eyeLook.transform.position = eyeLookPos[0].position;
        }

        if (moveLeft == 1)
        {
            camMain.transform.position = Vector3.SmoothDamp(camMain.transform.position, camPos[2].position, ref velocity, panSpeed * Time.deltaTime);
            bobby.transform.position = bobbyPos[2].position;
            bobby.transform.rotation = bobbyPos[2].rotation;
            bobbyAnim.Play("HappyIdle");
            eyeLook.transform.position = eyeLookPos[2].position;
        }
    }
}
