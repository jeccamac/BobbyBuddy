using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LivingRoomActions : MonoBehaviour
{
    [SerializeField] private Button[] _actionButtons; //CURRENTLY NOT IN USE

    //testing text display
    [SerializeField]
    public string[] speech = 
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

    public void Button1()
    {
        if (speech != null)
        {
            string speak = speech[Random.Range(0, speech.Length)];
            textDisplay.ShowText(speak, 3f);
        }
    }
}
