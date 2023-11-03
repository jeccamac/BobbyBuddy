using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomActions : MonoBehaviour
{
    //[SerializeField] private Button[] _actionButtons; //CURRENTLY NOT IN USE
    
    [SerializeField] private Rigidbody _brush = null;
    [SerializeField] private ParticleSystem _bubbles = null;
    [SerializeField] private Animator _animBrush = null;

    [Tooltip("Series of speech text that will be randomized every time the function is called")]
    [SerializeField] public string[] speech = 
    {
        "Brush my teeth!",
        "Flossing is good for you.",
        "Freshen up with mouthwash."
    };

    private TextDisplay textDisplay;
    private void Awake() 
    {
        textDisplay = FindObjectOfType<TextDisplay>();
        _brush = FindObjectOfType<Rigidbody>();
        _animBrush = _brush.gameObject.GetComponent<Animator>();
        _bubbles = FindObjectOfType<ParticleSystem>();
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
            _animBrush.Play("Brush");
            Debug.Log("brushing");
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
        }
        if (_bubbles != null)
        {
            _bubbles.Stop();
        }
    }
}
