using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainMenuUI : MonoBehaviour
{
    public UnityEvent playBtn;
    public UnityEvent controlsBtn;
    public UnityEvent mainMenuBtn;
    public UnityEvent quitBtn;

    public GameObject mainMenuPanel;
    public GameObject controlsPanel;

    void Start()
    {
        
    }

    public void Play()
    {
        mainMenuPanel.SetActive(true);
        controlsPanel.SetActive(false);
    }

    public void OpenControls()
    {
        mainMenuPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    public void OpenMainMenu()
    {
        controlsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void Quit()
    {
        Debug.Log("Quitting the game...");

        #if UNITY_EDITOR

        if (UnityEditor.EditorApplication.isPlaying == true)
            UnityEditor.EditorApplication.isPlaying = false;

        #endif

        Application.Quit();
    }
}
