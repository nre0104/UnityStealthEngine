using UnityEngine;

namespace Minimap
{
    public class MinimapIcon : MonoBehaviour
    {

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void HideWithDelay(float delay)
        {
            Invoke("Hide", delay);
        }
    }
}
