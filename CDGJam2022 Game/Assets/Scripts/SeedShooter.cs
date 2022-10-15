using System;
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
        
        public Transform throwPoint;

        public SeedType[] seeds = new SeedType[3];

        public int seedIndex = 0;

        void Awake()
        {
            input = GetComponent<VirtualController>();
        }

        void OnShoot()
        {
            Rigidbody2D instance = GameObject.Instantiate(seeds[seedIndex].seed.gameObject, input.aimPoint.position, Quaternion.identity).GetComponent<Rigidbody2D>();

            Vector2 throwV = input.aim.normalized * throwStrength;
            instance.velocity = throwV;
        }

        void OnCycleLeft()
        {
            CycleSeed(-1);
        }

        void OnCycleRight()
        {
            CycleSeed(1);
        }

        void CycleSeed(int i)
        {
            seedIndex += i;

            if (seedIndex < 0) seedIndex = seeds.Length - 1;
            else if (seedIndex >= seeds.Length) seedIndex = 0;
        }
    }

    [Serializable]
    public class SeedType
    {
        public string name;
        public Sprite icon;
        public Rigidbody2D seed;
        public int charges;
    }
}
