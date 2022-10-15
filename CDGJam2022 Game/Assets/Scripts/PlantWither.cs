using System.Collections;
using UnityEngine;

namespace CDGJam {

    public class PlantWither : MonoBehaviour {

        [SerializeField] float _witherTime;
        [SerializeField] int _seedIndex;
        SeedShooter _shotFrom;

        public void PassParent(SeedShooter shotFrom, int index) {
            _shotFrom = shotFrom;
            _seedIndex = index;
        }

        private void Awake() {
            StartCoroutine(Wither());
        }

        IEnumerator Wither() {
            yield return new WaitForSeconds(_witherTime);
            Destroy(this.gameObject);
        }

        private void OnDestroy() {
            _shotFrom.RechargeSeed(_seedIndex);
        }
    }
}
