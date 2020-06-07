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
        var pingDuration = transform.gameObject.GetComponent<PingController>().PingDuration;

        transform.gameObject.GetComponent<BombBehaviourController>().increaseDestroyDelay(pingDuration);
        Invoke("HideIconsInRange", pingDuration);
    }
}
