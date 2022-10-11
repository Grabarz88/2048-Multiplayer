using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRestartPanel : MonoBehaviour
{
    public GameObject restartMenu;
    public bool isPauseActive = false;
    void Start()
    {
        restartMenu.gameObject.SetActive(false);
    }
    
    public void restart()
    {
        isPauseActive = true;
        restartMenu.gameObject.SetActive(true);
        
    }
}


