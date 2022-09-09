using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : SingletonPersistent<MainMenu>
{

    [Header("Menus")]
    [SerializeField]
    private Canvas m_MainMenu;
    [SerializeField]
    private Canvas m_GamePlay;

    [Header("Main Menu")]
    [SerializeField]
    private Text t_HighScore;

    public bool inGame = false;

    [Space]
    [SerializeField]
    private GameObject[] objectsToToggle;

    private void Start()
    {
        ToMainMenu();
    }
    public void ToMainMenu()
    {
        FreezeBall(true);
        m_MainMenu.gameObject.SetActive(true);
        m_GamePlay.gameObject.SetActive(false);


        ToggleListOfObjects(false);
        t_HighScore.text = PlayerPrefs.GetInt("HighScore").ToString();
        inGame = false;
    }

    public void ToGamePlay()
    {
        FreezeBall(false);
        ToggleListOfObjects(true);
        m_MainMenu.gameObject.SetActive(false);
        m_GamePlay.gameObject.SetActive(true);
        inGame = true;
    }

    private void ToggleListOfObjects(bool active)
    {
        foreach(GameObject go in objectsToToggle)
        {
            go.SetActive(active);
        }
    }

    public void FreezeBall(bool freeze)
    {
        TrackFinger.Instance.FreezeBallToggle(freeze);
    }
}
