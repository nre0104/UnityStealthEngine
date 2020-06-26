using UnityEngine;
using Invector;
using Invector.vCharacterController;
using UnityEditor;

public class PlayerController : MonoBehaviour
{
    public bool isHidden = false;
    public bool isSprinting;

    void Update()
    {
        isSprinting = GetComponent<vThirdPersonController>().isSprinting;
    }
}
