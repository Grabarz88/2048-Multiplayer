using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehaviourScript : MonoBehaviour
{
  
    //Ten skrypt odpowiada za ruch kafelek.
    [SerializeField] int TableNumberX; // Pozycja X kafelka
    [SerializeField] int TableNumberY; // Pozycha Y kafelka
    [SerializeField] int value; // Liczba na kafelku
    [SerializeField] GameObject FieldSpawner;
    List<GameObject> fields;
    bool moved = false;
    
    
    void AfterSpawn(int X, int Y)
    {
        TableNumberX = X;
        TableNumberY = Y;
        gameObject.transform.position = new Vector2(TableNumberX, TableNumberY);
    }
    
    
    void OnGUI() 
    {
        if(Input.GetButtonDown("MoveRight") && moved == false)
        {
            TableNumberX++;
            moved = true;
            fields = FieldSpawner.GetComponent<SpawnField>().fields;

            foreach (GameObject field in fields)
        {
            // int fieldXposition = field.GetComponent<FieldScript>().TableXGetter();
            Debug.Log(field.GetComponent<FieldScript>().TableXGetter());
        }
            
        }
        // else if(Input.GetButtonDown("MoveLeft") && moved == false)
        // {
        //     TableNumberX--;
        //     moved = true;
        //     kafelek.gameObject.transform.position = new Vector2(pozycje[TableNumberX].gameObject.transform.position.x, pozycje[TableNumberY].gameObject.transform.position.y);
        // }
    }



}
