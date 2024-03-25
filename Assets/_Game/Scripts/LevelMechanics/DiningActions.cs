using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class DiningActions : MonoBehaviour
{
    [Header("Nutrition Facts")]
    [SerializeField] private string[] speech = {};

    [Header("Speech after eating")]
    [Tooltip("Text after eating specific foods")]
    [SerializeField] private string[] speechFood = 
    {
        "Protein",
        "VegFruits",
        "Sweets",
        "Soda",
        "Water"
    };

    public void CallSpeech(int speechLine)
    {
        string speak = speech[speechLine];
        TextDisplay.Instance.ShowText(speak, 5f);
    }

    public void NutritionFacts()
    {
        string speak = speech[Random.Range(0, speech.Length)];
        TextDisplay.Instance.ShowText(speak, 5f);
    }

    public void EatFood(int foodLine) //TEMP FUNCTION FOR TESTING
    {
        string speak = speechFood[foodLine];
        TextDisplay.Instance.ShowText(speak, 5f);
    }
}
