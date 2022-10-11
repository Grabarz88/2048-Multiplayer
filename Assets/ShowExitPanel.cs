using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowExitPanel : MonoBehaviour
{
    public GameObject exitMenu;
    public bool isPauseActive = false;
    void Start()
    {
        exitMenu.gameObject.SetActive(false);
    }
    
    public void exit()
    {
        isPauseActive = true;
        exitMenu.gameObject.SetActive(true);
        
    }
}


