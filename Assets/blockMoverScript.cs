using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockMoverScript : MonoBehaviour
{
    
    [SerializeField] GameObject kafelek;
    [SerializeField] int TableNumberX;
    [SerializeField] int TableNumberY;
    [SerializeField] GameObject[] pozycje = new GameObject[3]; 
    bool moved = false;
    

    


    void Start()
    {
        kafelek.gameObject.transform.position = new Vector2(0, 0);

        bool moved = false;
    }

    void OnGUI() 
    {
        if(Input.GetButtonDown("MoveRight") && moved == false)
        {
            TableNumberX++;
            moved = true;
            kafelek.gameObject.transform.position = new Vector2(pozycje[TableNumberX].gameObject.transform.position.x, pozycje[TableNumberY].gameObject.transform.position.y);
        }
        else if(Input.GetButtonDown("MoveLeft") && moved == false)
        {
            TableNumberX--;
            moved = true;
            kafelek.gameObject.transform.position = new Vector2(pozycje[TableNumberX].gameObject.transform.position.x, pozycje[TableNumberY].gameObject.transform.position.y);
        }


    }

    
}
