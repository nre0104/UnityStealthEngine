﻿using UnityEngine;

namespace Assets.Scripts.Drone
{
    /**
     * Rotation controller of the drone
     */
    public class DroneCamaraController : MonoBehaviour
    {
        public float mouseSensitivity = 100f;
        public Transform DroneObj;

        float xRotation = 0f;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            DroneObj.Rotate(Vector3.up * mouseX);
        }
    }

}
