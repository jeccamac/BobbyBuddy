using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class ActionCountTimer : MonoBehaviour
{
    [Header("Action Timer Settings")]
    [SerializeField] public float timeRemaining;
    [SerializeField] public bool timerRunning = false; //enables visibility of action timer panel
    public bool timerEnded = false; //tracks if timer ended, condition for outside scripts to access


    [Header("Action Counter Settings")]
    [SerializeField] private float _counter;
    [SerializeField] private float _counterMax;
    [SerializeField] public bool counterRunning = false;

    [Header("Image and Text Display")]
    [SerializeField] private GameObject actionPanel = null;
    [SerializeField] private Slider actionSlider;
    [SerializeField] private Text countTimeText = null;
    [SerializeField] public Text announceText = null;
    [SerializeField] private Color colorFail;
    [SerializeField] private Color colorSuccess;
    private Animator animAction;
    private float _minutes;
    private float _seconds;
    
    private void Start()
    {
       // actionPanel = GetComponentInChildren<GameObject>();
        actionPanel.SetActive(false);

        animAction = GetComponent<Animator>();

        //stop or disable slider scaleIn animator
        animAction.enabled = false;
        countTimeText.enabled = false;
        announceText.enabled = false;
    }
    private void Update() {
        if (timerRunning)
        {
            //start timer animation
            animAction.enabled = true;
            animAction.Play("ScaleIn");

            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;

                //slider scale
                actionSlider.value = timeRemaining;

                //text display
                countTimeText.enabled = true;
                DisplayTime(timeRemaining);
            } else if (timeRemaining <= 0) //then if reaches 0
            {
                countTimeText.enabled = false;
                timeRemaining = 0;
                timerRunning = false;

                //conditions to end end timer animation
                timerEnded = true;
                Announce("Great Job!", colorSuccess);
            }
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
        timerEnded = false;
        timeRemaining = timeInSeconds;
        actionSlider.maxValue = timeRemaining;
        actionPanel.SetActive(true);
    }

    public void StopTimer()
    {
        if (timeRemaining != 0 && timerEnded == false)
        {
            timerRunning = false;
            timeRemaining = 0;
            Announce("Canceled", colorFail);
        }
    }

    public void CancelTimer()
    {
        timerRunning = false;
        Announce("Failed!", colorFail);
    }

    public void Announce(string announceTxt, Color colorTxt)
    {
        animAction.Play("ScaleOut");

        StartCoroutine(DisplayAnnouncement());

        IEnumerator DisplayAnnouncement()
        {
            announceText.text = announceTxt;
            announceText.color = colorTxt;

            yield return new WaitForSeconds(1f);
            actionPanel.SetActive(false);
            announceText.enabled = true;
            animAction.Play("PopIn");

            yield return new WaitForSeconds(2f);
            announceText.enabled = false;
        }
        
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
