using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class BathroomActions : MonoBehaviour
{
    [Header("Display Settings")]
    [SerializeField] private ActionCountTimer actionTimer = null;
    [SerializeField] private TextDisplay textDisplay;

    [Header("Bathroom Settings")]
    [SerializeField] private GameObject[] bathObjects = {};
    // [SerializeField] private GameObject _brushActions = null;
    // [SerializeField] private GameObject _flossActions = null;
    [SerializeField] private GameObject[] bathActions = {};
    [SerializeField] private SpriteRenderer _areaHighlight = null;
    [SerializeField] private ParticleSystem _bubbles = null;
    private Vector3[] startPos;
    private Animator _animBrush, _animAreaHL, _animFloss, _animMW = null;
    private bool hasBrushed, hasFlossed, hasMouthwash, clickedAway = false;

    
    [Tooltip("Series of speech text every time the function is called")]
    [SerializeField] private string[] speech = 
    {
        "Help me brush my teeth!",
        "Keep brushing for 2 minutes.",
        "Flossing is good for you.",
        "Freshen up with mouthwash."
    };

    private void Awake() 
    {
        _animBrush = bathObjects[0].GetComponent<Animator>();
        _animAreaHL = _areaHighlight.gameObject.GetComponent<Animator>();
        _animFloss = bathObjects[1].GetComponent<Animator>();
        _animMW = bathObjects[2].GetComponent<Animator>();
    }

    private void Start() 
    {
        _areaHighlight.enabled = false;
        _animFloss.enabled = false;

        //set all actions and trigger boxes to false on start
        for (int i=0; i < bathActions.Length; i++)
        {
            bathActions[i].SetActive(false);
        }

        //save start position of all bathroom objects that will be moved around
        startPos = new Vector3[bathObjects.Length];
        for (int i=0; i < bathObjects.Length; i++)
        {
            startPos[i] = bathObjects[i].transform.position;
        }
    }

    private void Update()
    {
        HasBrushed();
        HasFlossed();
        HasMouthwash();
        UpdateDental();
    }
    public void CallSpeech(int speechLine)
    {
        string speak = speech[speechLine];
        textDisplay.ShowText(speak, 3f);
    }

        //BRUSH ACTIONS
    public void BrushTeeth()
    {
        //animate brush to brush position
        if (_animBrush != null)
        {
            CallSpeech(1);
            _animBrush.Play("Brush");

            _areaHighlight.enabled = true;
            _animAreaHL.Play("Glow");

            //start timer
            actionTimer.StartTimer(5);
        }
        
        //particle effects
        if (_bubbles != null)
        {
            _bubbles.Play();
        }

        //animation? how to do - if drag up & down, then bubbles
    }

    public void StopBrushing()
    {
        if (clickedAway == true && actionTimer.timeRemaining != 0)
        {
            actionTimer.CancelTimer();
            actionTimer.timeRemaining = 0; //reset
        } 
        else if (clickedAway == false && actionTimer.timeRemaining > 0 && !actionTimer.timerEnded)
        {
            actionTimer.StopTimer();
            hasBrushed = false;
        }
        
        if (_animBrush != null)
        {
            _animBrush.Play("Idle");
            _areaHighlight.enabled = false;
        }
        if (_bubbles != null)
        {
            _bubbles.Stop();
        }
    }

    public void HasBrushed()
    {
        //if action was completed, then brushing complete and add tooth health
        if (actionTimer.timeRemaining <= 0 && actionTimer.timerEnded && actionTimer.timerComplete)
        {
            hasBrushed = true;
            actionTimer.timerComplete = false;

            //stop animations
            if (_animBrush != null)
            {
                _animBrush.Play("Idle");
                _areaHighlight.enabled = false;
            }

            if (_bubbles != null)
            {
                _bubbles.Stop();
            }

            //reset brush
            ObjectReset();
        }
    }


        //FLOSS ACTIONS
    public void StartFlossing()
    {
        actionTimer.StartCounter(3);

        if (_animFloss != null)
        {
            _animFloss.enabled = true;
            _animFloss.Play("Open");
        }
    }

    public void StopFlossing()
    {
        if (actionTimer.counterRunning && !actionTimer.counterComplete)
        {
            actionTimer.CancelCounter();
            hasFlossed = false;

            if (_animFloss != null)
            {
                _animFloss.Play("Close");
            }

        }
    }

    public void HasFlossed()
    {
        if ( actionTimer.counterComplete == true )
        {
            hasFlossed = true;
            actionTimer.counterComplete = false;

            //stop animations
            if (_animFloss != null)
            {
                _animFloss.Play("Close");
            }

            ObjectReset();
        }
    }

        //MOUTHWASH ACTIONS
    public void StartMouthwash()
    {
        // if (_animMW != null)
        // {
        //     //start animation
        //     CallSpeech(3);
        //     _animMW.Play("Open");

        //     _areaHighlight.enabled = true;
        //     _animAreaHL.Play("Glow");

        //     actionTimer.StartTimer(3);
        // }

        _areaHighlight.enabled = true;
        _animAreaHL.Play("Glow");

        actionTimer.StartTimer(3);        
    }

    public void StopMouthwash()
    {
        if (clickedAway == true && actionTimer.timeRemaining != 0)
        {
            actionTimer.CancelTimer();
            actionTimer.timeRemaining = 0; //reset
        }
        else if (clickedAway == false && actionTimer.timeRemaining > 0 && !actionTimer.timerEnded)
        {
            actionTimer.StopTimer();
            hasBrushed = false;
        }
        
        if (_animMW != null)
        {
            _animMW.Play("Close");
            //_areaHighlight.enabled = false;
        }
        _areaHighlight.enabled = false; //delete when anim is added
    }

    public void HasMouthwash()
    {
        //if action was completed, then mouthwash complete
        if (actionTimer.timeRemaining <= 0 && actionTimer.timerEnded && actionTimer.timerComplete)
        {
            hasMouthwash = true;
            actionTimer.timerComplete = false;

            //stop animations
            if (_animMW != null)
            {
                _animMW.Play("Close");
                _areaHighlight.enabled = false;
            }

            //reset position
            ObjectReset();
        }
    }

    private void UpdateDental()
    {
        if (hasBrushed)
        {
            DataManager.Instance.AddHealth(15);
            hasBrushed = false;
            Debug.Log("brushing added tooth health amount of 20");
        }

        if (hasFlossed)
        {
            DataManager.Instance.AddHealth(10);
            hasFlossed = false;
            Debug.Log("flossing added 15 tooth health");
        }

        if (hasMouthwash)
        {
            DataManager.Instance.AddHealth(5);
            Debug.Log("mouthwash added 5 tooth health");
        }
    }

    public void ActionReset()
    {
        clickedAway = true;
        
        StopBrushing();
        StopFlossing();
        StopMouthwash();
        ObjectReset();

        _animFloss.enabled = false;

        clickedAway = false;
    }
    private void ObjectReset()
    {
        for (int i=0; i<bathObjects.Length; i++)
        {
            bathObjects[i].transform.position = startPos[i];
        }
    }
}
