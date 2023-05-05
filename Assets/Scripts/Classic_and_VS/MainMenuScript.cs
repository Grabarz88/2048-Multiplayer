using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
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

    public void Quit()
    {
        Application.Quit();
    }
}
