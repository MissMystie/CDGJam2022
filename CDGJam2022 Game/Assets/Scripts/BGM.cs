using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDGJam
{
    public class BGM : MonoBehaviour
    {
        public bool isPlaying = false;
        public StudioEventEmitter bgm;

        public static BGM Instance;

        void Awake()
        {
            //If an instance already exists
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void Play() { 
            isPlaying = true;
        }

        public void Stop()
        {
            isPlaying = false;
        }
    }
}
