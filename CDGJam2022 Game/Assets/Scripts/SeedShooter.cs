using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

namespace CDGJam
{
    public class SeedShooter : MonoBehaviour
    {
        public VirtualController input;
        public float throwStrength = 5f;
        public Rigidbody2D seed;
        public Transform throwPoint;

        void Awake()
        {
            input = GetComponent<VirtualController>();
        }

        void OnShoot()
        {
            Rigidbody2D instance = GameObject.Instantiate(seed.gameObject, input.aimPoint.position, Quaternion.identity).GetComponent<Rigidbody2D>();

            Vector2 throwV = input.aim.normalized * throwStrength;
            instance.velocity = throwV;
        }
    }
}
