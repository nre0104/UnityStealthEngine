using UnityEngine;

namespace Assets.Scripts
{
    /**
     * Creates an ping and writes the found GameObjects into the list PingedObjects of the static GameManager
     */
    public class PingController : MonoBehaviour
    {
        public LayerMask PingLayer;
        public float PingRadius;
        public float PingDuration;

        public void Ping()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, PingRadius, PingLayer);

            if (hits != null)
            {
                // Debug.Log("No. of pinged objects: " + hits.Length);

                foreach (var hit in hits)
                {
                    // Debug.Log("Pinged Object - " + hit.transform.name);
                    GameManager.PingedObjects.Add(hit.transform.gameObject);
                }
            }

            Invoke("UnPing", PingDuration);
        }

        /**
         * Clear the list of pinged objects
         */
        public void UnPing()
        {
            GameManager.PingedObjects.Clear();
        }

        /**
         * Remove on e object from the pinged objects list
         */
        public void BlockPingFor(GameObject gameObj)
        {
            if (GameManager.PingedObjects.Contains(gameObj))
            {
                GameManager.PingedObjects.Remove(gameObj);
            }
        }
    }
}