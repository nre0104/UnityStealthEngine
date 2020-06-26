using UnityEngine;

namespace Assets.Scripts.Minimap
{
    /**
     * Controls the state of the MiniMap icons
     */
    public class MinimapIconController : MonoBehaviour
    {
        public void Show()
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }

        public void Hide()
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }

        public void HideWithDelay(float delay)
        {
            Invoke("Hide", delay);
        }
    }
}
