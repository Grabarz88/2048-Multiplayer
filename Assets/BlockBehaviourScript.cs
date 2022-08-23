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
    FieldScript Fieldscript;
    List<GameObject> fields;
    bool moved = false;
    string dir;
    
    
    void AfterSpawn(int X, int Y)
    {
        TableNumberX = X;
        TableNumberY = Y;
        gameObject.transform.position = new Vector2(TableNumberX, TableNumberY);
    }
    
    
    void Update() 
    {
        
        if(Input.GetButtonDown("MoveRight") && moved == false)
        {
            TableNumberX++;
            dir = "right";
            moved = true;    
        }
        else if(Input.GetButtonDown("MoveLeft") && moved == false)
        {
            TableNumberX--;
            dir = "left";
            moved = true; 
        }
        else if(Input.GetButtonDown("MoveUp") && moved == false)
        {
            TableNumberY++;
            dir = "up";
            moved = true; 
        }
        else if(Input.GetButtonDown("MoveDown") && moved == false)
        {
            TableNumberY--;
            dir = "down";
            moved = true; 
        }  
        // Debug.Log(TableNumberX);
        // Debug.Log(TableNumberY);
        fields = FieldSpawner.GetComponent<SpawnField>().fields;
        foreach (GameObject field in fields)
        {
            Fieldscript = field.GetComponent<FieldScript>();
            if (TableNumberX == Fieldscript.TableXGetter() && TableNumberY == Fieldscript.TableYGetter() && Fieldscript.IsWall() == false)
            {
                gameObject.transform.position = new Vector2(Fieldscript.PositionXGetter(), Fieldscript.PositionYGetter());
            }
            else if (TableNumberX == Fieldscript.TableXGetter() && TableNumberY == Fieldscript.TableYGetter() && Fieldscript.IsWall() == true)
            {
                if(dir == "right"){TableNumberX--;}
                else if(dir == "left"){TableNumberX++;}
                else if(dir == "up"){TableNumberY--;}
                else if(dir == "down"){TableNumberY++;}
            }
            
        }
        moved = false;
    }



}
