using UnityEngine;

namespace Assets.Scripts.Drone
{
    /**
     * WASD-Controller of the drone
     */
    public class DroneMovementController : MonoBehaviour
    {
        public CharacterController controller;
        public float droneSpeed = 12f;

        void Update()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * droneSpeed * Time.deltaTime);
        }
    }

}
