using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

namespace CDGJam
{
    public class SeedShooter : MonoBehaviour
    {
        public Action<int> onSeedUpdate;

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

            onSeedUpdate?.Invoke(seedIndex);
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
            seedIndex = GetSeedIndex(seedIndex + i);
            onSeedUpdate?.Invoke(seedIndex);
        }

        public int GetSeedIndex(int index)
        {
            if (index < 0) index = seeds.Length - 1;
            else if (index >= seeds.Length) index = 0;
            
            return index;
        }

        public void RechargeSeed(int seedIndex) {
            seeds[seedIndex].charges++;
            onSeedUpdate?.Invoke(seedIndex);
        }
    }

    [Serializable]
    public class SeedType
    {
        public string name;
        public Sprite[] icons = new Sprite[4];
        public Seed seed;
        public int charges;
    }
}