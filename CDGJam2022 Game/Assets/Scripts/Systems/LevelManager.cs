using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDGJam
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;

        public GameObject player;
        public SeedShooter shooter;

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
            player.GetComponent<SeedShooter>();

            if (!player)
            {
                Debug.LogWarning("GameManager: No player found", this);
                return;
            }

        }
    }
}
