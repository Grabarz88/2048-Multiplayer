using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartPanelScript : MonoBehaviour
{
    public GameObject ShowRestartPanelButton;
    
    public void RestartYes()
    {
        SceneManager.LoadScene("CustomGame");
    }

    public void RestartNo()
    {
        ShowRestartPanelButton.GetComponent<ShowRestartPanel>().isPauseActive = false;
        gameObject.SetActive(false);
    }


}
