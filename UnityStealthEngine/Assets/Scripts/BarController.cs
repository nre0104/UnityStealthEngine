using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        Debug.Log("+");
        slider.value += 1;
    }

    public void DecreaseByOne()
    {
        Debug.Log("-");
        slider.value -= 1;
    }
}
