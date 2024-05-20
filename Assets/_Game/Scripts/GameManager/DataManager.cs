using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

    [Header("Tooth Health Settings")]
    [SerializeField] public float currentHealth = 100;
    [SerializeField] public float maxHealth = 100;
    public int dentalState = 5;

    [Header("Hunger Settings")]
    [SerializeField] public float currentHunger = 100;
    [SerializeField] private float maxHunger = 100;
    public int hungerState = 2;
    
    [Tooltip("Hunger rate is subtracted from currentHunger over hungerTimeStart")]
    [SerializeField] public float hungerRate = 1;

    [Tooltip("Timer start countdown in seconds")]
    [SerializeField] private float hungerTimeStart = 3f; //constant
    private float hungerTimer;
    public bool hungerOn = false;

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

        hungerTimer = hungerTimeStart;

        //_textDisplay = GetComponent<TextDisplay>();
    }
    private void Update()
    {
        ChangeDentalState();
        //HungerStatus();
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
        if (dentalState == 5 && currentHealth >= 50){ dentalState = 5; } //healthy
        else if (dentalState == 5 && currentHealth <= 50){ dentalState = 4; } //good
        else if (dentalState == 4 && currentHealth >= 50){ dentalState = 5; } //back up to healthy
        else if (dentalState == 4 && currentHealth <= 0) //down to yellow
        { 
            dentalState = 3;
            currentHealth = 100; //reset
        }
        else if (dentalState == 3 && currentHealth >= 50) { dentalState = 3; } //yellow1 if above 50
        else if (dentalState == 3 && currentHealth <= 50) { dentalState = 2; } //yellow2 if below 50
        else if (dentalState == 2 && currentHealth >= 50) { dentalState = 3; } //back up to yellow 1
        else if (dentalState == 2 && currentHealth <= 0) //down to rot
        { 
            dentalState = 1;
            currentHealth = 100; //reset
        }
        else if (dentalState == 1 && currentHealth >= 50) { dentalState = 1; } //rot1 above 50
        else if (dentalState == 1 && currentHealth <= 50) { dentalState = 0; } //rot2 below 50
        else if (dentalState == 0 && currentHealth >= 50) { dentalState = 1; } //back up to rot1
    }

    private void HungerStatus()
    {
        if (hungerOn)
        {
            hungerTimer -= Time.deltaTime; //countdown
            if (hungerTimer <= 0)
            {
                currentHunger = currentHunger - hungerRate;
                currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);
                if (hungerState == 0) { TextDisplay.Instance.ShowText("Bobby is hungry, let's get some food!", 3f); }
                hungerTimer = hungerTimeStart; //reset
            }

            if (currentHunger >= 90) { hungerState = 2; } //full
            else if (currentHunger <= 40) { hungerState = 1; } //hungry
            else if (currentHunger <= 10) { hungerState = 0; }//starving, disable certain mechanics if hungry
        }
    }

    public void AddHunger(float hungerAmt)
    {
            currentHunger += hungerAmt;
            currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);
    }

    public void AddHealth(float healthAmt)
    {
        currentHealth += healthAmt;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public void SubHealth(float healthAmt)
    {
        currentHealth -= healthAmt;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public void UpHealth()
    {
        var curHealth = currentHealth; //save health stat to be compared later to new health stat
        if (currentHealth <= 50)
        {
            currentHealth += 50; //increase 1 dental state
        } else if (currentHealth >= 50 && dentalState != 5)
        {
            dentalState += 1; //increase to next dental state
            dentalState = Mathf.Clamp(dentalState, 0, 5);
            currentHealth = Mathf.Abs(50 - curHealth); //subtract the next dental state health from previous health stat
        } else if (currentHealth >= 50 && dentalState == 1)
        {
            dentalState += 1;
            currentHealth = Mathf.Abs(50 - curHealth);
        }
        Debug.Log("Dental state is currently" + dentalState);
        Debug.Log("Tooth health is currently" + currentHealth);
        // dentalState += 2;
    }
}
