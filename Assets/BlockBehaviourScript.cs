using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehaviourScript : MonoBehaviour
{
  
    //Ten skrypt odpowiada za ruch kafelek.
    [SerializeField] public int CurrentXTablePosition;
    [SerializeField] public int CurrentYTablePosition;

    [SerializeField] int TableNumberX; // Pozycja X kafelka
    [SerializeField] int TableNumberY; // Pozycha Y kafelka
    [SerializeField] int value; // Liczba na kafelku
    [SerializeField] GameObject FieldSpawner;
    [SerializeField] GameObject BlockSpawner;
    FieldScript FieldScript;
    BlockBehaviourScript NextBlockBehaviourScript;
    List<GameObject> fields;
    List<GameObject> blocks;
    bool moved = false;
    bool unmovable = false;
    string dir;
    
    
    void AfterSpawn(int X, int Y)
    {
        CurrentXTablePosition = X;
        CurrentYTablePosition = Y;
        TableNumberX = CurrentXTablePosition;
        TableNumberY = CurrentYTablePosition;
        gameObject.transform.position = new Vector2(CurrentXTablePosition, CurrentYTablePosition);
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

        fields = FieldSpawner.GetComponent<SpawnField>().fields;
        foreach (GameObject field in fields)
        {
            FieldScript = field.GetComponent<FieldScript>();
            if (TableNumberX == FieldScript.TableXGetter() && TableNumberY == FieldScript.TableYGetter() && FieldScript.IsWall() == false)
            {
               if (FieldScript.IsTaken() == false)
               {
                    gameObject.transform.position = new Vector2(FieldScript.PositionXGetter(), FieldScript.PositionYGetter());
                    CurrentXTablePosition = TableNumberX;
                    CurrentYTablePosition = TableNumberY;
               }
               else if (FieldScript.IsTaken() == true)
               {
                    blocks = BlockSpawner.GetComponent<SpawnBlock>().blocks;
                    foreach (GameObject block in blocks)
                    {
                        NextBlockBehaviourScript = block.GetComponent<BlockBehaviourScript>();
                        if ((NextBlockBehaviourScript.CurrentXTablePosition == TableNumberX) && (NextBlockBehaviourScript.CurrentYTablePosition == TableNumberY))
                        {
                            if(NextBlockBehaviourScript.isUnMovable() == true)
                            {
                                moved = false;
                            }
                            else if (NextBlockBehaviourScript.isUnMovable() == false)
                            {
                                if (NextBlockBehaviourScript.getValue() == value)
                                {
                                    NextBlockBehaviourScript.levelUp();
                                    Destroy(gameObject);
                                    
                                }
                                else if(NextBlockBehaviourScript.getValue() != value)
                                {
                                    TableNumberX = CurrentXTablePosition;
                                    TableNumberY = CurrentYTablePosition;
                                    moved = false;
                                }
                            }
                        }
                    }
               }
            }
            else if (TableNumberX == FieldScript.TableXGetter() && TableNumberY == FieldScript.TableYGetter() && FieldScript.IsWall() == true)
            {
                if(dir == "right"){TableNumberX--;}
                else if(dir == "left"){TableNumberX++;}
                else if(dir == "up"){TableNumberY--;}
                else if(dir == "down"){TableNumberY++;}
                unmovable = true;
            }
            
        }
        moved = false;
    }

    public bool isUnMovable() {return unmovable;}
    public int getValue() {return value;}
    public void levelUp()
    {
        BlockSpawner.GetComponent<SpawnBlock>().levelUp(CurrentXTablePosition, CurrentYTablePosition, value);
    }



}
