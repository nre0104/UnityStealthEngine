using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Ping
{
    // TODO: Use custom Event to transport parameters (List of Pinged Objects)
    public class PingController : MonoBehaviour
    {
        public readonly List<GameObject> pingedObjects = new List<GameObject>();
        public LayerMask PingLayer;
        public float PingRadius;
        public float PingDuration;
        public UnityEvent OnPing;
        public UnityEvent OnUnPing;

        private void Start()
        {
            Ping();
            Invoke("UnPing", PingDuration);
        }

        public void Ping()
        {
            var hits = Physics.SphereCastAll(transform.position, PingRadius, Vector3.forward, PingRadius, PingLayer);
            
            if (hits != null)
            {
                foreach (var hit in hits)
                {
                    Debug.Log("Pinged - " + hit.transform.name);
                    pingedObjects.Add(transform.gameObject);
                }
            }

            OnPing.Invoke();
        }

        public void UnPing()
        {
            OnUnPing.Invoke();
        }
    }
}