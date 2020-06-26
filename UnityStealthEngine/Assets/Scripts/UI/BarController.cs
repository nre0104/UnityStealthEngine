using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class BarController : MonoBehaviour
    {
        public Slider slider;

        public void SetSlide(int value)
        {
            slider.value = value;
        }

        public void SetMax(int value)
        {
            slider.maxValue = value;
        }

        public void SetMin(int value)
        {
            slider.minValue = value;
        }

        public void IncreaseByOne()
        {
            slider.value += 1;
        }

        public void DecreaseByOne()
        {
            slider.value -= 1;
        }
    }
}
