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
        public string growableTag = "Growable";

        public GameObject breakPFX;

        public GameObject plantGround;
        public GameObject plantWall;
        public GameObject plantCeil;

        public bool rotateToTerrain;

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

                GameObject instance = GameObject.Instantiate(plant, impactPoint, rotateToTerrain? Quaternion.Euler(0,0,angle) : Quaternion.identity);
                
            }
            else
            {
                GameObject.Instantiate(breakPFX, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
