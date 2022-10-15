using CDGJam;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CDGJam
{
    public class MainMenuUI : MonoBehaviour
    {
        public static PlayerControls controls;

        public GameObject mainMenuUI;
        public GameObject settingsUI;
        public GameObject controlsUI;

        [Space]

        public string startScene;

        public enum UIState { MAINMENU, SETTINGS, CONTROLS }
        public UIState state;

        private void OnAwake()
        {
            controls = new PlayerControls();
        }

        private void OnEnable()
        {
            controls.Enable();
        }

        private void OnDisable()
        {
            controls.Disable();
        }

        void Start()
        {
            state = UIState.MAINMENU;

            mainMenuUI.SetActive(true);
            settingsUI.SetActive(false);
            controlsUI.SetActive(false);

            controls.UI.Pause.performed += ctx => OnPause();
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
            if (state == UIState.SETTINGS)
            {
                state = UIState.MAINMENU;

                settingsUI.SetActive(false);
                mainMenuUI.SetActive(true);
            }
        }

        public void OpenControls()
        {
            state = UIState.CONTROLS;

            mainMenuUI.SetActive(false);
            controlsUI.SetActive(true);
        }

        public void CloseControls()
        {
            if (state == UIState.CONTROLS)
            {
                state = UIState.MAINMENU;

                controlsUI.SetActive(false);
                mainMenuUI.SetActive(true);
            }  
        }

        public void OnPause()
        {
            if (state == UIState.CONTROLS) CloseControls();
            else if (state == UIState.SETTINGS) CloseSettings();
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