using Assets.Scripts;
using Ping;
using UnityEngine;

public class MinimapBomb : MonoBehaviour
{
    void ShowIconsInRange()
    {
        GameManager.Instance.ShowAllMinimapIcons(GameManager.PingedObjects);
    }

    void HideIconsInRange()
    {
        GameManager.Instance.HideAllMinimapIcons(GameManager.PingedObjects);
    }

    public void MinimapPing()
    {
        ShowIconsInRange();
        var pingDuration = transform.gameObject.GetComponent<PingController>().PingDuration - 0.05f;

        transform.gameObject.GetComponent<BombBehaviourController>().IncreaseDestroyDelay(pingDuration);
        Invoke("HideIconsInRange", pingDuration);
    }
}
