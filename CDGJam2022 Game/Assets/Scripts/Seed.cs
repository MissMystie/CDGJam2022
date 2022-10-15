using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

namespace CDGJam
{
    public class Seed : MonoBehaviour
    {
        public string growableTag = "Growable";

        public GameObject plant;

        private void OnCollisionEnter2D(Collision2D col)
        {
            if(col.gameObject.tag == growableTag)
            {
                GameObject instance = GameObject.Instantiate(plant, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
