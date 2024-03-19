using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasButtons : MonoBehaviour
{
    [Header("Canvas Buttons Settings")]
    [SerializeField] GameObject menuPanel, raycastBlock = null;

    public void OpenSettings()
    {
        if (menuPanel != null) { menuPanel.SetActive(true); }
        if (raycastBlock != null) { raycastBlock.SetActive(true); }
        AudioManager.Instance.PlaySFX("Button Cancel");
    }

    public void ResumeButton()
    {
        if (menuPanel != null) { menuPanel.SetActive(false); }
        if (raycastBlock != null) { raycastBlock.SetActive(false); }
        AudioManager.Instance.PlaySFX("Button Select");
    }

    public void CreditsButton()
    {
        //get credits panel
        //raycast block?
        AudioManager.Instance.PlaySFX("Button Select");
    }
}
