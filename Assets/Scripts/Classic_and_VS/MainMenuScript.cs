using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        if(GameObject.Find("ObjectToRememberColors")){Destroy(GameObject.Find("ObjectToRememberColors"));}
    }
    public void Classic()
    {
        SceneManager.LoadScene("CustomGame");
    }

    public void Custom()
    {
        SceneManager.LoadScene("CustomSettings");
    }

    public void LocalVS()
    {
        SceneManager.LoadScene("VSSettings");
    }

    public void LocalBR()
    {
        SceneManager.LoadScene("BRSettings");
    }
    
    public void OnlineModes()
    {
        SceneManager.LoadScene("OnlineMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
