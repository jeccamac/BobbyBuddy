using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadSceneAsync(sceneIndex));
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private static IEnumerator LoadSceneAsync(int buildIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(buildIndex);
        while (!asyncLoad.isDone) { yield return null; }
    }

    private static IEnumerator LoadSceneAsync(string buildName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(buildName);
        while (!asyncLoad.isDone) { yield return null; }
    }
}
