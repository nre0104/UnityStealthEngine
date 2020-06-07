using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class BombBehaviourController : MonoBehaviour
    {
        [System.Serializable]
        public class BombEvent : UnityEvent<GameManager> { }

        public float delay;
        public float explosionRadius;
        public float explosionForce;
        public float upModifier;
        public LayerMask interactionLayer;
        public GameObject explosionEffectPrefab;
        private GameObject explosion;

        public BombEvent OnExplode;
        private float destroyObjDelay = 3f;

        void Start()
        {
            Invoke("Explode", delay);
        }

        void Explode()
        {
            OnExplode.Invoke(null);

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

            explosion = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);

            gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;

            Invoke("DestroyObjects", destroyObjDelay);
        }

        void DestroyObjects()
        {
            Destroy(explosion);
            Destroy(gameObject);
        }

        public void increaseDestroyDelay(float delay)
        {
            destroyObjDelay += delay;
        }
    }
}