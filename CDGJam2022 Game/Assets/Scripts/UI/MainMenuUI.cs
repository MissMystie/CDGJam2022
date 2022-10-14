using CDGJam;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainMenuUI : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject controlsUI;

    public string startScene;

    public enum UIState { MAINMENU, CONTROLS }
    public UIState state;

    void Start()
    {
        state = UIState.MAINMENU;

        mainMenuUI.SetActive(true);
        controlsUI.SetActive(false);
    }

    public void PlayBtn()
    {
        SceneLoader.LoadScene(startScene);
    }

    public void OpenControls()
    {
        state = UIState.CONTROLS;

        mainMenuUI.SetActive(false);
        controlsUI.SetActive(true);
    }

    public void CloseControls()
    {
        state = UIState.MAINMENU;

        controlsUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    public void OnPause()
    {
        CloseControls();
    }

    public void QuitBtn()
    {
        Debug.Log("Quitting the game...");

        #if UNITY_EDITOR

        if (UnityEditor.EditorApplication.isPlaying == true)
            UnityEditor.EditorApplication.isPlaying = false;

        #endif

        Application.Quit();
    }
}
