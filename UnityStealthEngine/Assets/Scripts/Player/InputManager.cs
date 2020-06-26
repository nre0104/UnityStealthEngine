using Assets.Scripts.Enemy;
using Assets.Scripts.Minimap;
using Invector.vCharacterController;
using UnityEngine;

namespace Assets.Scripts.Player
{
    /**
     * Manager of the players input
     */
    public class InputManager : MonoBehaviour
    {
        public GameObject bombObject;
        public GameObject bombSpawnPoint;
        public float throwForce;

        public GameObject PlayerCamera;
        public GameObject DronePrefab;
        public GameObject StonePrefab;
        private GameObject drone;
        public Camera cam;
        public Material StunMaterial;

        void Start()
        {
            MinimapController.Hide();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                ThrowBomb();
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                if (MinimapController.IsActive)
                {
                    MinimapController.Hide();
                }
                else if (!MinimapController.IsActive)
                {
                    MinimapController.Show();
                }
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                if (drone == null)
                {
                    UseDrone();
                }
                else
                {
                    KillDrone();
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ThrowStone();
            }

            if (Input.GetKey(KeyCode.E))
            {
                Ray ray = new Ray(bombSpawnPoint.transform.position, bombSpawnPoint.transform.forward);
                Debug.DrawLine(ray.origin, ray.GetPoint(3f), Color.cyan);
                RaycastHit hit;
                
                if (Physics.Raycast(ray, out hit, 3f))
                {
                    if (hit.collider.GetComponent<InteractableController>() != null)
                    {
                        hit.transform.gameObject.transform.GetComponent<EnemyController>().state = EnemyController.State.Stunned;

                        for (int i = 0; i < hit.transform.childCount; i++)
                        {
                            if (hit.transform.GetChild(i).tag == "Body")
                            {
                                hit.transform.GetChild(i).GetComponent<Renderer>().material = StunMaterial;
                            }
                        }
                    }
                }

            }
        }

        void UseDrone()
        {
            PlayerCamera.SetActive(false);
            drone = Instantiate(DronePrefab, bombSpawnPoint.transform.position, GameManager.player.transform.rotation);
            GameManager.player.GetComponent<vThirdPersonInput>().enabled = false;
        }

        void KillDrone()
        {
            Destroy(drone);
            PlayerCamera.SetActive(true);
            GameManager.player.GetComponent<vThirdPersonInput>().enabled = true;
        }

        void ThrowBomb()
        {
            GameObject bomb = Instantiate(bombObject, bombSpawnPoint.transform.position, Quaternion.identity);
            Rigidbody r = bomb.GetComponent<Rigidbody>();
        
            r.AddForce(bombSpawnPoint.transform.forward * throwForce * 1.05f);
        }

        void ThrowStone()
        {
            GameObject stone = Instantiate(StonePrefab, bombSpawnPoint.transform.position, Quaternion.identity);
            Rigidbody r = stone.GetComponent<Rigidbody>();

            r.AddForce(bombSpawnPoint.transform.forward * throwForce * 1.05f);
        }
    }
}
