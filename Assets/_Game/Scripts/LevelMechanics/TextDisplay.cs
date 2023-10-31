using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour
{
    [Header("Text Display Settings")]
    [Tooltip("Text display to inform player")]
    [SerializeField] private Text _textDisplay = null;
    [SerializeField] private Image _objImage = null;
    private Animator _animator;

    private void Start() 
    {
        _animator = GetComponent<Animator>();
        _textDisplay.enabled = false;
        _objImage.enabled = false;
    }

    //call this event from anywhere
    public void ShowText(string textInfo, float textDelay)
    {
        _textDisplay.enabled = true;
        _objImage.enabled = true; //enable background image
        _animator.Play("FadeIn");

        StartCoroutine(TextSequence()); //start timer

        IEnumerator TextSequence()
        {
            //display text
            if (_textDisplay != null) { _textDisplay.text = textInfo; }

            //wait for seconds before deleting
            yield return new WaitForSeconds(textDelay);
            _animator.Play("FadeOut");
            _textDisplay.enabled = false;
            _objImage.enabled = false;
        }
    }
}
