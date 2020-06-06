using System.Collections;
using System.Collections.Generic;
using Minimap;
using UnityEngine;

namespace Ping
{
    public class PingHandler : MonoBehaviour
    {
        private readonly List<Transform> ActiveObjects = new List<Transform>();
        public LayerMask PingLayer;
        public float PingRadius;
        public float PingDuration;

        private void Start()
        {
            Invoke("Ping", 3f);
            Invoke("Hide", PingDuration);
        }

        public void Ping()
        {
            Collider[] hitCollider = Physics.OverlapSphere(transform.position, PingRadius, PingLayer);

            foreach (Collider collider in hitCollider)
            {
                if (collider.gameObject.GetComponent<Collider>() != false)
                {
                    List<Transform> hittedObjects = new List<Transform>();

                    for (int i = 0; i < collider.gameObject.transform.childCount; i++)
                    {
                        hittedObjects.Add(collider.gameObject.transform.GetChild(i));
                    }

                    foreach (Transform hittedTransform in hittedObjects)
                    {
                        MinimapIcon minimapIcon = hittedTransform.Find("MinimapIcon").GetComponent<MinimapIcon>();

                        if (hittedTransform.GetComponent<SpriteRenderer>() != null)

                        {
                            minimapIcon.Show();
                            ActiveObjects.Add(hittedTransform);
                        }
                    }
                }
            }
        }

        public void Hide()
        {
            foreach (Transform activeTransform in ActiveObjects)
            {
                activeTransform.gameObject.SetActive(false);
            }
        }
    }
}