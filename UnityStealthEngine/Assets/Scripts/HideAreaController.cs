using System;
using UnityEngine;

public class HideAreaController : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        try
        {
            if (other.gameObject == GameManager.player)
            {
                if (gameObject.GetComponent<Collider>().bounds.Contains(GameManager.player.gameObject.GetComponent<Collider>().bounds.center))
                {
                    other.gameObject.GetComponent<PlayerController>().isHidden = true;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    void OnTriggerExit(Collider other)
    {
        try
        {
            if (other.gameObject == GameManager.player)
            {
                other.gameObject.GetComponent<PlayerController>().isHidden = false;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
