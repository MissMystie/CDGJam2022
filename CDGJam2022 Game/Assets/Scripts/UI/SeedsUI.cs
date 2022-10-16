using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CDGJam
{
    public class SeedsUI : MonoBehaviour
    {
        private SeedShooter shooter;

        public IconHolder mainIcon;
        public IconHolder leftIcon;
        public IconHolder rightIcon;

        void Awake()
        {
            shooter = LevelManager.Instance.player.GetComponent<SeedShooter>();
        }

        private void OnEnable()
        {
            if (shooter != null) shooter.onSeedUpdate += UpdateUI;
        }

        private void OnDisable()
        {
            if (shooter != null) shooter.onSeedUpdate -= UpdateUI;
        }

        void Start()
        {
            if (shooter == null) 
            else UpdateUI(shooter.seedIndex);
        }

        void UpdateUI(int index)
        {
            mainIcon.SetSeedType(shooter.seeds[index]);
            leftIcon.SetSeedType(shooter.seeds[shooter.GetSeedIndex(index - 1)]);
            rightIcon.SetSeedType(shooter.seeds[shooter.GetSeedIndex(index + 1)]);
        }

        [Serializable]
        public class IconHolder 
        {
            public Image img;
            private SeedType seed;

            public void SetSeedType(SeedType type)
            {
                seed = type;
                img.sprite = seed.icons[seed.charges];
            }
        }
    }
}
