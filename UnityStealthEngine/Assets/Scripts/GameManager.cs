using System.Collections.Generic;
using Minimap;
using Ping;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    private static readonly List<GameObject> AllMinimapIcons = new List<GameObject>();
    public static readonly List<GameObject> PingedObjects = new List<GameObject>();

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

        DontDestroyOnLoad(this.gameObject);
    }

    public void Hi()
    {
        Debug.Log("Hi");
    }

    void Start()
    {
        foreach (var minimapIconController in FindObjectsOfType<MinimapIconController>())
        {
            AllMinimapIcons.Add(minimapIconController.gameObject);
        }

        HideAllMinimapIcons(AllMinimapIcons);

        foreach (var icon in AllMinimapIcons)
        {
            if (icon.transform.parent.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                icon.GetComponent<MinimapIconController>().Show();
            }
        }
    }

    public void ShowAllMinimapIcons(List<GameObject> list)
    {
        foreach (var gameObj in list)
        {
            gameObj.transform.GetComponentInChildren<MinimapIconController>().Show();
        }
    }

    public void HideAllMinimapIcons(List<GameObject> list)
    {
        foreach (var gameObj in list)
        {
            gameObj.transform.GetComponentInChildren<MinimapIconController>().Hide();
        }
    }
}