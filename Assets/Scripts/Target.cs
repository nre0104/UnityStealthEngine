using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0f)
        {
            GameObject parent = null;

            if (transform.parent != null && transform.parent.name.Contains("_World"))   // Better with tag?
            {
                parent = transform.parent.gameObject;
            }

            Destroy(this.gameObject);

            if (parent != null)
            {
                Destroy(parent.gameObject);
            }
        }
    }
}
