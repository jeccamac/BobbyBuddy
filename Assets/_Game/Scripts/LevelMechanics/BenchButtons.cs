using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenchButtons : MonoBehaviour
{
    [Header("Bench Actions Settings")]
    [SerializeField] public GameObject[] pets = {}; //gecko, sparrow, pudu

    [Header("Toggle UI Settings")]
    [SerializeField] public PatioActions patioActions = null;
    [SerializeField] public Animator benchUI = null;
    [SerializeField] public GameObject togglePanel = null;
    [SerializeField] public GameObject[] uiToToggle = {};
    private bool isPanelActive;

    private void Start() 
    {
        benchUI.enabled = false;
        isPanelActive = false;
    }

    private void Update()
    {
        if (patioActions.moveRight == 1) 
        {
            benchUI.enabled = true;
            togglePanel.SetActive(true);
        } 
        else 
        { 
            isPanelActive = false;
            benchUI.Play("IdleOut");
            togglePanel.SetActive(false); 
        }
    }
    public void TogglePanel()
    {
        if (!isPanelActive)
        { 
            benchUI.enabled = true;
            benchUI.Play("SlideIn");
            isPanelActive = true;
            Debug.Log("panel is active ");
        } else 
        { 
            benchUI.Play("SlideOut"); 
            isPanelActive = false;
            Debug.Log("panel is not active");
        }
    }
    public void PetGecko()
    {
        pets[0].SetActive(true);
        pets[1].SetActive(false);
        pets[2].SetActive(false);
    }

    public void PetSparrow()
    {
        pets[0].SetActive(false);
        pets[1].SetActive(true);
        pets[2].SetActive(false);
    }

    public void PetPudu()
    {
        pets[0].SetActive(false);
        pets[1].SetActive(false);
        pets[2].SetActive(true);
    }
    public void ToggleUI()
    {
        for(int i = 0; i < uiToToggle.Length; i++)
        {
            if(uiToToggle[i].activeSelf == true) { uiToToggle[i].SetActive(false);  } 
            else {uiToToggle[i].SetActive(true);}
            
        }
    }
}
