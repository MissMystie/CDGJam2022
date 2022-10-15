using UnityEngine;

namespace CDGJam.Assets.Scripts {
    public class BouncePlatform : MonoBehaviour {

        [SerializeField] float _bounceFactor = 1.00f;
        [SerializeField] float _bounceForceMax = 25f;
        [SerializeField] float _bounceForceMin = 10f;

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController player)) {
                float riseVelocity = Mathf.Abs(player.rb.velocity.y * _bounceFactor);
                
                // Clamp.
                if(riseVelocity > _bounceForceMax) {
                    riseVelocity = _bounceForceMax;
                }
                else if(riseVelocity < _bounceForceMin) {
                    riseVelocity = _bounceForceMin;
                }
                
                player.rb.velocity = new Vector2(player.rb.velocity.x, riseVelocity);
            }
        }
    }
}
