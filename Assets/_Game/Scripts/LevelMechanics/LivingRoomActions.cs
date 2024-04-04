using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LivingRoomActions : MonoBehaviour
{
    [Header("Living Room Settings")]
    [SerializeField] private GameObject _lights = null;
    [SerializeField] public Animator _bobbyAnim = null;
    private bool _lightsEnabled = true;

    [Header("Display Settings")]

    [Tooltip("Series of speech text that will be randomized every time the function is called")]
    [SerializeField] public string[] speech = 
    {
        "Hey this is Bobby!",
        "I'm a bit ticklish.",
        "Tap my belly!",
        "You're my best friend."
    };

    [Tooltip("Series of dancing animations that will be randomized every time the function is called")]
    [SerializeField] public string[] dance = {}; //add dance animations
    
    private void Awake() 
    {
        _bobbyAnim = GameObject.FindWithTag("Player").GetComponent<Animator>();
    }

    public void CallSpeech()
    {
        if (speech != null)
        {
            string speak = speech[Random.Range(0, speech.Length)];
            TextDisplay.Instance.ShowText(speak, 3f);
        }
    }

    public void Dance() //random dancing animations here
    {
        if (DataManager.Instance.hungerOn)  // if HUNGER ON
        {
            if (DataManager.Instance.hungerState != 0)
            {
                if (dance != null) 
                {
                    string danceNo = dance[Random.Range(0, dance.Length)];
                    _bobbyAnim.Play(danceNo);
                }
            } else
            { 
                TextDisplay.Instance.ShowText("Bobby is too hungry to dance right now.", 3f); 
                _bobbyAnim.Play("Idle");
            }
        } else                              // else HUNGER OFF behavior
        {
            if (dance != null) 
            {
                string danceNo = dance[Random.Range(0, dance.Length)];
                _bobbyAnim.Play(danceNo);
            }
        }
        
    
        AudioManager.Instance.PlaySFX("Button Click");
    }

    public void ToggleLights()
    {
        //turn off lights
        if (_lights != null)
        {
            if (_lightsEnabled == true)
            {
                _lights.SetActive(false);
                _lightsEnabled = false;
                
                AudioManager.Instance.PlaySFX("Lights Off");
            } else
            {
                _lights.SetActive(true);
                _lightsEnabled = true;
                TextDisplay.Instance.ShowText("Did you touch the light switch?", 3f);
                AudioManager.Instance.PlaySFX("Lights On");
            }
        }

        _bobbyAnim.Play("Spooked");
        Debug.Log("light settings " + _lights.activeSelf);
    }

    public void IdleAnim()
    {
        _bobbyAnim.Play("Idle");
    }
}
