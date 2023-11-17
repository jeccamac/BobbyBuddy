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
    [SerializeField] private GameObject _brushActions = null;
    [SerializeField] private GameObject _flossActions = null;
    [SerializeField] private SpriteRenderer _brushHighlight = null;
    [SerializeField] private ParticleSystem _bubbles = null;
    private Vector3[] startPos;
    private Animator _animBrush = null;
    private Animator _animBrushHL = null;
    private bool hasBrushed = false;
    private bool hasFlossed = false;
    
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
        _animBrushHL = _brushHighlight.gameObject.GetComponent<Animator>();
    }

    private void Start() 
    {
        _brushActions.SetActive(false);
        _flossActions.SetActive(false);

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
        UpdateDental();
    }
    public void CallSpeech(int speechLine)
    {
        string speak = speech[speechLine];
        textDisplay.ShowText(speak, 3f);
    }

    public void BrushTeeth()
    {
        //animate brush to brush position
        if (_animBrush != null)
        {
            CallSpeech(1);
            _animBrush.Play("Brush");

            _brushHighlight.enabled = true;
            _animBrushHL.Play("Glow");

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
        if (actionTimer.timeRemaining > 0 && actionTimer.timerEnded == false)
        {
            actionTimer.CancelTimer();
            hasBrushed = false;
        }
        
        if (_brushActions.activeSelf == true) //only run if brushActions is active, if not active then dont do anything
        {
            if (_animBrush != null)
            {
                _animBrush.Play("Idle");
                _brushHighlight.enabled = false;
            }
            if (_bubbles != null)
            {
                _bubbles.Stop();
            }
        }
    }

    public void StopFlossing()
    {
        if (actionTimer.counterRunning && !actionTimer.counterComplete)
        {
            actionTimer.CancelCounter();
            hasFlossed = false;
        }
    }

    public void HasBrushed()
    {
        //if action was completed, then brushing complete and add tooth health
        if ( actionTimer.timeRemaining <= 00 && actionTimer.timerEnded == true && actionTimer.timerComplete == true )
        {
            hasBrushed = true;
            actionTimer.timerComplete = false;

            //stop animations
            if (_animBrush != null)
            {
                _animBrush.Play("Idle");
                _brushHighlight.enabled = false;
            }

            if (_bubbles != null)
            {
                _bubbles.Stop();
            }

            //reset brush
            ObjectReset();
        }
    }

    public void StartFlossing()
    {
        actionTimer.StartCounter(3);
    }

    public void HasFlossed()
    {
        if ( actionTimer.counterComplete == true )
        {
            hasFlossed = true;
            actionTimer.counterComplete = false;

            //stop animations

            //reset floss
            ObjectReset();
        }
    }

    public void UpdateDental()
    {
        if (hasBrushed)
        {
            DataManager.Instance.AddHealth(20);
            hasBrushed = false;
            Debug.Log("brushing added tooth health amount of 20");
        }

        if (hasFlossed)
        {
            DataManager.Instance.AddHealth(20);
            hasFlossed = false;
            Debug.Log("flossing added 20 tooth health");
        }
    }

    public void ObjectReset()
    {
        for (int i=0; i<bathObjects.Length; i++)
        {
            bathObjects[i].transform.position = startPos[i];
        }
    }
}
