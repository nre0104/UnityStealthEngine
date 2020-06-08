using Invector.vCharacterController;
using UnityEngine;
using Minimap;

public class InputManager : MonoBehaviour
{
    public GameObject bombObject;
    public GameObject spawnPoint;
    public float throwForce;

    public GameObject PlayerCamera;
    public GameObject DronePrefab;
    private GameObject drone;

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
            if (Minimap.MinimapController.IsActive)
            {
                Minimap.MinimapController.Hide();
            } else if (!Minimap.MinimapController.IsActive)
            {
                Minimap.MinimapController.Show();
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
    }

   void UseDrone()
   {
       PlayerCamera.SetActive(false);
       drone = Instantiate(DronePrefab, spawnPoint.transform.position, Quaternion.identity);
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
       GameObject bomb = Instantiate(bombObject, spawnPoint.transform.position, Quaternion.identity);
       Rigidbody r = bomb.GetComponent<Rigidbody>();
       r.AddForce(spawnPoint.transform.forward * throwForce * 1.05f);
   }
}
