using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomSetterScript : MonoBehaviour
{
    
    [SerializeField] public InputField InputX;
    [SerializeField] public InputField InputY;
    [SerializeField] GameObject FieldSpawner;
    [SerializeField] GameObject InvalidValuePanel;


    public int X;
    public int Y;
    bool isIntX;
    bool isIntY;
    
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        InvalidValuePanel.SetActive(false);
    }

    
    public void StartCustom()
    {

        isIntX = int.TryParse(InputX.text, out X);
        isIntY = int.TryParse(InputY.text, out Y);
        if(isIntX && isIntY)
        {
            if(X >= 4 && Y >= 4 && X <= 100 && Y <= 100)
            {
                X = X+2;
                Y = Y+2;
                SceneManager.LoadScene("CustomGame");
            }
            else
            {
                InvalidValuePanel.SetActive(true);
            }
            
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
