using UnityEngine;

namespace Minimap
{
    public class MinimapIconController : MonoBehaviour
    {

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
