using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CDGJam
{
    [RequireComponent(typeof(Collider2D))]
    public class TriggerCollider2D : MonoBehaviour
    {
        [SerializeField] UnityEvent _onEnter;
        [SerializeField] UnityEvent _onExit;

        private void OnTriggerEnter2D(Collider2D collision) {
            _onEnter.Invoke();
        }

        private void OnTriggerExit(Collider other) {
            _onExit.Invoke();
        }
    }
}
