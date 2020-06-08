﻿using UnityEngine;

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
            Debug.Log(hits.Length);

            if (hits != null)
            {
                foreach (var hit in hits)
                {
                    Debug.Log("Pinged - " + hit.transform.name);
                    GameManager.PingedObjects.Add(transform.gameObject);
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