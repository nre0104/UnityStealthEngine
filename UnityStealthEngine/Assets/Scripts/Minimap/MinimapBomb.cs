using UnityEngine;

namespace Assets.Scripts.Minimap
{
    /**
     * Implementations to create a Ping-bomb out of the ping an a basic bomb
     */
    public class MinimapBomb : MonoBehaviour
    {
        void ShowIconsInRange()
        {
            GameManager.Instance.ShowAllMinimapIcons(GameManager.PingedObjects);
        }

        void HideIconsInRange()
        {
            GameManager.Instance.HideAllMinimapIcons(GameManager.PingedObjects);
        }

        public void MinimapPing()
        {
            ShowIconsInRange();
            var pingDuration = transform.gameObject.GetComponent<PingController>().PingDuration - 0.05f;

            transform.gameObject.GetComponent<BombBehaviourController>().IncreaseDestroyDelay(pingDuration);
            Invoke("HideIconsInRange", pingDuration);
        }
    }
}
