using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalVSRestartPanelScript : MonoBehaviour
{
    public GameObject SettingsPanel;
    
    public void RestartYes()
    {
        SceneManager.LoadScene("VsSettings");
    }

    public void RestartNo()
    {
        SettingsPanel.SetActive(true);
        gameObject.SetActive(false);
    }

}
