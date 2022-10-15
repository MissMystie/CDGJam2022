using MoreMountains.Feedbacks;
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
        public Rigidbody2D rb { get; private set; }
        public Animator anim { get; private set; }

        [Header("State")]

        public int faceDir = 1;
        public bool isGrounded;
        public bool isClimbing;

        [Header("Movement")]

        public float moveSpeed = 5;
        public float jumpVelocity = 8;

        [Header("Ground Check")]

        public Transform groundCheck;
        public float groundCheckRadius = 0.1f;
        public LayerMask groundLayer;

        [Header("Climbing")]

        public float climbSpeed = 3;
        public bool onClimbableCol;
        public Collider2D climbableCol;
        public string climbableTag = "Climbable";

        [Header("Feedback")]

        public MMFeedbacks jumpFX;
        public MMFeedbacks landFX;

        [Header("Animation")]

        public string groundedAnim = "grounded";
        public string xSpeedAnim = "xSpeed";
        public string ySpeedAnim = "ySpeed";
        public string climbingAnim = "climbing";

        void Awake()
        {
            input = GetComponent<VirtualController>();
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponentInChildren<Animator>();
        }

        void Update()
        {
            Animate();
        }

        private void FixedUpdate()
        {
            Collider2D ground = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

            bool wasGrounded = isGrounded;
            isGrounded = (ground != null);
            if (!wasGrounded && isGrounded) OnGrounded();

            if (isClimbing && !onClimbableCol)
                StopClimbing();
            if (!isClimbing && onClimbableCol && input.move.y > 0)
                StartClimbing();

            if (isClimbing)
                Climbing();
            else
                Movement();
        }

        public void Movement()
        {
            Vector2 v = rb.velocity;

            v.x = input.move.x * moveSpeed;

            rb.velocity = v;
        }

        public void Climbing()
        {
            rb.velocity = new Vector2(0, input.move.y * climbSpeed);
        }

        public void OnJump()
        {
            if (isGrounded || isClimbing)
            {
                if(isClimbing) StopClimbing();
                rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
                jumpFX?.PlayFeedbacks();
            }
        }

        public void OnGrounded()
        {
            if (isClimbing) StopClimbing();
            if (rb.velocity.y < 0)
                landFX?.PlayFeedbacks();
        }

        public void StartClimbing()
        {
            isClimbing = true;
            rb.isKinematic = true;
            transform.position = new Vector2(climbableCol.bounds.center.x, transform.position.y);
        }

        public void StopClimbing()
        {
            isClimbing = false;
            rb.isKinematic = false;
        }

        public void Animate() {
            anim.SetBool(groundedAnim, isGrounded);
            anim.SetBool(climbingAnim, isClimbing);
            anim.SetFloat(xSpeedAnim, rb.velocity.x);
            anim.SetFloat(ySpeedAnim, rb.velocity.y);
        }   

        public void OnTriggerEnter2D(Collider2D col)
        {
            if (!onClimbableCol && col.gameObject.tag == climbableTag)
            {
                onClimbableCol = true;
                climbableCol = col;
            }
        }

        public void OnTriggerExit2D(Collider2D col)
        {
            if(onClimbableCol && col == climbableCol)
            {
                onClimbableCol = false;
                climbableCol = null;
            }
        }

        public void OnDrawGizmos()
        {
            if (groundCheck != null)
            {
                Gizmos.color = isGrounded ? Color.green : Color.red;
                Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
            }
        }
    }
}
