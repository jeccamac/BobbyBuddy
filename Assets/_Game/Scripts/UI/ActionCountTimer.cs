using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
//using UnityEngine.UIElements;

public class ActionCountTimer : MonoBehaviour
{
    [Header("Action Timer Settings")]
    public float timeRemaining;
    public bool timerRunning = false; //enables visibility of action timer panel
    public bool timerEnded = false; //tracks if timer ended, condition for outside scripts to access
    public bool timerComplete = false;

    [Header("Action Counter Settings")]
    public float _counter;
    public float _counterMax;
    public bool counterRunning = false;
    public bool counterComplete = false;

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
    private SwipeDetection swipeDetection;
    
    private void Start()
    {
        swipeDetection = FindObjectOfType<SwipeDetection>();

        actionPanel.SetActive(false);

        animAction = GetComponent<Animator>();

        //stop or disable slider scaleIn animator
        animAction.enabled = false;
        countTimeText.enabled = false;
        announceText.enabled = false;
    }
    private void Update() 
    {
        RunTimer();
        
        RunCounter();
    }

    public void StartTimer(float timeInSeconds)
    {
        timerRunning = true;
        timerEnded = false;
        timerComplete = false;
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

    private void RunTimer()
    {
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
                timerComplete = true;
                Announce("Great Job!", colorSuccess);
            }
        }
    }

    public void Announce(string announceTxt, Color colorTxt)
    {
        if (animAction != null && animAction.enabled)
        {
            animAction.Play("ScaleOut");
        }

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

    public void StartCounter(float countAmt) //enable swipe detection to start counting swipes
    {
        if (!counterRunning && counterComplete == false) //CHECK
        {
            Debug.Log("started counter");
            Announce("Start!", colorSuccess);

            _counter = 0;
            _counterMax = countAmt;
            counterRunning = true;
            swipeDetection.enableSwiping = true;
            
            RunCounter();
        }
        
    }

    private void RunCounter() //animate & display counter, run in Update
    {
        if (counterRunning)
        {
            //counter animation
            actionPanel.SetActive(true);
            countTimeText.enabled = true;
            animAction.enabled = true;
            animAction.Play("ScaleIn");

            //counter display
            actionSlider.value = _counter / _counterMax;
            countTimeText.text = _counter.ToString();

            //separate UI for counter?
        } else if (counterComplete)
        {
            //close counter animation
            //countTimeText.enabled = false;
            Announce("Finished!", colorSuccess);
            Debug.Log("Counter reached maximum");
            
            //reset values
            _counter = 0;
            counterRunning = false;
            counterComplete = false;
            swipeDetection.enableSwiping = false;
        }
    }

    public void CancelCounter()
    {
        counterRunning = false;
        Announce("Canceled!", colorFail);
    }

    public void AddCount() //do the maths
    {
        if (counterRunning)
        {
            _counter++;
            Debug.Log("adding count " + actionSlider.value);
            Debug.Log("counter " + _counter.ToString() + "out of count max" + _counterMax);
        }
        
        if (_counter == _counterMax)
        {
            counterComplete = true;
            counterRunning = false;
            Debug.Log("counter completed");
        }
    }

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
