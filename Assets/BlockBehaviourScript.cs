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
    public bool moving = false;
    public bool unmovable = false;
    string dir;
    
    
    // void Start()
    // {
    //     FieldSpawner = GameObject.Find("FieldSpawner"); // Pamiętaj dodać na końcu (clone) jak już zrobisz jakieś menu itp.
    //     BlockSpawner = GameObject.Find("BlockSpawner");
    // }
    
    public void AfterSpawn(int X, int Y)
    {
        CurrentXTablePosition = X;
        CurrentYTablePosition = Y;
        TableNumberX = CurrentXTablePosition;
        TableNumberY = CurrentYTablePosition;

        FieldSpawner = GameObject.Find("FieldSpawner");
        BlockSpawner = GameObject.Find("BlockSpawner");
        fields = FieldSpawner.GetComponent<SpawnField>().fields;
        foreach(GameObject field in fields)
        {
            FieldScript = field.GetComponent<FieldScript>();
            if (TableNumberX == FieldScript.TableXGetter() && TableNumberY == FieldScript.TableYGetter())
            {
                gameObject.transform.position = new Vector2(FieldScript.PositionXGetter(), FieldScript.PositionYGetter());
            }
        }
    }
    
    
    void Update() 
    {
        
        if(Input.GetButtonDown("MoveRight") && moving == false && unmovable == false)
        {
            TableNumberX++;
            dir = "right";
            moving = true;    
        }
        else if(Input.GetButtonDown("MoveLeft") && moving == false && unmovable == false)
        {
            TableNumberX--;
            dir = "left";
            moving = true; 
        }
        else if(Input.GetButtonDown("MoveUp") && moving == false && unmovable == false)
        {
            TableNumberY++;
            dir = "up";
            moving = true; 
        }
        else if(Input.GetButtonDown("MoveDown") && moving == false && unmovable == false)
        {
            TableNumberY--;
            dir = "down";
            moving = true; 
        }  

        fields = FieldSpawner.GetComponent<SpawnField>().fields;
        foreach (GameObject field in fields)
        {
            FieldScript = field.GetComponent<FieldScript>();
            if (TableNumberX == FieldScript.TableXGetter() && TableNumberY == FieldScript.TableYGetter())
            {    
                if(FieldScript.IsWall() == false)
                {
                    if (FieldScript.IsTaken() == false)
                    {
                        gameObject.transform.position = new Vector2(FieldScript.PositionXGetter(), FieldScript.PositionYGetter());
                        foreach (GameObject leftField in fields)
                        {
                            FieldScript = leftField.GetComponent<FieldScript>();
                            if (FieldScript.TableXGetter() == CurrentXTablePosition && FieldScript.TableYGetter() == CurrentYTablePosition)
                            {
                                FieldScript.isTaken = false;
                            }
                        }
                        CurrentXTablePosition = TableNumberX;
                        CurrentYTablePosition = TableNumberY;
                        moving = false;
                        if (dir == "right"){TableNumberX++;}
                        else if (dir == "left"){TableNumberX--;}
                        else if (dir == "up"){TableNumberY++;}
                        else if (dir == "down"){TableNumberY--;}
                    }
                        else if (FieldScript.IsTaken() == true)
                    {
                        blocks = BlockSpawner.GetComponent<SpawnBlock>().blocks;
                        foreach (GameObject block in blocks)
                        {
                            NextBlockBehaviourScript = block.GetComponent<BlockBehaviourScript>();
                            if ((NextBlockBehaviourScript.CurrentXTablePosition == TableNumberX) && (NextBlockBehaviourScript.CurrentYTablePosition == TableNumberY) && NextBlockBehaviourScript != this.gameObject.GetComponent<BlockBehaviourScript>() )
                            {
                                if(NextBlockBehaviourScript.isUnMovable() == false)
                                {
                                    moving = false;
                                }
                                else if (NextBlockBehaviourScript.isUnMovable() == true)
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
                                        unmovable = true;
                                        moving = false;
                                        BlockSpawner.GetComponent<SpawnBlock>().checkSpawnReady();
                                    }
                                }
                            }
                        }
                    }
                }
                else if (FieldScript.IsWall() == true)
                {
                    if(dir == "right"){TableNumberX--;}
                    else if(dir == "left"){TableNumberX++;}
                    else if(dir == "up"){TableNumberY--;}
                    else if(dir == "down"){TableNumberY++;}
                    unmovable = true;
                    moving = false;
                    BlockSpawner.GetComponent<SpawnBlock>().checkSpawnReady();
                } 
            }
        }
        
    }

    public bool isUnMovable() {return unmovable;}
    public int getValue() {return value;}
    public void levelUp()
    {
        BlockSpawner.GetComponent<SpawnBlock>().levelUp(CurrentXTablePosition, CurrentYTablePosition, value);
    }



}
