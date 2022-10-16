using System.Collections;
using UnityEngine;

namespace CDGJam {

    public class PlantWither : MonoBehaviour {

        private Animator anim;
        [SerializeField] private bool withers = true;
        [SerializeField] private float witherTime;
        [SerializeField] int seedIndex;
        private SeedShooter emitter;

        public GameObject deathPFX;

        public string growAnim = "grow";

        private void Awake()
        {
            anim = GetComponent<Animator>();

            if (withers)
            {
                if (anim) anim.SetTrigger(growAnim);
                StartCoroutine(Wither());
            }
        }

        public void SetEmitter(SeedShooter newEmitter) {
            emitter = newEmitter;
        }

        IEnumerator Wither() {
            yield return new WaitForSeconds(witherTime);
            if(emitter != null) emitter.RechargeSeed(seedIndex);
            if(deathPFX != null) 
                GameObject.Instantiate(deathPFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
