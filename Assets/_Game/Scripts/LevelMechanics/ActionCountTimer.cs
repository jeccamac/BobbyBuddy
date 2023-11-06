using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class ActionCountTimer : MonoBehaviour
{
    [Header("Action Timer Settings")]
    [SerializeField] private float _timerSeconds;
    [SerializeField] public bool timerRunning = false;

    [Header("Action Counter Settings")]
    [SerializeField] private float _counter;
    [SerializeField] private float _counterMax;
    [SerializeField] public bool counterRunning = false;

    [Header("Image and Text Display")]
    private Animator animAction;
    [SerializeField] private Slider actionSlider;
    [SerializeField] public Text countTimeText = null;

    private float _minutes;
    private float _seconds;


    private void Start()
    {
        animAction = GetComponent<Animator>();
        //actionSlider = GetComponent<Slider>();

        //stop or disable slider scaleIn animator
        animAction.enabled = false;
        countTimeText.enabled = false;
    }
    private void Update() {
        if (timerRunning)
        {
            //start timer animation
            animAction.enabled = true;
            animAction.Play("ScaleIn");

            if (_timerSeconds > 0)
            {
                _timerSeconds -= Time.deltaTime;

                //slider scale
                actionSlider.value = _timerSeconds;

                //text display
                countTimeText.enabled = true;
                DisplayTime(_timerSeconds);
            } else 
            { 
                Debug.Log("Timer has run out!");
                countTimeText.enabled = false;
                _timerSeconds = 0;
                timerRunning = false;
            }
        } else
        {
            //end timer animation
            animAction.Play("ScaleOut");
            animAction.enabled = false;
        }
        
        /*
        if (counterRunning)
        {
            AddCount();
            //separate UI for counter?
        } */
    }

    public void StartTimer(float timeInSeconds)
    {
        timerRunning = true;
        _timerSeconds = timeInSeconds;
        actionSlider.maxValue = _timerSeconds;
    }

    /*
    public void StartCounter(float countAmt)
    {
        counterRunning = true;
        _counterMax = countAmt;
    }

    public void AddCount()
    {
        if (_counter <= 0)
        {
            _counter++;
            
        } else if (_counter >= _counterMax)
        {
            counterRunning = false;
            Debug.Log("Counter reached maximum");
        }
    }
    */

    private void DisplayTime(float timeDisplay)
    {
        if (countTimeText.enabled == true)
        {
            timeDisplay += 1;
            _minutes = Mathf.FloorToInt(timeDisplay / 60);
            _seconds = Mathf.FloorToInt(timeDisplay % 60);
            countTimeText.text = string.Format("{0:00}:{1:00}", _minutes, _seconds);
        }
    }
}
