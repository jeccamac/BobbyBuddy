using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenButtons : MonoBehaviour
{
    [Header("Garden Settings")]
    [SerializeField] public GameObject[] garden = {}; //green, harvest, nightshade

    [Header("Toggle UI Settings")]
    [SerializeField] public GameObject[] uiToToggle = {};

    // Update is called once per frame
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
