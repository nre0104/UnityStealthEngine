using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public BarController SeeBar;
    public BarController HearBar;

    public GameObject lostView;

    void Start()
    {
        SeeBar.SetMax(5);
        SeeBar.SetSlide(0);
        
        HearBar.SetMax(5);
        HearBar.SetSlide(0);

        GameManager.SeeBar = SeeBar;
        GameManager.HearBar = HearBar;
    }

    void Update()
    {
        if (Math.Abs(SeeBar.slider.value - SeeBar.slider.maxValue) < 0.02f)
        {
            Time.timeScale = 0.0f;
            lostView.SetActive(true);
        }
    }
}