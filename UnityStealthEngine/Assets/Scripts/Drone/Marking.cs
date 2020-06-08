using UnityEngine;

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
                GameObject obj = hit.transform.gameObject;
                GameManager.OldEnemiesMaterials.Enqueue(obj.GetComponent<Renderer>().material);

                obj.GetComponent<Renderer>().material = visibleMaterial;
                GameManager.MarkedEnemies.Add(obj);
            }
        }

        public void UnMark()
        {
            foreach (var markedEnemy in GameManager.MarkedEnemies)
            {
                markedEnemy.gameObject.GetComponent<Renderer>().material = GameManager.OldEnemiesMaterials.Dequeue();
            }
        }
    }
}

