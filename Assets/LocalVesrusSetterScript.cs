using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LocalVesrusSetterScript : MonoBehaviour
{
    
    public int Player1Color = 1;
    public int Player2Color = 2;
    [SerializeField] public GameObject Player1ColorPanel;
    [SerializeField] public GameObject Player2ColorPanel;

    public bool Player2IsComputer = true; 

    [SerializeField] Image Player1PanelSpr;
    [SerializeField] Image Player2PanelSpr;

    public void Start()
    {
    
        Player1PanelSpr = Player1ColorPanel.gameObject.GetComponent<Image>();
        Player2PanelSpr = Player2ColorPanel.gameObject.GetComponent<Image>();

    }

    public void Player1NextColor()
    {
        Player1Color++;
        if (Player1Color >= 6)
        {
            Player1Color = 1;
        }
        if(Player1Color == Player2Color){Player1Color ++;}
        ChangePlayer1Color(Player1PanelSpr, Player1Color);
    }

    public void Player1PreviousColor()
    {
        Player1Color--;
        if (Player1Color <= 0)
        {
            Player1Color = 5;
        }
        if(Player1Color == Player2Color){Player1Color --;}
        ChangePlayer1Color(Player1PanelSpr, Player1Color);
    }

    public void Player2NextColor()
    {
        Player2Color++;
        if (Player2Color >= 6)
        {
            Player2Color = 1;
        }
        if (Player2Color == Player1Color){Player2Color++;}
        ChangePlayer1Color(Player2PanelSpr, Player2Color);
    }

    public void Player2PreviousColor()
    {
        Player2Color--;
        if (Player2Color <= 0)
        {
            Player2Color = 5;
        }
        if (Player2Color == Player1Color){Player2Color--;}
        ChangePlayer1Color(Player2PanelSpr, Player2Color);
    }


    public void ChangePlayer1Color(Image PanelToChange, int colorID)
    {
        if(colorID == 1){PanelToChange.color = new Color32(119,221,250,255);} //blue
        else if(colorID == 2){PanelToChange.color = new Color32(242,118,140,255);} //red
        else if(colorID == 3){PanelToChange.color = new Color32(137,217,171,255);} //green
        else if(colorID == 4){PanelToChange.color = new Color32(236,123,222,255);} //pink  
        else if(colorID == 5){PanelToChange.color = new Color32(104,105,104,255);} //silver 
    }    

    public void StartGame()
    {
        GameObject ObjectToRememberColors = GameObject.Find("ObjectToRememberColors");
        ObjectToRememberColors.GetComponent<ScriptToRememberColors>().Player1ColorSetter(Player1Color);
        ObjectToRememberColors.GetComponent<ScriptToRememberColors>().Player2ColorSetter(Player2Color);
        DontDestroyOnLoad(ObjectToRememberColors);
        SceneManager.LoadScene("LocalVS");
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
