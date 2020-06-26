using Invector.vCharacterController;
using UnityEngine;

namespace Assets.Scripts.Player
{
    /**
     * Provides the states of the player object
     */
    public class PlayerController : MonoBehaviour
    {
        public bool isHidden = false;
        public bool isSprinting;

        void Update()
        {
            isSprinting = GetComponent<vThirdPersonController>().isSprinting;
        }
    }
}
