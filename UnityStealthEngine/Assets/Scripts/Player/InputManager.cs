﻿using System.Collections.Generic;
using Invector.vCharacterController;
using UnityEngine;
using Minimap;
using UnityEngine.AI;
using UnityEngineInternal;

public class InputManager : MonoBehaviour
{
    public GameObject bombObject;
    public GameObject spawnPoint;
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
            if (Minimap.MinimapController.IsActive)
            {
                Minimap.MinimapController.Hide();
            }
            else if (!Minimap.MinimapController.IsActive)
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

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ThrowStone();
        }

        if (Input.GetKey(KeyCode.E))
        {
            Ray ray = new Ray(spawnPoint.transform.position, spawnPoint.transform.forward);
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
        drone = Instantiate(DronePrefab, spawnPoint.transform.position, GameManager.player.transform.rotation);
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

    void ThrowStone()
    {
        GameObject stone = Instantiate(StonePrefab, spawnPoint.transform.position, Quaternion.identity);
        Rigidbody r = stone.GetComponent<Rigidbody>();

        r.AddForce(spawnPoint.transform.forward * throwForce * 1.05f);
    }
}
