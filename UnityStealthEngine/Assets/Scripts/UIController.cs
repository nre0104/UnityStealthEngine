using System;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public BarController SeeBar;
    public BarController HearBar;

    public GameObject lostView;

    void Start()
    {
        SeeBar.SetMax(15);
        SeeBar.SetSlide(0);
        
        HearBar.SetMax(15);
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