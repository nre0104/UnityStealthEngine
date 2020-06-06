using UnityEngine;
using Minimap;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        Minimap.Minimap.Hide();
    }

   void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (Minimap.Minimap.IsActive)
            {
                Minimap.Minimap.Hide();
            } else if (!Minimap.Minimap.IsActive)
            {
                Minimap.Minimap.Show();
            }
        }
    }
}
