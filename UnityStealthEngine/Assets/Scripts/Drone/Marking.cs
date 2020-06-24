using UnityEngine;
using UnityEngineInternal;

namespace Drone
{
    public class Marking : MonoBehaviour
    {
        public Camera camera;
        public float markingRang = 7f;
        public Material visibleMaterial;
        public LayerMask layer;

        void Update()
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Mark();
            }
        }

        public void Mark()
        {
            Ray ray = new Ray(camera.transform.position, camera.transform.forward);
            Debug.DrawLine(ray.origin, ray.GetPoint(markingRang), Color.red);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, markingRang, layer))
            {
                if (hit.transform.gameObject.GetComponentsInChildren<Renderer>() != null)
                {
                    Debug.Log(hit.transform.gameObject.name);
                    GameObject obj = hit.transform.gameObject;
                    for (int i = 0; i < obj.transform.childCount; i++)
                    {
                        if (obj.transform.GetChild(i).tag == "Body")
                        {
                            GameManager.OldEnemiesMaterials.Enqueue(obj.transform.GetChild(i).GetComponent<Renderer>().material);
                            obj.transform.GetChild(i).GetComponent<Renderer>().material = visibleMaterial;
                        }
                    }
                    GameManager.MarkedEnemies.Add(obj);
                }
                else
                {
                    Debug.Log("Renderer is missing on this object");
                }
            }
        }

        public void UnMark()
        {
            foreach (var markedEnemy in GameManager.MarkedEnemies)
            {
                for (int i = 0; i < markedEnemy.transform.childCount; i++)
                {
                    if (markedEnemy.transform.GetChild(i).tag == "Body")
                    {
                        markedEnemy.transform.GetChild(i).GetComponent<Renderer>().material = GameManager.OldEnemiesMaterials.Dequeue();
                    }
                }
            }
        }
    }
}
