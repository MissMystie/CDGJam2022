using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

namespace CDGJam
{
    public class Seed : MonoBehaviour
    {
        public string growableTag = "Growable";

        public GameObject breakPFX;

        public GameObject plant;

        private void OnCollisionEnter2D(Collision2D col)
        {
            if(col.gameObject.tag == growableTag)
            {
                GameObject.Instantiate(plant, transform.position, Quaternion.identity);
            }
            else
            {
                GameObject.Instantiate(breakPFX, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
