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

        public Transform aimArm;

        public float cdTimer = 0f;
        public float cdTime = 0.5f;

        void Awake()
        {
            input = GetComponent<VirtualController>();
        }

        private void Update()
        {
            Vector3 rot = new Vector3(0, 0, Vector2.SignedAngle(Vector2.right, input.aim));
            aimArm.transform.eulerAngles = rot;
        }

        void OnShoot()
        {
            if(seeds[seedIndex].charges <= 0) {
                // text pop-up?
                return;
            }

            Seed instance = GameObject.Instantiate(seeds[seedIndex].seed.gameObject, input.aimPoint.position, Quaternion.identity).GetComponent<Seed>();
            instance.SetEmitter(this);
            
            Vector2 throwV = input.aim.normalized * throwStrength;
            instance.rb.velocity = throwV;

            seeds[seedIndex].charges--;

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

        public void RechargeSeed(int index) {
            seeds[index].charges++;
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