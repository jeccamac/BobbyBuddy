using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GardenButtons : MonoBehaviour
{
    [Header("Garden Actions Settings")]
    [SerializeField] public GameObject[] garden = {}; //green, harvest, nightshade

    [Header("Toggle UI Settings")]
    [SerializeField] public PatioActions patioActions = null;
    [SerializeField] public Animator gardenUI = null;
    [SerializeField] public GameObject togglePanel = null;
    [SerializeField] public GameObject[] uiToToggle = {};
    private bool isPanelActive;

    private void Start() 
    {
        gardenUI.enabled = false;
        isPanelActive = false;
    }

    private void Update()
    {
        if (patioActions.moveLeft == 1) 
        {
            gardenUI.enabled = true;
            togglePanel.SetActive(true);
        } 
        else 
        { 
            isPanelActive = false;
            togglePanel.SetActive(false); 
        }
    }
    public void TogglePanel()
    {
        if (!isPanelActive)
        { 
            gardenUI.enabled = true;
            gardenUI.Play("SlideIn");
            isPanelActive = true;
            Debug.Log("panel is active ");
        } else 
        { 
            gardenUI.Play("SlideOut"); 
            isPanelActive = false;
            Debug.Log("panel is not active");
        }
    }
    public void GardenGreen()
    {
        garden[0].SetActive(true);
        garden[1].SetActive(false);
        garden[2].SetActive(false);
    }

    public void GardenHarvest()
    {
        garden[0].SetActive(false);
        garden[1].SetActive(true);
        garden[2].SetActive(false);
    }

    public void GardenNightshade()
    {
        garden[0].SetActive(false);
        garden[1].SetActive(false);
        garden[2].SetActive(true);
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
