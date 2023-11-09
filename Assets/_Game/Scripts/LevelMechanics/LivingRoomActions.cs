using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LivingRoomActions : MonoBehaviour
{
    [SerializeField] private TextDisplay textDisplay;

    [Tooltip("Series of speech text that will be randomized every time the function is called")]
    [SerializeField] public string[] speech = 
    {
        "Hey this is Bobby!",
        "I'm a bit ticklish.",
        "Poke my belly!",
        "You're my best friend."
    };
    public void CallSpeech()
    {
        if (speech != null)
        {
            string speak = speech[Random.Range(0, speech.Length)];
            textDisplay.ShowText(speak, 3f);
        }
    }
}
