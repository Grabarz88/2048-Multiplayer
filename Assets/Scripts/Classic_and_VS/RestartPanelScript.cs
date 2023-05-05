using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartPanelScript : MonoBehaviour
{
    public GameObject SettingsPanel;
    
    public void RestartYes()
    {
        SceneManager.LoadScene("CustomGame");
    }

    public void RestartNo()
    {
        SettingsPanel.SetActive(true);
        gameObject.SetActive(false);
    }


}
