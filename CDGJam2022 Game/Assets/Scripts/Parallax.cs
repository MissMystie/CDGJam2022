using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDGJam
{
    public class Parallax : MonoBehaviour
    {
        public Camera cam;
        public Transform anchor;
        public Vector2 offset;

        private Vector2 screenBounds;
        private Vector3 lastScreenPos;

        public Layer[] layers;

        void Start()
        {
            if (!cam) cam = FindObjectOfType<Camera>();

            screenBounds = new Vector2(16, 9);

            Vector3 pos = cam.transform.position + (Vector3)offset;
            pos.z = anchor.transform.position.z;
            //anchor.transform.position = pos;

            lastScreenPos = cam.transform.position;
        }

        void Reset()
        {
            cam = Camera.main;
            anchor = transform;
        }

        void LateUpdate()
        {
            foreach (Layer layer in layers)
            {
                //float spd = 1 - Mathf.Clamp01(Mathf.Abs(transform.position.z / layer.obj.position.z));
                float distX = (cam.transform.position.x - lastScreenPos.x) * (1 - layer.speedX);
                float distY = (cam.transform.position.y - lastScreenPos.y) * (1 - layer.speedY);
                Vector2 dist = new Vector2(distX, distY);

                layer.obj.Translate(dist);

                //layer.obj.Translate(Vector3.right * dist * (1 - layer.speed));
            }

            lastScreenPos = cam.transform.position;
        }

        [Serializable]
        public class Layer
        {
            public Transform obj;

            private float width;
            private float halfWidth;

            private float height;
            private float halfHeight;

            public bool horizontal = true;
            public float choke = 0.25f;
            public float speedX = 0.4f;
            public float speedY = 0;
        }
    }
}
