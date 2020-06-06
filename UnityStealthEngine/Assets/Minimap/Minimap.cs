using UnityEngine;

public class Minimap : MonoBehaviour
{
    private static Minimap instance;
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
