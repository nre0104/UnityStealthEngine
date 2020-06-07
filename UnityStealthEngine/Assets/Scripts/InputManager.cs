using UnityEngine;
using Minimap;

public class InputManager : MonoBehaviour
{
    public GameObject bombObject;
    public GameObject spawnPoint;
    public float throwForce;

    void Start()
    {
        Minimap.MinimapController.Hide();
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
    }
    
   void ThrowBomb()
   {
       GameObject bomb = Instantiate(bombObject, spawnPoint.transform.position, Quaternion.identity);
       Rigidbody r = bomb.GetComponent<Rigidbody>();
       r.AddForce(spawnPoint.transform.forward * throwForce * 1.05f);
   }
}
