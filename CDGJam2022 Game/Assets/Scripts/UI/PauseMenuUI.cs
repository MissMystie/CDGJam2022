using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CDGJam
{
    public class PauseMenuUI : MonoBehaviour
    {
        public string mainMenuScene = "MainMenu";

        public void ResumeBtn()
        {
            UIManager.Instance.Unpause();
        }

        public void PauseBtn()
        {
            UIManager.Instance.Pause();
        }

        public void SettingsBtn()
        {
            UIManager.Instance.OpenSettings();
        }

        public void ControlsBtn()
        {
            UIManager.Instance.OpenControls();
        }

        public void MainMenuBtn()
        {
            Debug.Log("Loading main menu...");
            SceneLoader.LoadScene(mainMenuScene);
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
