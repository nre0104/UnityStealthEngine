using UnityEngine;

public class UIController : MonoBehaviour
{
    public BarController SeeBar;
    public BarController HearBar;

    void Start()
    {
        SeeBar.SetMax(5);
        SeeBar.SetSlide(0);
        
        HearBar.SetMax(5);
        HearBar.SetSlide(0);

        GameManager.SeeBar = SeeBar;
        GameManager.HearBar = HearBar;
    }
}
