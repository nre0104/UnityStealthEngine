using UnityEngine;

namespace Assets.Scripts.Enemy
{
    /**
     * Defines the radius in which the object is Interactable
     */
    public class InteractableController : MonoBehaviour
    {
        /**
         * Radius in which the Enemy is interactable
         */
        public float radius = 3f;

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position,radius);
        }
    }
}
