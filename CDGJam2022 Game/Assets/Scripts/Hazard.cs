using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDGJam
{
    [RequireComponent(typeof(Collider2D))]
    public class Hazard : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject == LevelManager.Instance.player)
            {
                LevelManager.Instance.RespawnPlayer();
            }
        }
    }
}

