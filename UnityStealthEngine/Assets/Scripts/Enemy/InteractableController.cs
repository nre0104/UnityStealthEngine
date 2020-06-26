using UnityEngine;

/**
 * Defines the radius in which the object is interactable
 */
public class InteractableController : MonoBehaviour
{
    public float radius = 3f;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,radius);
    }
}
