using FMODUnity;
using MoreMountains.Feedbacks;
using System;
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
        public float friction = 0.175f;
        public float drag = 0.075f;
        public float acc = 0.1f;
        public float airAcc = 0.05f;
        public float minVelocity = 0.1f;

        [Header("Ground Check")]

        public Transform groundCheck;
        public float groundCheckRadius = 0.1f;
        public LayerMask groundLayer;

        [Header("Climbing")]

        public float climbSpeed = 3;
        public bool onClimbableCol;
        public Collider2D climbableCol;
        public string climbableTag = "Climbable";

        private float gravityScale;

        [Header("Feedback")]

        public MMFeedbacks jumpFX;
        public MMFeedbacks landFX;

        [Header("Animation")]

        public string groundedAnim = "grounded";
        public string xSpeedAnim = "xSpeed";
        public string ySpeedAnim = "ySpeed";
        public string climbingAnim = "climbing";
        public ParticleSystem walkingPFX;

        [Header("SFX")]

        public StudioEventEmitter walkingSFX;
        public StudioEventEmitter jumpSFX;
        public StudioEventEmitter climbingSFX;

        void Awake()
        {
            input = GetComponent<VirtualController>();
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponentInChildren<Animator>();

            gravityScale = rb.gravityScale;
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

            if (!isGrounded || (rb.velocity.x < minVelocity && rb.velocity.x > -minVelocity))
            {
                walkingSFX.Stop();
                if (walkingPFX.isPlaying) walkingPFX.Stop();
            }
            else if (!walkingPFX.isPlaying)
            {
                walkingSFX.Play();
                walkingPFX.Play();
            }

            if (isClimbing && !onClimbableCol)
                StopClimbing();
            if (!isClimbing && onClimbableCol && input.move.y > 0)
                StartClimbing();

            if (isClimbing)
                Climbing();
            else
                Movement(Time.fixedDeltaTime);
        }

        public void Movement(float deltaTime)
        {
            Vector2 v = rb.velocity;

            if (input.move.x != 0)
            {
                //v.x = input.move.x * moveSpeed;

                v.x += input.move.x * moveSpeed * (isGrounded? acc : airAcc) * deltaTime;
                v.x = Mathf.Clamp(v.x, -moveSpeed, moveSpeed);
            }
            //Apply friction
            else 
            {
                v.x *= (1 - ((isGrounded? friction : drag) * deltaTime));
                v.x = Mathf.MoveTowards(v.x, 0f, minVelocity);
            }

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
                jumpSFX.Play();
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
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
            //transform.position = new Vector2(climbableCol.bounds.center.x, transform.position.y);
            climbingSFX.Play();
        }

        public void StopClimbing()
        {
            isClimbing = false;
            rb.gravityScale = gravityScale;
            climbingSFX.Stop();
        }

        public void Animate() {

            if (input.move.x != 0) faceDir = Math.Sign(input.move.x);

            Vector3 scale = anim.transform.localScale;
            scale.x = faceDir;
            anim.transform.localScale = scale;

            anim.SetBool(groundedAnim, isGrounded);
            anim.SetBool(climbingAnim, isClimbing);
            anim.SetFloat(xSpeedAnim, Mathf.Abs(rb.velocity.x));
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
