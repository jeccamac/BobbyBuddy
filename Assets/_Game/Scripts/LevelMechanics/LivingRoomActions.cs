using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivingRoomActions : MonoBehaviour
{
    [SerializeField] private Button[] _actionButtons; //CURRENTLY NOT IN USE

    //testing text display
    [SerializeField] private string text;
    private TextDisplay textDisplay;

    private void Start() 
    {
        textDisplay = FindObjectOfType<TextDisplay>();
    }

    public void Button1()
    {
        if (text != null)
        {
            textDisplay.ShowText(text, 3f);
        }
    }
}
