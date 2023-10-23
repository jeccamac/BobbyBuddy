using System.Collections;
using System.Collections.Generic;
//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    [SerializeField] private Image _fadeToBlack = null;
    
    [Header("Scenes")]
    [SerializeField] private string _currentScene;
    [SerializeField] private string _room1;
    [SerializeField] private string _room2;

    [Header("On Scene Load")]
    
    [SerializeField] private bool _fadeIn = true;
    [SerializeField] private float _fadeInDelay = 1;
    
    [Header("On Scene End")]
    [SerializeField] private bool _fadeOut = true;
    [SerializeField] private float _fadeOutDelay = 1;

    

    private void Start() 
    {
        DataManager.Instance.level = _currentScene;

        // Intro Sequence
        if (_fadeIn)
        {
            FadeFromBlack();
        }
    }

    public void TransitionRoom1()
    {
        if (_fadeOut && _fadeToBlack != null)
        {
            StartCoroutine(FadeToBlack(_fadeOutDelay, _room1));
        } else { NextScene(_room1); }
    }

    public void TransitionRoom2()
    {
        if (_fadeOut && _fadeToBlack != null)
        {
            StartCoroutine(FadeToBlack(_fadeOutDelay, _room2));
        } else { NextScene(_room2); }
    }

    private void FadeFromBlack()
    {
        StartCoroutine(FadeFromBlack(_fadeInDelay));
    }

    private IEnumerator FadeFromBlack(float time) // on scene start
    {
        _fadeToBlack.gameObject.SetActive(true);
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            float delta = 1 - t / time;
            _fadeToBlack.color = new Color (0, 0, 0, delta);
            yield return null;
        }

        _fadeToBlack.gameObject.SetActive(false);
    }

    private IEnumerator FadeToBlack(float time, string roomName) // on scene exit
    {
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
