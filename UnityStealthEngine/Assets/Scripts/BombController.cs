using UnityEngine;

namespace Assets.Scripts
{
    public class BombController : MonoBehaviour
    {
        public float delay;
        public float explosionRadius;
        public float explosionForce;
        public float upModifier;
        public LayerMask interactionLayer;
        public GameObject explosionEffectPrefab;
        private GameObject explosion;

        void Start()
        {
            Invoke("Bomb", delay);
        }

        void Bomb()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius, interactionLayer);

            foreach (Collider collider in hitColliders)
            {
                // Change physical position in scene
                Rigidbody rigidbody = collider.GetComponent<Rigidbody>();

                if (rigidbody != null)
                {
                    rigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius, upModifier);
                }
            }

            gameObject.GetComponent<AudioSource>().Play();
            explosion = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);

            gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;

            Invoke("Explode", 3f);
        }

        void Explode()
        {
            Destroy(explosion);
            Destroy(gameObject);
        }
    }
}