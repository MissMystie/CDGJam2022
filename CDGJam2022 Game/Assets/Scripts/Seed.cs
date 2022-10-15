using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

namespace CDGJam
{
    public class Seed : MonoBehaviour
    {
        public const string growableTag = "Growable";

        public GameObject breakPFX;
        public PlantWither plant;
        public SeedShooter _shotFrom;
        public int seedIndex;

        public void PassParent(SeedShooter shooter, int seedIndex) {
            _shotFrom = shooter;
            this.seedIndex = seedIndex;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if(col.gameObject.tag == growableTag)
            {
                PlantWither newPlant = Instantiate(plant, transform.position, Quaternion.identity).GetComponent<PlantWither>();
                newPlant.PassParent(_shotFrom, seedIndex);  
            }
            else 
            {
                GameObject.Instantiate(breakPFX, transform.position, Quaternion.identity);
                _shotFrom.RechargeSeed(seedIndex);
            }

            Destroy(gameObject);
        }
    }
}
