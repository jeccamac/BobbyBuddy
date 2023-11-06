using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BathroomActions : MonoBehaviour
{
    //[SerializeField] private Button[] _actionButtons; //CURRENTLY NOT IN USE

    [SerializeField] public ActionCountTimer actionTimer = null;
    
    [SerializeField] private Rigidbody _brush = null;
    [SerializeField] private SpriteRenderer _highlight = null;
    [SerializeField] private ParticleSystem _bubbles = null;
    private Animator _animBrush = null;
    private Animator _animHighlight = null;
    private TextDisplay textDisplay;

    [Tooltip("Series of speech text that will be randomized every time the function is called")]
    [SerializeField] public string[] speech = 
    {
        "Help me brush my teeth!",
        "Keep brushing inside the box for 2 minutes.",
        "Flossing is good for you.",
        "Freshen up with mouthwash."
    };

    private void Awake() 
    {
        actionTimer = FindObjectOfType<ActionCountTimer>();
        textDisplay = FindObjectOfType<TextDisplay>();
        _brush = FindObjectOfType<Rigidbody>();
        _highlight = FindObjectOfType<SpriteRenderer>();

        _animBrush = _brush.gameObject.GetComponent<Animator>();
        _animHighlight = _highlight.gameObject.GetComponent<Animator>();

        _bubbles = FindObjectOfType<ParticleSystem>();
    }

    private void Start() 
    {
        _highlight.enabled = false;
    }

    public void CallSpeech(int speechLine)
    {
        string speak = speech[speechLine];
        textDisplay.ShowText(speak, 3f);
    }

    public void BrushTeeth()
    {
        //_brush.useGravity = false;
        //animate brush to brush position
        if (_animBrush != null)
        {
            CallSpeech(1);
            _animBrush.Play("Brush");

            _highlight.enabled = true;
            _animHighlight.Play("Glow");

            //start timer

        }
        
        if (_bubbles != null)
        {
            _bubbles.Play();
        }
        //? how to do - if drag up & down, then bubbles
    }

    public void StopBrushing()
    {
        //_brush.useGravity = true;
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

    public void ActionTime() //THIS IS A TEMP FUNCTION TO TEST TIMER/COUNTER. DELETE IN FUTURE IMPLEMENTATIONS
    {
        actionTimer.StartTimer(10);
    }
}
