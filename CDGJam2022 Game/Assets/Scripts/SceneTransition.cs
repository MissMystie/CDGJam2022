using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDGJam
{
    public class SceneTransition : MonoBehaviour
    {
        public float transitionTime = 20f;
        public string nextLevel = "Tutorial";

        void Start()
        {
            StartCoroutine(Win());
        }

        IEnumerator Win()
        {
            yield return new WaitForSeconds(transitionTime);

            SceneLoader.LoadScene(nextLevel);
        }
    }
}
