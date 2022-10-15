using UnityEngine;

namespace CDGJam.Assets.Scripts
{
    public class BouncePlatform : MonoBehaviour
    {

        [SerializeField] private LayerMask mask;
        [SerializeField] float _bounceFactor = 1.00f;
        [SerializeField] float _bounceForceMax = 25f;
        [SerializeField] float _bounceForceMin = 10f;

        private void OnTriggerEnter2D(Collider2D col)
        {

            if (col.gameObject.IsInLayerMask(mask))
            {
                Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();

                float bounceStrength = Mathf.Abs(rb.velocity.magnitude * _bounceFactor);

                // Clamp.
                if (bounceStrength > _bounceForceMax)
                {
                    bounceStrength = _bounceForceMax;
                }
                else if (bounceStrength < _bounceForceMin)
                {
                    bounceStrength = _bounceForceMin;
                }

                Debug.Log(transform.up);
                rb.velocity = transform.up * bounceStrength;
                //player.rb.velocity = new Vector2(player.rb.velocity.x, riseVelocity);
            }

        }
    }
}
