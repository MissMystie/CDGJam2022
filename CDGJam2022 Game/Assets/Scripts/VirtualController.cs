using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CDGJam
{
    public class VirtualController : MonoBehaviour
    {
        public PlayerInput input;
        private InputAction moveAction;
        private InputAction aimAction;

        public Vector2 move;
        public Vector2 aim;

        public Transform aimPoint;

        public const string keyboardControls = "KeyboardMouse";
        public const string gamepadControls = "Gamepad";

        void Awake()
        {
            input = GetComponent<PlayerInput>();
        }

        private void OnEnable()
        {
            if (input != null)
            {
                move = Vector2.zero;
                aim = Vector2.zero;

                moveAction = input.actions["Move"];
                aimAction = input.actions["Aim"];
            }
        }

        private void OnDisable()
        {
            if (input != null) 
            {
                move = Vector2.zero;
                aim = Vector2.zero;

                moveAction = null;
                aimAction = null;
            }
        }

        void Update()
        {
            if (input != null)
            {
                if (moveAction == null) Debug.Log("NULL");
                move = moveAction.ReadValue<Vector2>();
                aim = GetAim(aimPoint.position);
            }
        }

        public Vector2 GetAim(Vector2 aimPoint)
        {
            if (input.currentControlScheme == keyboardControls)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(aimAction.ReadValue<Vector2>());
                return (mousePos - aimPoint).normalized;
            }
            else
                return aimAction.ReadValue<Vector2>();
        }
    }
}
