using System;
using System.Collections;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public BarController SeeBar;
    public BarController HearBar;

    public GameObject lostView;

    private int sliderValue = 0;
    private int oldSliderValue = 0;

    void Start()
    {
        SeeBar.SetMax(15);
        SeeBar.SetSlide(0);
        
        HearBar.SetMax(15);
        HearBar.SetSlide(0);

        GameManager.SeeBar = SeeBar;
        GameManager.HearBar = HearBar;

        StartCoroutine("DecreaseValue", 0.25f);
    }

    void Update()
    {
        if (Math.Abs(SeeBar.slider.value - SeeBar.slider.maxValue) < 0.02f)
        {
            Time.timeScale = 0.0f;
            lostView.SetActive(true);
        }

        sliderValue = Mathf.FloorToInt(SeeBar.slider.value);
    }

    IEnumerator DecreaseValue(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            DecreaseIfNotChanged();
        }
    }

    void DecreaseIfNotChanged()
    {
        if (sliderValue != oldSliderValue)
        {
            oldSliderValue = sliderValue;
        }
        else
        {
            SeeBar.DecreaseByOne();
        }
    }
}