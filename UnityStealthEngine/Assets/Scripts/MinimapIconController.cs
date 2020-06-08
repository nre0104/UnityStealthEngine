using System.Collections.Generic;
using UnityEngine;

namespace Minimap
{
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
