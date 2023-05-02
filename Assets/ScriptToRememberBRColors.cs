using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScriptToRememberBRColors : MonoBehaviour
{
    [SerializeField] GameObject Player1Panel;
    [SerializeField] GameObject Player2Panel;
    [SerializeField] GameObject Player3Panel;
    [SerializeField] GameObject Player4Panel;
    int ActivePanelsCount = 2;

    public int Player1Color = 1;
    public int Player2Color = 2;
    public int Player3Color = 0;
    public int Player4Color = 0;
    [SerializeField] public GameObject Player1ColorPanel;
    [SerializeField] public GameObject Player2ColorPanel;
    [SerializeField] public GameObject Player3ColorPanel;
    [SerializeField] public GameObject Player4ColorPanel;
    [SerializeField] Image Player1PanelSpr;
    [SerializeField] Image Player2PanelSpr;
    [SerializeField] Image Player3PanelSpr;
    [SerializeField] Image Player4PanelSpr;

    [SerializeField] Text Player1IsComputerOrPlayer;
    [SerializeField] Text Player2IsComputerOrPlayer;
    [SerializeField] Text Player3IsComputerOrPlayer;
    [SerializeField] Text Player4IsComputerOrPlayer;
    public bool isPlayer1Playing = true;
    public bool isPlayer2Playing = true;
    public bool isPlayer3Playing = false;
    public bool isPlayer4Playing = false;
    public bool isPlayer1Computer = false;
    public bool isPlayer2Computer = false;
    public bool isPlayer3Computer = false;
    public bool isPlayer4Computer = false;

    int ComputerEnemiesCount = 0;
   
    void Start()
    {
    DontDestroyOnLoad(this.gameObject);
    Player1Panel.gameObject.SetActive(true);
    Player2Panel.gameObject.SetActive(true);
    Player3Panel.gameObject.SetActive(false);
    Player4Panel.gameObject.SetActive(false);   

    Player1PanelSpr = Player1ColorPanel.gameObject.GetComponent<Image>();
    Player2PanelSpr = Player2ColorPanel.gameObject.GetComponent<Image>();
    Player3PanelSpr = Player3ColorPanel.gameObject.GetComponent<Image>();
    Player4PanelSpr = Player4ColorPanel.gameObject.GetComponent<Image>();
    }


    public void Player1PanelCheck()
    {
        if(Player1Panel.activeSelf == false)
        {
            Player1Panel.gameObject.SetActive(true);
            ActivePanelsCount++;
            Player1NextColor();
            isPlayer1Playing = true;
        }
        else
        {
            if(ActivePanelsCount >= 3)
            {
                Player1Panel.gameObject.SetActive(false);
                ActivePanelsCount--;
                Player1Color = 0;
                isPlayer1Playing = false;
            }
        }
    }

    public void Player2PanelCheck()
    {
        if(Player2Panel.activeSelf == false)
        {
            Player2Panel.gameObject.SetActive(true);
            ActivePanelsCount++;
            Player2NextColor();
            isPlayer2Playing = true;
        }
        else
        {
            if(ActivePanelsCount >= 3)
            {
                Player2Panel.gameObject.SetActive(false);
                ActivePanelsCount--;
                Player2Color = 0;
                isPlayer2Playing = false;
            }
        }
    }

    public void Player3PanelCheck()
    {
        if(Player3Panel.activeSelf == false)
        {
            Player3Panel.gameObject.SetActive(true);
            ActivePanelsCount++;
            Player3NextColor();
            isPlayer3Playing = true;
        }
        else
        {
            if(ActivePanelsCount >= 3)
            {
                Player3Panel.gameObject.SetActive(false);
                ActivePanelsCount--;
                Player3Color = 0;
                isPlayer3Playing = false;
            }
        }
    }

    public void Player4PanelCheck()
    {
        if(Player4Panel.activeSelf == false)
        {
            Player4Panel.gameObject.SetActive(true);
            ActivePanelsCount++;
            Player4NextColor();
            isPlayer4Playing = true;
        }
        else
        {
            if(ActivePanelsCount >= 3)
            {
                Player4Panel.gameObject.SetActive(false);
                ActivePanelsCount--;
                Player4Color = 0;
                isPlayer4Playing = false;
            }
        }
    }

    public void Player1IsComputerChange()
    {
        if(isPlayer1Computer == false)
        {
            if(ComputerEnemiesCount + 1 < ActivePanelsCount)
            {
                isPlayer1Computer = true;
                ComputerEnemiesCount++;
                Player1IsComputerOrPlayer.text = "SI";
            }
        }
        else if(isPlayer1Computer == true)
        {
            isPlayer1Computer = false;
            ComputerEnemiesCount--;
            Player1IsComputerOrPlayer.text = "PLAYER";
        }
    }

    public void Player2IsComputerChange()
    {
        if(isPlayer2Computer == false)
        {
            if(ComputerEnemiesCount + 1 < ActivePanelsCount)
            {
                isPlayer2Computer = true;
                ComputerEnemiesCount++;
                Player2IsComputerOrPlayer.text = "SI";
            }
        }
        else if(isPlayer2Computer == true)
        {
            isPlayer2Computer = false;
            ComputerEnemiesCount--;
            Player2IsComputerOrPlayer.text = "PLAYER";
        }
    }

    public void Player3IsComputerChange()
    {
        if(isPlayer3Computer == false)
        {
            if(ComputerEnemiesCount + 1 < ActivePanelsCount)
            {
                isPlayer3Computer = true;
                ComputerEnemiesCount++;
                Player3IsComputerOrPlayer.text = "SI";
            }
        }
        else if(isPlayer3Computer == true)
        {
            isPlayer3Computer = false;
            ComputerEnemiesCount--;
            Player3IsComputerOrPlayer.text = "PLAYER";
        }
    }

    public void Player4IsComputerChange()
    {
        if(isPlayer4Computer == false)
        {
            if(ComputerEnemiesCount + 1 < ActivePanelsCount)
            {
                isPlayer4Computer = true;
                ComputerEnemiesCount++;
                Player4IsComputerOrPlayer.text = "SI";
            }
        }
        else if(isPlayer4Computer == true)
        {
            isPlayer4Computer = false;
            ComputerEnemiesCount--;
            Player4IsComputerOrPlayer.text = "PLAYER";
        }
    }

    public void Player1NextColor()
    {
        do
        {
            Player1Color++;
            if (Player1Color >= 6)
            {
                Player1Color = 1;
            }
        }while (Player1Color == Player2Color || Player1Color == Player3Color || Player1Color == Player4Color);
        
        ChangePlayerColor(Player1PanelSpr, Player1Color);
    }

    public void Player1PreviousColor()
    {
        do
        {
            Player1Color--;
            if (Player1Color <= 0)
            {
                Player1Color = 5;
            }
        }while (Player1Color == Player2Color || Player1Color == Player3Color || Player1Color == Player4Color);
        
        ChangePlayerColor(Player1PanelSpr, Player1Color);
    }

    public void Player2NextColor()
    {
        do
        {
            Player2Color++;
            if (Player2Color >= 6)
            {
                Player2Color = 1;
            }
        }while (Player2Color == Player1Color || Player2Color == Player3Color || Player2Color == Player4Color);
        
        ChangePlayerColor(Player2PanelSpr, Player2Color);
    }

    public void Player2PreviousColor()
    {
        do
        {
            Player2Color--;
            if (Player2Color <= 0)
            {
                Player2Color = 5;
            }
        }while (Player2Color == Player1Color || Player2Color == Player3Color || Player2Color == Player4Color);
        
        ChangePlayerColor(Player2PanelSpr, Player2Color);
    }


    public void Player3NextColor()
    {
        do
        {
            Player3Color++;
            if (Player3Color >= 6)
            {
                Player3Color = 1;
            }
        }while (Player3Color == Player1Color || Player3Color == Player2Color || Player3Color == Player4Color);
        
        ChangePlayerColor(Player3PanelSpr, Player3Color);
    }

    public void Player3PreviousColor()
    {
        do
        {
            Player3Color--;
            if (Player3Color <= 0)
            {
                Player3Color = 5;
            }
        }while (Player3Color == Player1Color || Player3Color == Player2Color || Player3Color == Player4Color);
        
        ChangePlayerColor(Player3PanelSpr, Player3Color);
    }

    public void Player4NextColor()
    {
        do
        {
            Player4Color++;
            if (Player4Color >= 6)
            {
                Player4Color = 1;
            }
        }while (Player4Color == Player1Color || Player4Color == Player2Color || Player4Color == Player3Color);
        
        ChangePlayerColor(Player4PanelSpr, Player4Color);
    }

    public void Player4PreviousColor()
    {
        do
        {
            Player4Color--;
            if (Player4Color <= 0)
            {
                Player4Color = 5;
            }
        }while (Player4Color == Player1Color || Player4Color == Player2Color || Player4Color == Player3Color);
        
        ChangePlayerColor(Player4PanelSpr, Player4Color);
    }


    public void ChangePlayerColor(Image PanelToChange, int colorID)
    {
        if(colorID == 1){PanelToChange.color = new Color32(119,221,250,255);} //blue
        else if(colorID == 2){PanelToChange.color = new Color32(242,118,140,255);} //red
        else if(colorID == 3){PanelToChange.color = new Color32(137,217,171,255);} //green
        else if(colorID == 4){PanelToChange.color = new Color32(236,123,222,255);} //pink  
        else if(colorID == 5){PanelToChange.color = new Color32(104,105,104,255);} //silver 
    }   

    public void StartBR()
    {
        SceneManager.LoadScene("LocalBR");
    } 

}
