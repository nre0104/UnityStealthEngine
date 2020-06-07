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
            //Ping();
        }

        public void Ping()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, PingRadius, PingLayer);     // TODO: Without any layer at all it works but with any layer it doesnt't

            Debug.Log(hits.Length);
            Debug.Log(PingLayer);

            if (hits != null)
            {
                foreach (var hit in hits)
                {
                    Debug.Log("Pinged - " + hit.transform.name);
                    GameManager.PingedObjects.Add(transform.gameObject);
                }
            }

            Invoke("UnPing", PingDuration + 0.001f);
        }

        public void UnPing()
        {
            GameManager.PingedObjects.Clear();
        }
    }
}