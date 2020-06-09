using UnityEngine;

public class HideAreaController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.player)
        {
            other.gameObject.GetComponent<PlayerController>().isHidden = true;

            Debug.Log("Hidden");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameManager.player)
        {
            other.gameObject.GetComponent<PlayerController>().isHidden = false;

            Debug.Log("Visible");
        }
    }
}
