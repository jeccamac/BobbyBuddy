using System.Collections;
using System.Collections.Generic;
//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    [SerializeField] private Image _fadeToBlack = null;
    [SerializeField] private GameObject _raycastBlock = null;
    [SerializeField] private TextDisplay _textDisplay = null;
    
    [Header("Scenes")]
    [SerializeField] private string _currentScene;
    [SerializeField] private string _room1;
    [SerializeField] private string _room2;
    
    [Tooltip("Extra Room Option")]
    [SerializeField] private string _optionalRoom3;

    [Tooltip("Condition to transition to optional room 3")]
    [SerializeField] public bool _canTravel;

    [Header("On Scene Load")]
    
    [SerializeField] private bool _fadeIn = true;
    [SerializeField] private float _fadeInDelay = 1;
    public Animator _bobbyAnim;
    [SerializeField] public string _animStart;
    
    [Header("On Scene End")]
    [SerializeField] private bool _fadeOut = true;
    [SerializeField] private float _fadeOutDelay = 1;

    private void Start() 
    {
        DataManager.Instance.level = _currentScene;
        //_textDisplay = GetComponent<TextDisplay>();
        _bobbyAnim = GameObject.FindWithTag("Player").GetComponent<Animator>();

        // Intro Sequence
        if (_raycastBlock != null) { _raycastBlock.gameObject.SetActive(true); }
        if (_fadeIn)
        {
            FadeFromBlack();
        }
    }

    private void Update() 
    {
        if (DataManager.Instance.hungerState != 0)
        {
            _canTravel = true;
        } else { _canTravel = false; }
    }

    public void TransitionRoom1()
    {
        if (_fadeOut && _fadeToBlack != null)
        {
            _fadeToBlack.gameObject.SetActive(true);
            StartCoroutine(FadeToBlack(_fadeOutDelay, _room1));
        } else { NextScene(_room1); }
    }

    public void TransitionRoom2()
    {
        if (_fadeOut && _fadeToBlack != null)
        {
            _fadeToBlack.gameObject.SetActive(true);
            StartCoroutine(FadeToBlack(_fadeOutDelay, _room2));
        } else { NextScene(_room2); }
    }

    public void TransitionRoom3()
    {
        if (_canTravel)
        {
            if (_fadeOut && _fadeToBlack != null)
            {
                _fadeToBlack.gameObject.SetActive(true);
                StartCoroutine(FadeToBlack(_fadeOutDelay, _optionalRoom3));
            } else { NextScene(_optionalRoom3); }
        } else { _textDisplay.ShowText("Bobby is too hungry to go out.", 3f); }
        
    }

    public void QuitToMenu()
    {
        // quit to menu
        if (_fadeOut && _fadeToBlack != null)
        {
            _fadeToBlack.gameObject.SetActive(true);
            StartCoroutine(FadeToBlack(_fadeOutDelay, "MainScreen"));
        } else { NextScene("MainScreen"); }
    }

    private void FadeFromBlack()
    {
        StartCoroutine(FadeFromBlack(_fadeInDelay));
    }

    private IEnumerator FadeFromBlack(float time) // on scene start
    {
        _fadeToBlack.gameObject.SetActive(true);

        // bobby animation on scene start
        _bobbyAnim.Play(_animStart);
        //Debug.Log("animation start " + _animStart);

        //fade from black screen
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            float delta = 1 - t / time;
            _fadeToBlack.color = new Color (0, 0, 0, delta);
            yield return null;
        }

        _fadeToBlack.gameObject.SetActive(false);
        if (_raycastBlock != null) { _raycastBlock.gameObject.SetActive(false); }
    }

    private IEnumerator FadeToBlack(float time, string roomName) // on scene exit
    {
        if (_raycastBlock != null) { _raycastBlock.gameObject.SetActive(true); }
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            float delta = t / time;
            _fadeToBlack.color = new Color (0, 0, 0, delta);
            yield return null;
        }

        NextScene(roomName);
    }

    private void NextScene(string _nextScene)
    {
        DataManager.Instance.level = _nextScene;
        DataManager.SceneLoader.LoadScene(_nextScene);
    }
}
