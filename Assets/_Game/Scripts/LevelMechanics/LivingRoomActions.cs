using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LivingRoomActions : MonoBehaviour
{
    //testing text display
    [Tooltip("Series of speech text that will be randomized every time the function is called")]
    [SerializeField] public string[] speech = 
    {
        "Hey this is Bobby!",
        "I'm a bit ticklish.",
        "Poke my belly!",
        "You're my best friend."
    };

    private TextDisplay textDisplay;
    private void Start() 
    {
        textDisplay = FindObjectOfType<TextDisplay>();
    }

    public void CallSpeech()
    {
        if (speech != null)
        {
            string speak = speech[Random.Range(0, speech.Length)];
            textDisplay.ShowText(speak, 3f);
        }
    }
}
