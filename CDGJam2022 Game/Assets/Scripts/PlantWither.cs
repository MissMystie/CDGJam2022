﻿using System.Collections;
using UnityEngine;

namespace CDGJam {

    public class PlantWither : MonoBehaviour {

        [SerializeField] private bool withers = true;
        [SerializeField] private float witherTime;
        [SerializeField] int seedIndex;
        private SeedShooter emitter;

        public void SetEmitter(SeedShooter newEmitter) {
            emitter = newEmitter;
        }

        private void Awake() {
            if(withers) StartCoroutine(Wither());
        }

        IEnumerator Wither() {
            yield return new WaitForSeconds(witherTime);
            if(emitter != null) emitter.RechargeSeed(seedIndex);
            Destroy(gameObject);
        }
    }
}
