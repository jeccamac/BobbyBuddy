using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance = null; // Singleton instance
    [Header("Scene Settings")]
    [SerializeField] private SceneLoader _sceneLoader = null;
    public static SceneLoader SceneLoader => Instance._sceneLoader;
    public string level {get; set;} // current level room

    [Header("Player Health Settings")]
    [SerializeField] public float currentHealth = 100;
    [SerializeField] public float maxHealth = 100;
    public int dentalState = 5;

    private void Awake() 
    {
        // Singleton pattern, should only be one of these instances on the DataManager prefab
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        if (_sceneLoader == null)
        {
            _sceneLoader = GetComponentInChildren<SceneLoader>();
        }
    }

    private void Update()
    {
        ChangeDentalState(); 
    }

    public Room GetRoom()
    {
        switch (level)
            {
                case "MainScreen":
                    return Room.MainScreen;
                case "LivingRoom":
                    return Room.LivingRoom;
                case "DiningArea":
                    return Room.DiningArea;
                case "BathroomVanity":
                    return Room.BathroomVanity;
                case "DentistOffice":
                    return Room.DentistOffice;
                default:
                    return Room.LivingRoom;
            }
    }

    public void ChangeDentalState()
    {
        //change dental states
        if (dentalState == 5 && currentHealth <= 50){ dentalState = 4; }
        else if (dentalState == 4 && currentHealth <= 0) 
        { 
            dentalState = 3;
            currentHealth = 100; //reset
        }
        else if (dentalState == 3 && currentHealth <= 50) { dentalState = 2; }
        else if (dentalState == 2 && currentHealth <= 0)
        { 
            dentalState = 1;
            currentHealth = 100; //reset
        }
        else if (dentalState == 1 && currentHealth <= 50)
        {
            dentalState = 0;
        } 
    }

    public void AddHealth(float healthAmt)
    {
        currentHealth += healthAmt;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log("current health is " + currentHealth);
        Debug.Log("dental state is "+ dentalState);
    }

    public void SubHealth(float healthAmt)
    {
        currentHealth -= healthAmt;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log("current health is " + currentHealth);
        Debug.Log("dental state is "+ dentalState);
    }

    public void UpHealth()
    {
        dentalState ++;
        Debug.Log("dental state is "+ dentalState);
    }
}
