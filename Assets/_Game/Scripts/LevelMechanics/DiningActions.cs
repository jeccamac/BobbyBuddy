using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class DiningActions : MonoBehaviour
{
    [SerializeField] private TextDisplay textDisplay;
    [SerializeField] private string[] speech = {};

    private void Awake() 
    {
        textDisplay = FindObjectOfType<TextDisplay>();
    }

    public void CallSpeech(int speechLine)
    {
        string speak = speech[speechLine];
        textDisplay.ShowText(speak, 3f);
    }

    public void CallRandomSpeech()
    {
        string speak = speech[Random.Range(0, speech.Length)];
        textDisplay.ShowText(speak, 3f);
    }
}
