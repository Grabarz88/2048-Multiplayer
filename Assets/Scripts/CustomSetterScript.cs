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


    public int X;
    public int Y;
    bool isIntX;
    bool isIntY;
    
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    
    public void StartCustom()
    {

        isIntX = int.TryParse(InputX.text, out X);
        isIntY = int.TryParse(InputY.text, out Y);
        if(isIntX && isIntY)
        {
            X = X+2;
            Y = Y+2;
            SceneManager.LoadScene("CustomGame");
            
        }
    }
}
