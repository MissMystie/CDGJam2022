using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.EventSystems.EventTrigger;

namespace CDGJam
{
    public class PlayerController : MonoBehaviour
    {
        public VirtualController input;
        public Rigidbody2D rb;

        public float moveSpeed = 5;
        public float jumpVelocity = 8;

        void Awake()
        {
            input = GetComponent<VirtualController>();
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            Vector2 v = rb.velocity;

            v.x = input.move.x * moveSpeed;

            rb.velocity = v;
        }

        public void OnJump()
        {
            Debug.Log("On Jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
        }
    }

}
