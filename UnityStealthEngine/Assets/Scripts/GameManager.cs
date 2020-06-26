using System.Collections.Generic;
using Assets.Scripts.Minimap;
using Assets.Scripts.UI;
using UnityEngine;

/**
 * Centralized information manager of the game
 * Stores different lists
 */
public class GameManager : MonoBehaviour
{   
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private static readonly List<GameObject> AllMinimapIcons = new List<GameObject>();
    public static readonly List<GameObject> PingedObjects = new List<GameObject>();
    public static List<GameObject> MarkedEnemies = new List<GameObject>();
    public static Queue<Material> OldEnemiesMaterials = new Queue<Material>();
    public static GameObject player;

    public static BarController SeeBar;
    public static BarController HearBar;

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

        foreach (var minimapIconController in FindObjectsOfType<MinimapIconController>())
        {
            AllMinimapIcons.Add(minimapIconController.gameObject);
        }

        HideAllMinimapIcons(AllMinimapIcons);

        foreach (var icon in AllMinimapIcons)
        {
            if (icon.transform.parent.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                player = icon.transform.parent.gameObject;
                icon.GetComponent<MinimapIconController>().Show();
            }
        }
    }

    /**
     * Shows all MinimapIcons within the list
     * <para>A list of GameObjects with an MinimapController</para>
     */
    public void ShowAllMinimapIcons(List<GameObject> list)
    {
        foreach (var gameObj in list)
        {
            var minimapIconController = gameObj.transform.GetComponentInChildren<MinimapIconController>();

            if (minimapIconController != null)
            {
                minimapIconController.Show();
            }
        }
    }

    /**
     * Hides all MinimapIcons within the list
     * <para>A list of GameObjects with an MinimapController</para>
     */
    public void HideAllMinimapIcons(List<GameObject> list)
    {
        foreach (var gameObj in list)
        {
            var minimapIconController = gameObj.transform.GetComponentInChildren<MinimapIconController>();

            if (minimapIconController != null)
            {
                minimapIconController.Hide();
            }
        }
    }
}