using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPointController : MonoBehaviour
{
    public GameObject winView;
    public LayerMask playerLayer;

    private void OnTriggerEnter(Collider other)
    {
        if ((playerLayer & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
        {
            Time.timeScale = 0.0f;
            winView.SetActive(true);
        }
    }
}
