using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDGJam
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;
        public Vector2 checkpoint;
        public float respawnTime = 0.5f;

        public StudioEventEmitter deathSFX;

        public GameObject player {get; private set;}

        void Awake()
        {
            //If an instance already exists
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            player = GameObject.FindGameObjectWithTag("Player");

            if (!player)
            {
                Debug.LogWarning("LevelManager: No player found", this);
                return;
            }
        }

        private void Start()
        {
            checkpoint = player.transform.position;
        }

        public void RespawnPlayer()
        {
            StartCoroutine(RespawnCoroutine());
        }

        public IEnumerator RespawnCoroutine()
        {
            deathSFX.Play();
            player.gameObject.SetActive(false); //Disables the player object

            yield return new WaitForSeconds(respawnTime);

            player.transform.position = checkpoint;
            player.gameObject.SetActive(true); //Enables the player object

            Debug.Log("Player respawned!");
        }


        public void ClearLevel() {
            Debug.Log("You win :)");
        }
    }
}
