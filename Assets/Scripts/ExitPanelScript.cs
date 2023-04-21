using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPanelScript : MonoBehaviour
{
    public GameObject SettingsPanel;
    
    public void ExitYes()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitNo()
    {
        SettingsPanel.SetActive(true);
        gameObject.SetActive(false);
    }


}
