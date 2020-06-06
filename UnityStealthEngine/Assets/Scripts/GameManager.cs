using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        Minimap.Hide();
    }

   void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (Minimap.IsActive)
            {
                Minimap.Hide();
            } else if (!Minimap.IsActive)
            {
                Minimap.Show();
            }
        }
    }
}
