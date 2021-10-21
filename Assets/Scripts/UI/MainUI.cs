using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Scripts.Managers;

public class MainUI : Singleton<MainUI>
{
    [SerializeField] private List<UIMenu> menus = new List<UIMenu>();
    [SerializeField] private UIMenu currentMenu;

    protected override void Awake()
    {
        base.Awake();
        currentMenu.Open();
        EventManager.OnEndGame += EndGame;
    }

    private void OnDestroy()
    {
        EventManager.OnEndGame -= EndGame;
    }

    private void EndGame()
    {
        OpenMenu<EndgameMenu>();
    }

    public static T GetMenu<T>() where T : UIMenu
    {
        UIMenu panel = Instance.menus.FirstOrDefault(x => x.GetComponent<T>() != null);
        return (T)panel;
    }

    public static T OpenMenu<T>() where T : UIMenu
    {
        if(Instance.currentMenu != null)
            Instance.currentMenu.Close();

        T panel = GetMenu<T>();
        panel.Open();
        Instance.currentMenu = panel;
        return panel;
    }

    public static void CloseMenu<T>() where T : UIMenu
    {
        T panel = GetMenu<T>();
        panel.Close();
    }

    public static void CloseCurrentMenu()
    {
        Instance.currentMenu.Close();
    }
}
