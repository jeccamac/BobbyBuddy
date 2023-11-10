using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance = null; // Singleton instance
    [Header("Scene Settings")]
    [SerializeField] private SceneLoader _sceneLoader = null;
    public static SceneLoader SceneLoader => Instance._sceneLoader;
    public string level {get; set;} // current level room

    [Header("Player Health Settings")]
    [SerializeField] public float currentHealth = 360;
    [SerializeField] public float maxHealth = 360;

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

    public void HealthUpdate()
    {
        
    }
}
