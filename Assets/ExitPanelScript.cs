using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPanelScript : MonoBehaviour
{
    public GameObject ShowExitPanelButton;
    
    public void ExitYes()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitNo()
    {
        ShowExitPanelButton.GetComponent<ShowExitPanel>().isPauseActive = false;
        gameObject.SetActive(false);
    }


}
