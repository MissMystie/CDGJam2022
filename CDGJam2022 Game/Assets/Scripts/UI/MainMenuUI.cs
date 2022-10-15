using CDGJam;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CDGJam
{
    public class MainMenuUI : MonoBehaviour
    {

        public GameObject mainMenuUI;
        public GameObject settingsUI;
        public GameObject controlsUI;

        [Space]

        public string startScene;

        public enum UIState { MAINMENU, SETTINGS, CONTROLS }
        public UIState state;

        void Start()
        {
            state = UIState.MAINMENU;

            mainMenuUI.SetActive(true);
            settingsUI.SetActive(false);
            controlsUI.SetActive(false);
        }

        public void PlayBtn()
        {
            SceneLoader.LoadScene(startScene);
        }

        public void OpenSettings()
        {
            state = UIState.SETTINGS;

            mainMenuUI.SetActive(false);
            settingsUI.SetActive(true);
        }

        public void CloseSettings()
        {
            state = UIState.MAINMENU;

            settingsUI.SetActive(false);
            mainMenuUI.SetActive(true);
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
}