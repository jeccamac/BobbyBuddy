using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandbox_Eat : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private string animEvent;

    private void Start() 
    {
        animator = GetComponent<Animator>();
    }

    public void Eat()
    {
        animator.Play(animEvent);
        Debug.Log("eating");
    }
}
