using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace CDGJam
{
    [RequireComponent(typeof(Collider2D))]
    public class TextPanel : MonoBehaviour
    {
        [SerializeField] TMP_Text _textDisplay;
        [SerializeField] float _timeBetweenCharacters;
        [SerializeField, TextArea] string _text;
        bool active;

        private void Awake() {
            active = false;
            _textDisplay.gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if(other.gameObject.TryGetComponent<PlayerController>(out var player)) {
                active = true;
                _textDisplay.gameObject.SetActive(true);
                StartCoroutine(DisplayText());
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if(active && other.TryGetComponent<PlayerController>(out var player)) {
                _textDisplay.text = string.Empty;
                _textDisplay.gameObject.SetActive(false);
                active = false;
            }
        }

        IEnumerator<WaitForSeconds> DisplayText() {
            int substringIndex = 0;

            while (substringIndex < _text.Length) {
                if (!active) {
                    yield break;
                }

                _textDisplay.text = _text.Substring(0, ++substringIndex);
                yield return new WaitForSeconds(_timeBetweenCharacters);
            }
        }
    }
}
