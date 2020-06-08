using UnityEngine;

namespace Ping
{
    public class PingController : MonoBehaviour
    {
        public LayerMask PingLayer;
        public float PingRadius;
        public float PingDuration;

        private void Start()
        { 
            // Ping();
            // Invoke("Ping", 3f);
        }

        public void Ping()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, PingRadius, PingLayer);

            if (hits != null)
            {
                Debug.Log("No. of pinged objects: " + hits.Length);

                foreach (var hit in hits)
                {
                    Debug.Log("Pinged Object - " + hit.transform.name);
                    GameManager.PingedObjects.Add(hit.transform.gameObject);
                }
            }

            Invoke("UnPing", PingDuration);
        }

        public void UnPing()
        {
            GameManager.PingedObjects.Clear();
        }
    }
}