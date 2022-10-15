using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CDGJam
{
    public class AudioSettings : MonoBehaviour
    {
        private void Awake()
        {
            Init();
        }

        public AudioBus[] buses = new AudioBus[1];

        private void OnEnable() { foreach (AudioBus bus in buses) bus.Enable(); }

        private void OnDisable() { foreach (AudioBus bus in buses) bus.Disable(); }

        public void Init() { foreach (AudioBus bus in buses) bus.LoadBus(); }
    }

    [System.Serializable]
    public class AudioBus
    {
        public string name;
        public string key;
        public string busName;
        public float volume = 1f;
        public Slider slider;

        protected Bus bus;

        public AudioBus(string _key, string _busName) { key = _key; busName = _busName; }

        public void Enable()
        {
            if (slider != null)
            {
                slider.onValueChanged.AddListener(delegate { SetVolume(slider.value / slider.maxValue); });
            }
        }

        public void Disable()
        {
            if (slider != null)
            {
                slider.onValueChanged.RemoveListener(delegate { SetVolume(slider.value / slider.maxValue); });
            }
        }

        public void LoadBus()
        {
            bus = RuntimeManager.GetBus("bus:/" + busName);
            if (PlayerPrefs.HasKey(key))
                volume = PlayerPrefs.GetFloat(key);

            slider.normalizedValue = volume;
            bus.setVolume(volume);
        }

        public void SetVolume(float newVolume)
        {
            volume = newVolume;
            bus.setVolume(volume);
            PlayerPrefs.SetFloat(key, volume);
        }
    }
}
