using System.Collections;
using System.Collections.Generic;
using Minimap;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public static List<GameObject> minimapIcons = new List<GameObject>();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        foreach (var minimapIconController in FindObjectsOfType<MinimapIconController>())
        {
            minimapIcons.Add(minimapIconController.gameObject);
        }

        HideAllIcons(minimapIcons);

        foreach (var icon in minimapIcons)
        {
            if (icon.transform.parent.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                icon.GetComponent<MinimapIconController>().Show();
            }
        }
    }

    public void ShowAllIcons(List<GameObject> list)
    {
        foreach (var gameObj in list)
        {
            gameObj.transform.GetComponentInChildren<MinimapIconController>().Show();
        }
    }

    public void HideAllIcons(List<GameObject> list)
    {
        foreach (var gameObj in list)
        {
            gameObj.transform.GetComponentInChildren<MinimapIconController>().Hide();
        }
    }
}
