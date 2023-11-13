using System.Collections;
using System.Collections.Generic;
//using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class ToothHealth : MonoBehaviour
{
    [Header("Tooth Health Display")]
    [SerializeField] private Image toothIcon;
    [SerializeField] private Image healthBar;
    private int _dentalState;
    private float _currentHealth;
    private float _maxHealth;
    
    [Header("Health Color Status")]
    [SerializeField] private Color healthy;
    [SerializeField] private Color yellow;
    [SerializeField] private Color critical;

    [Header("Tooth Icon Images")]
    [SerializeField] private Sprite healthy1, healthy2, yellow1, yellow2, rot1, rot2;

    private TextDisplay textDisplay;

    private void Start() 
    {
        textDisplay = FindObjectOfType<TextDisplay>();

        _dentalState = DataManager.Instance.dentalState;
        _currentHealth = DataManager.Instance.currentHealth;
        _maxHealth = DataManager.Instance.maxHealth;

        SwapIcons();
    }

    private void Update()
    {
        //get health info from data manager
        GetHealth();
        
        //update tooth health display
        SwapIcons();
    }

    public void GetHealth()
    {
        _dentalState = DataManager.Instance.dentalState;
        _currentHealth = DataManager.Instance.currentHealth;
        _maxHealth = DataManager.Instance.maxHealth;
        healthBar.fillAmount = _currentHealth / _maxHealth;
    }

    public void SwapIcons()
    {
            //icons
        if (_dentalState == 5)
        {
            toothIcon.sprite = healthy1;  
            healthBar.color = healthy;
        }
        else if (_dentalState == 4)
        {
            toothIcon.sprite = healthy2;
            healthBar.color = healthy;
        }
        else if (_dentalState == 3)
        {
            toothIcon.sprite = yellow1;
            healthBar.color = yellow;
        }
        else if (_dentalState == 2)
        {
            toothIcon.sprite = yellow2;
            healthBar.color = yellow;
        }
        else if (_dentalState == 1)
        {
            toothIcon.sprite = rot1;
            healthBar.color = critical;
        } 
        else if (_dentalState == 0)
        {
            toothIcon.sprite = rot2;
            healthBar.color = critical;
        }

        //     //bar color
        // if (_currentHealth >= 60)
        // {
        //     healthBar.color = healthy;
        // } 
        // else if (_currentHealth <= 60 && _currentHealth >= 20)
        // {
        //     healthBar.color = yellow;
        // } 
        // else if (_currentHealth <= 20)
        // {
        //     healthBar.color = critical;
        // }
    }

        //SHOW TEXT SOMEWHERE, IT'S CONSTANT IN THIS FUNCTION, NEEDS TO RUN ONCE
    //textDisplay.ShowText("I should take better care of my teeth", 3f);
    //textDisplay.ShowText("I might want visit the dentist", 3f);
    //textDisplay.ShowText("I should really get my teeth checked by the dentist", 3f);
}
