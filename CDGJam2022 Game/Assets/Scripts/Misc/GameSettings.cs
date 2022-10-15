using Unity;
using UnityEngine;

public static class GameSettings {
    const string appVolumeKey = "appvolume";
    const string windowModeKey = "windowmode";

    const float appVolumeDefault = 0.5f;
    const int windowModeDefault = 0;


    public static float AppVolume { get; private set; }
    public static int WindowMode { get; private set; }

    static GameSettings() {
        AppVolume = PlayerPrefs.GetFloat(appVolumeKey, appVolumeDefault);
        if (AppVolume < 0 || AppVolume > 1.0f)
            SetAppVolume(appVolumeDefault);

        WindowMode = PlayerPrefs.GetInt(windowModeKey, windowModeDefault);
        if (WindowMode < 0 || WindowMode > 4)
            SetWindowMode(windowModeDefault);
        WindowMode = windowModeDefault;
    }

    public static void SetAppVolume(float newValue) {
        AudioListener.volume = newValue;
        AppVolume = newValue;
        PlayerPrefs.SetFloat(appVolumeKey, AppVolume);
    }

    public static void SetWindowMode(int newMode) {
        Screen.fullScreenMode = (FullScreenMode)newMode;

        WindowMode = newMode;
        PlayerPrefs.SetInt(windowModeKey, WindowMode);
    }
}