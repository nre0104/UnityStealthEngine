using System.Collections.Generic;
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

        public void ShowAllIcons(List<GameObject> list)
        {
            foreach (var gameObj in list)
            {
                gameObj.transform.GetComponentInChildren<MinimapIconController>().Show();
            }
        }

        public void HideAllIcons(List<GameObject> list)
        {
            foreach (var gameObj in list)
            {
                gameObj.transform.GetComponentInChildren<MinimapIconController>().Hide();
            }
        }

        public void HideWithDelay(float delay)
        {
            Invoke("Hide", delay);
        }
    }
}
