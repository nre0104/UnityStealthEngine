using UnityEngine;

public class HideAreaController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.player)
        {
            other.gameObject.GetComponent<PlayerController>().isHidden = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameManager.player)
        {
            other.gameObject.GetComponent<PlayerController>().isHidden = false;
        }
    }
}
