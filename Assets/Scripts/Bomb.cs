using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float delay;
    public float explosionDamage;
    public float explosionRadius;
    public float explosionForce;
    public float upModifier;

    public LayerMask interactionLayer;
    public GameObject explosionEffectPrefab;
    private GameObject explosion;

    void Start()
    {
        Invoke("BombStuff", delay);
    }

    void BombStuff()
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
            
            // Health damage
            Target target = collider.GetComponent<Target>();

            if (target != null)
            {
                target.TakeDamage(explosionDamage);
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
