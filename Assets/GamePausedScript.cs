using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePausedScript : MonoBehaviour
{
    public GameObject settingsButton;
    public GameObject restartMenu;
    public GameObject exitMenu;
    ShowSettingsPanel ShowSettingsPanel;
    void Start()
    {
        restartMenu.gameObject.SetActive(false);
        exitMenu.gameObject.SetActive(false);
        ShowSettingsPanel = settingsButton.GetComponent<ShowSettingsPanel>();
    }

    public void resume()
    {
        ShowSettingsPanel.isPauseActive = false;
        gameObject.SetActive(false); 
    }
    public void restart()
    {
        restartMenu.gameObject.SetActive(true); 
        gameObject.SetActive(false);   
    }

    public void mainMenu()
    {
        exitMenu.gameObject.SetActive(true);   
        gameObject.SetActive(false);
    }

}
