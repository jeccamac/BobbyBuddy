using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class BathroomActions : MonoBehaviour
{
    [SerializeField] public ActionCountTimer actionTimer = null;
    [SerializeField] private SpriteRenderer _highlight = null;
    [SerializeField] private ParticleSystem _bubbles = null;
    private Vector3[] startPos;
    [SerializeField] private GameObject[] bathObjects = {};
    private GameObject _brush = null;
    private Animator _animBrush = null;
    private Animator _animHighlight = null;
    private TextDisplay textDisplay;
    

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
        actionTimer = FindObjectOfType<ActionCountTimer>();
        textDisplay = FindObjectOfType<TextDisplay>();
        _brush = GameObject.Find("Toothbrush");
        _highlight = FindObjectOfType<SpriteRenderer>();

        _animBrush = _brush.gameObject.GetComponent<Animator>();
        _animHighlight = _highlight.gameObject.GetComponent<Animator>();

        _bubbles = FindObjectOfType<ParticleSystem>();
    }

    private void Start() 
    {
        _highlight.enabled = false;
        _brush.SetActive(false);

        //save start position of all bathroom objects that will be moved around
        startPos = new Vector3[bathObjects.Length];
        for (int i=0; i < bathObjects.Length; i++)
        {
            startPos[i] = bathObjects[i].transform.position;
        }
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

            _highlight.enabled = true;
            _animHighlight.Play("Glow");

            //start timer
            actionTimer.StartTimer(5);
        }
        
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
        }
        
        if (_animBrush != null)
        {
            _animBrush.Play("Idle");
            _highlight.enabled = false;
        }
        if (_bubbles != null)
        {
            _bubbles.Stop();
        }
    }

    public void ObjectReset()
    {
        for (int i=0; i<bathObjects.Length; i++)
        {
            bathObjects[i].transform.position = startPos[i];
        }
        Debug.Log("reset object position");
    }
    public void ActionTime() //THIS IS A TEMP FUNCTION TO TEST TIMER/COUNTER. DELETE IN FUTURE IMPLEMENTATIONS
    {
        actionTimer.StartTimer(10);
    }
}
