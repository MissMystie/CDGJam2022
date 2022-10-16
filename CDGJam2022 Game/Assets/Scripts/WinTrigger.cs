using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDGJam
{
    public class WinTrigger : MonoBehaviour
    {
        public float winTime = 12f;
        public float transitionTime = 1f;

        public Animator anim;
        public string winAnim = "win";
        public string fadeinAnim = "fade in";
        public string nextLevel = "Tutorial";

        public StudioEventEmitter bgm;
        public StudioEventEmitter winFanfare;

        public Animator transition;

        void Start()
        {
            anim = GetComponent<Animator>();
        }

        IEnumerator Win()
        {
            Debug.Log("Entered win trigger");

            UIManager.Instance.Win();

            bgm.Stop();
            winFanfare.Play();
            PlayerController player = LevelManager.Instance.player.GetComponent<PlayerController>();
            player.input.enabled = false;
            player.rb.velocity = Vector2.zero;
            player.anim.SetTrigger(winAnim);
            anim.SetTrigger(winAnim);

            yield return new WaitForSeconds(winTime);

            StartCoroutine(NextLevel());
        }

        public void LoadNextLevel()
        {
            StopAllCoroutines();
            StartCoroutine(NextLevel());
        }

        IEnumerator NextLevel()
        {
            if (transition) transition.SetTrigger(fadeinAnim);

            yield return new WaitForSeconds(transitionTime);

            SceneLoader.LoadScene(nextLevel);
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (ReferenceEquals(col.gameObject, LevelManager.Instance.player.gameObject))
            {
                StartCoroutine(Win());
            }
        }
    }
}
