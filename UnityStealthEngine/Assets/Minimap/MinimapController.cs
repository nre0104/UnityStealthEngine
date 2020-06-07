using System.Collections.Generic;
using UnityEngine;

namespace Minimap 
{
    public class MinimapController : MonoBehaviour
    {
        private static MinimapController instance;
        public static bool IsActive;

        void Awake()
        {
            instance = this;
            if (instance.gameObject.activeSelf) IsActive = true;
        }

        public static void Show()
        {
            instance.gameObject.SetActive(true);
            IsActive = true;
        }

        public static void Hide()
        {
            instance.gameObject.SetActive(false);
            IsActive = false;
        }
    }
}