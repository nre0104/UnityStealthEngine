using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPointController : MonoBehaviour
{
    public GameObject winView;

    private void OnTriggerEnter(Collider other)
    {
        Time.timeScale = 0.0f;
        winView.SetActive(true);
    }
}
