using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehaviourScript : MonoBehaviour
{
    [SerializeField] public int TableNumberX;
    [SerializeField] public int TableNumberY;

    [SerializeField] public float positionX;
    [SerializeField] public float positionY;


    [SerializeField] GameObject FieldSpawner;
    [SerializeField] GameObject BlockSpawner;
    SpawnField SpawnField;
    SpawnBlock SpawnBlock;
    BlockBehaviourScript NextBlockBehaviourScript;

    public List<GameObject> fields;
    public List<GameObject> blocks;
    FieldScript FieldScript;

    [SerializeField] public int value;
    public string dir;
    [SerializeField] public bool unmovable;
    [SerializeField] public bool moved; // This variable is used to ensure that blocks won't spawn during "vidmo move". That means, if we press the button in direction that won't make any blocks move.
    [SerializeField] public bool cantLevelUpNow = false; // This variable is solution to issue #5
    
    void Start()
    {
    FieldSpawner = GameObject.Find("FieldSpawner");
    SpawnField = FieldSpawner.GetComponent<SpawnField>();
    fields = SpawnField.fields;
    dir = "empty";    
    }



    void Update()
    {
        if(Input.GetButtonDown("MoveRight")){dir = "right";}
        if(Input.GetButtonDown("MoveLeft")){dir = "left";}
        if(Input.GetButtonDown("MoveUp")){dir = "up";}
        if(Input.GetButtonDown("MoveDown")){dir = "down";}

        if(unmovable == false)
        {
            if(dir == "right"){TableNumberX++;}
            else if(dir == "left"){TableNumberX--;}
            else if(dir == "up"){TableNumberY++;}
            else if(dir == "down"){TableNumberY--;}

            foreach(GameObject field in fields)
            {
                FieldScript = field.GetComponent<FieldScript>();
                if(FieldScript.TableNumberX == TableNumberX && FieldScript.TableNumberY == TableNumberY)
                {
                    if(FieldScript.isWall == true)
                    {
                        if(dir == "right"){TableNumberX--;}
                        else if(dir == "left"){TableNumberX++;}
                        else if(dir == "up"){TableNumberY--;}
                        else if(dir == "down"){TableNumberY++;}
                        unmovable = true; //Pole jest ścianą, więc cofamy zmianę wartości pozycji w tabeli a potem deklarujemy że ten blok już się nie poruszy.
                    }
                    else if(FieldScript.isTaken == true && dir != "empty")
                    {
                        BlockSpawner = GameObject.Find("BlockSpawner");
                        SpawnBlock = BlockSpawner.GetComponent<SpawnBlock>();
                        blocks = SpawnBlock.blocks;
                        try
                        {
                        foreach(GameObject block in blocks)
                        {
                            if(block != null)
                            {
                                NextBlockBehaviourScript = block.GetComponent<BlockBehaviourScript>();
                                if(TableNumberX == NextBlockBehaviourScript.TableNumberX && TableNumberY == NextBlockBehaviourScript.TableNumberY && block != this.gameObject && NextBlockBehaviourScript.unmovable == true && NextBlockBehaviourScript.value == value)
                                {
                                    if(NextBlockBehaviourScript.cantLevelUpNow == false)
                                    {
                                        SpawnBlock.RemoveBlockFromList(block);
                                        Destroy(block);
                                        SpawnBlock.BlockLevelUp(NextBlockBehaviourScript.TableNumberX, NextBlockBehaviourScript.TableNumberY, value);
                                        ReleaseOldField(TableNumberX, TableNumberY, dir); //Musimy znaleźć stary kafelek i zadeklarować, że nie jest już zajęty.
                                        SpawnBlock.RemoveBlockFromList(this.gameObject);
                                        Destroy(this.gameObject);
                                    }
                                    else if (NextBlockBehaviourScript.cantLevelUpNow == true)
                                    {
                                        if(dir == "right"){TableNumberX--;}
                                        else if(dir == "left"){TableNumberX++;}
                                        else if(dir == "up"){TableNumberY--;}
                                        else if(dir == "down"){TableNumberY++;}
                                        unmovable = true;
                                    }

                                }
                                else if(TableNumberX == NextBlockBehaviourScript.TableNumberX && TableNumberY == NextBlockBehaviourScript.TableNumberY && block != this.gameObject && NextBlockBehaviourScript.unmovable == false)
                                {
                                    //Przypadek w którym nastąpiło zderzenie, ale uderzony kafelek może się jeszcze poruszać
                                    if(dir == "right"){TableNumberX--;} //Wycofujemy zwiększenie wartości. Następna iteracja Update na powrót ją zwiększy i znowu sprawdzi, czy następne pole jest już wolne
                                    else if(dir == "left"){TableNumberX++;}
                                    else if(dir == "up"){TableNumberY--;}
                                    else if(dir == "down"){TableNumberY++;} 
                                }
                                else if(TableNumberX == NextBlockBehaviourScript.TableNumberX && TableNumberY == NextBlockBehaviourScript.TableNumberY && block != this.gameObject && NextBlockBehaviourScript.unmovable == true && NextBlockBehaviourScript.value != value)
                                {
                                    //Przypadek w którym nastąpiło zderzenie kafelków o różnych wartościach
                                    if(dir == "right"){TableNumberX--;}
                                    else if(dir == "left"){TableNumberX++;}
                                    else if(dir == "up"){TableNumberY--;}
                                    else if(dir == "down"){TableNumberY++;}
                                    unmovable = true;
                                }
                            }
                        }
                        }
                        catch{}
                    }
                    else if(FieldScript.isTaken == false)
                    {
                        transform.position = new Vector2(FieldScript.positionX, FieldScript.positionY); //Ponieważ nie zmieniamy wartości unmovable, to bloczek się przesunie i będzie mógł ponownie sprawdzanie z Update, tym razem dla następnego kafelka.
                        moved = true;
                        TakeNewField(TableNumberX, TableNumberY);
                        ReleaseOldField(TableNumberX, TableNumberY, dir); //Musimy znaleźć stary kafelek i zadeklarować, że nie jest już zajęty.
                    }
                }
            }
        }
    }
    
    public void AfterSpawn(int x, int y)
    {
        FieldSpawner = GameObject.Find("FieldSpawner");
        SpawnField = FieldSpawner.GetComponent<SpawnField>();
        fields = SpawnField.fields;
        TableNumberX = x;
        TableNumberY = y;
        foreach (GameObject field in fields)
        {
            FieldScript = field.gameObject.GetComponent<FieldScript>();
            if(FieldScript.TableNumberX == TableNumberX && FieldScript.TableNumberY == TableNumberY)
            {
                positionX = FieldScript.positionX;
                positionY = FieldScript.positionY;
                FieldScript.isTaken = true; //Póki co, jest to potrzebne do testów. W dalszym etapie produkcji, pola chyba muszą same sprawdzać czy są zajęte
            }
        }
        this.gameObject.transform.position = new Vector2(positionX, positionY);
        TakeNewField(TableNumberX, TableNumberY); //Wykorzystanie tego tutaj jest bez sensu, skoro wyżej w tej funkcji ręcznie ustawiamy zajętość.
    }

    public void ReleaseOldField(int x, int y, string blockDir) //Ten x i y są niewykorzystane. To na pewno będzie działało?
    {
        foreach (GameObject previousField in fields) 
        {
            FieldScript = previousField.gameObject.GetComponent<FieldScript>();
            if(blockDir == "right")
            {
                if(FieldScript.TableNumberX == TableNumberX-1 && FieldScript.TableNumberY == TableNumberY){FieldScript.isTaken = false;}
            }
            else if(blockDir == "left")
            {
                if(FieldScript.TableNumberX == TableNumberX+1 && FieldScript.TableNumberY == TableNumberY){FieldScript.isTaken = false;}
            }
            else if(blockDir == "up")
            {
                if(FieldScript.TableNumberX == TableNumberX && FieldScript.TableNumberY == TableNumberY-1){FieldScript.isTaken = false;}
            }
            else if(blockDir == "down")
            {
                if(FieldScript.TableNumberX == TableNumberX && FieldScript.TableNumberY == TableNumberY+1){FieldScript.isTaken = false;}    
            }
        }
    }

    public void TakeNewField(int x, int y)
    {
        foreach (GameObject nextField in fields)
        {
            FieldScript = nextField.gameObject.GetComponent<FieldScript>();
            if(FieldScript.TableNumberX == TableNumberX && FieldScript.TableNumberY == TableNumberY){FieldScript.isTaken = true;}
        }
    }



}
