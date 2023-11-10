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
    
    [Header("Health Color Status")]
    [SerializeField] private Color healthy;
    [SerializeField] private Color yellow;
    [SerializeField] private Color rot;

    [Header("Tooth Icon Images")]
    [SerializeField] private Sprite healthy1, healthy2, yellow1, yellow2, rot1, rot2 = null;

    private void Start() 
    {
        toothIcon.sprite = healthy1;
    }

    private void Update()
    {
        //get health info from data manager
        float _currentHealth = DataManager.Instance.currentHealth;
        float _maxHealth = DataManager.Instance.maxHealth;
        healthBar.fillAmount = _currentHealth / _maxHealth;
    }
}
