using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSettingsPanel : MonoBehaviour
{
    public GameObject settingsPanel;
    public bool isPauseActive = false;
    void Start()
    {
        settingsPanel.gameObject.SetActive(false);
    }

    public void SettingsPanel()
    {
        isPauseActive = true;
        settingsPanel.gameObject.SetActive(true);
        
    }
}
