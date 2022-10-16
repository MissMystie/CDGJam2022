using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

namespace CDGJam
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Seed : MonoBehaviour
    {
        public const string growableTag = "Growable";

        public Rigidbody2D rb;

        public GameObject breakPFX;
        public SeedShooter emitter;
        public int seedIndex;
        public bool rotateToTerrain;

        [Space]

        public GameObject plantGround;
        public GameObject plantWall;
        public GameObject plantCeil;

        public void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void SetEmitter(SeedShooter newEmitter)
        {
            emitter = newEmitter;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if(col.gameObject.tag == growableTag)
            {
                Vector2 impactPoint = col.collider.ClosestPoint(transform.position);
                float angle = Vector2.Angle(Vector2.up, (Vector2)transform.position - impactPoint);

                Debug.Log("Angle: " + angle);

                GameObject plant;
                if (angle < 45) plant = plantGround;
                else if (angle < 135) plant = plantWall;
                else plant = plantCeil;

                if (plant != null)
                {
                    PlantWither newPlant = GameObject.Instantiate(plant, impactPoint, rotateToTerrain ? Quaternion.Euler(0, 0, -angle) : Quaternion.identity).GetComponent<PlantWither>();
                    if(newPlant != null) newPlant.SetEmitter(emitter);
                }
                else
                {
                    GameObject.Instantiate(breakPFX, transform.position, Quaternion.identity);
                    emitter.RechargeSeed(seedIndex);
                }

            }
            else 
            {
                GameObject.Instantiate(breakPFX, transform.position, Quaternion.identity);
                emitter.RechargeSeed(seedIndex);
            }

            Destroy(gameObject);
        }
    }
}
