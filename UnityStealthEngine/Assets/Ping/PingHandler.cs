using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Ping
{
    /**
     * Ping with event only usable with custom PingEvents of custom namespace Ping
     */
    public class PingHandler : MonoBehaviour
    {
        private readonly List<GameObject> pingedObjects = new List<GameObject>();
        public LayerMask PingLayer;
        public float PingRadius;
        public float PingDuration;
        [Tooltip("Event for all pinged GameObjects. Must inherit from Ping.PingEvent")]
        public PingEvent OnPing;
        [Tooltip("Event for all pinged GameObjects. Must inherit from Ping.PingEvent")]
        public PingEvent OnUnPing;

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

            OnPing.Invoke(pingedObjects);
        }

        public void UnPing()
        {
            OnUnPing.Invoke(pingedObjects);
        }
    }

    [System.Serializable]
    public class PingEvent : UnityEvent<List<GameObject>>
    {

    }
}