using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace CDGJam
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;
        public static PlayerControls controls;

        public GameObject hudUI;
        public GameObject pauseMenuUI;
        public GameObject settingsUI;
        public GameObject controlsUI;
        public StudioEventEmitter bgm;

        [Space]

        public GameObject player;

        public bool isPaused = false;

        public enum UIState { HUD, PAUSE, SETTINGS, CONTROLS }
        public UIState state;

        void Awake()
        {
            //If an instance already exists
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            controls = new PlayerControls();

            player = LevelManager.Instance.player;
        }

        private void OnEnable()
        {
            controls.Enable();
        }

        private void OnDisable()
        {
            controls.Disable();
        }

        private void Start()
        {
            state = UIState.HUD;

            hudUI.SetActive(true);
            pauseMenuUI.SetActive(false);
            settingsUI.SetActive(false);
            controlsUI.SetActive(false);

            controls.UI.Pause.performed += ctx => OnPause();
        }

        void OnPause()
        {
            switch (state)
            {
                case UIState.HUD:
                    Pause();
                    break;
                case UIState.PAUSE:
                    Unpause();
                    break;
                case UIState.SETTINGS:
                    CloseSettings();
                    break;
                case UIState.CONTROLS:
                    CloseControls();
                break;
            }
        }

        public void Pause()
        {
            state = UIState.PAUSE;

            hudUI.SetActive(false);
            pauseMenuUI.SetActive(true);
            controlsUI.SetActive(false);

            Time.timeScale = 0f;

            isPaused = true;
        }

        public void Unpause()
        {
            state = UIState.HUD;

            hudUI.SetActive(true);
            pauseMenuUI.SetActive(false);
            controlsUI.SetActive(false);

            Time.timeScale = 1f;

            isPaused = false;
        }

        public void OpenSettings()
        {
            state = UIState.SETTINGS;

            pauseMenuUI.SetActive(false);
            settingsUI.SetActive(true);
        }

        public void CloseSettings()
        {
            state = UIState.PAUSE;

            settingsUI.SetActive(false);
            pauseMenuUI.SetActive(true);
        }

        public void OpenControls()
        {
            state = UIState.CONTROLS;

            pauseMenuUI.SetActive(false);
            controlsUI.SetActive(true);
        }

        public void CloseControls()
        {
            state = UIState.PAUSE;

            pauseMenuUI.SetActive(true);
            controlsUI.SetActive(false);
        }
    }
}
