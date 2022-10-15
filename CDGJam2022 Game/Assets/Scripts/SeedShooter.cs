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
            if(seeds[seedIndex].charges <= 0) {
                // text pop-up?
                return;
            }

            Seed instance = GameObject.Instantiate(seeds[seedIndex].seed.gameObject, input.aimPoint.position, Quaternion.identity).GetComponent<Seed>();
            instance.SetEmitter(this);
            seeds[seedIndex].charges--;

            Vector2 throwV = input.aim.normalized * throwStrength;
            instance.rb.velocity = throwV;
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


        public void RechargeSeed(int seedIndex) {
            seeds[seedIndex].charges++;
        }
    }

    [Serializable]
    public class SeedType
    {
        public string name;
        public Sprite icon;
        public Seed seed;
        public int charges;
    }
}