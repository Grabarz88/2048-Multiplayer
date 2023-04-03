using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LocalVS_AI : MonoBehaviour
{
   
    public GameObject BlockSpawner;
    LocalVsSpawnBlock SpawnBlock;
    public GameObject FieldSpawner;
    SpawnField SpawnField;
    FieldScript FieldScript;
    public bool EnemyIsComputer = false;
    public LocalVSBlockBehaviourScript P1BBH;
    public LocalVSBlockBehaviourScript P2BBH;
    public string dirIfYOuCanWin = "null";
    public List<GameObject> blocks;
    public List<GameObject> P1Blocks; //Probably not requierd anymore
    public List<GameObject> P2Blocks; //Probably not required anymore
    public List<GameObject> fields;
    public GameObject P1MainBlock;
    public GameObject P2MainBlock;

    void Start()
    {
        GameObject BlockSpawner = GameObject.Find("LocalVSBlockSpawner");
        SpawnBlock = BlockSpawner.GetComponent<LocalVsSpawnBlock>();
        GameObject FieldSpawner = GameObject.Find("FieldSpawner");
        SpawnField = FieldSpawner.GetComponent<SpawnField>();
        GameObject ObjectToRememberColors = GameObject.Find("ObjectToRememberColors");
        EnemyIsComputer = ObjectToRememberColors.GetComponent<ScriptToRememberColors>().EnemyIsComputer();
    }

    public void Player2IsMoving_AI()
    { 
        if(EnemyIsComputer == true)
        {
            P1MainBlock = GameObject.Find("P1MainBlock");
            P2MainBlock = GameObject.Find("P2MainBlock");
            P1BBH = P1MainBlock.GetComponent<LocalVSBlockBehaviourScript>();
            P2BBH = P2MainBlock.GetComponent<LocalVSBlockBehaviourScript>();

            if(CheckIfYouCanWinNow())
            {
                Debug.Log("Mam Cię");
                StartCoroutine(FinishPlayer());
            }
            else
            {
                StartCoroutine(ThinkAndMove());
            }
    
        }
        
    }

    public bool CheckIfYouCanWinNow()
    {
        bool result = false;
        if(P2BBH.value > P1BBH.value) //If Computer has a block of higher value, he should check if he can have a finishing move.
        {
            if((P1BBH.TableNumberX - P2BBH.TableNumberX == 0) || (P1BBH.TableNumberY - P2BBH.TableNumberY == 0)) //We check if they are on the same line
            {
                if(P1BBH.TableNumberY - P2BBH.TableNumberY == 0) //They are one the same Y axis. So we will be operating on fields from X axis
                {
                    int i = P1BBH.TableNumberX - P2BBH.TableNumberX;
                    if(i == 1)
                    {
                        dirIfYOuCanWin = "right";
                        result = true;
                    }
                    else if(i == -1)
                    {
                        dirIfYOuCanWin = "left";
                        result = true;
                    }
                    else 
                    {
                        do
                        {
                            fields = SpawnField.fields;
                            fields.TrimExcess();
                            foreach(GameObject field in fields)
                            {
                                FieldScript = field.GetComponent<FieldScript>();
                                if(FieldScript.TableNumberY == P2BBH.TableNumberY && FieldScript.TableNumberX == P2BBH.TableNumberX + i)
                                {
                                    if(FieldScript.isTaken)
                                    {
                                        dirIfYOuCanWin = "null";
                                        return false;
                                    }
                                }
                            }
                            if(i>0)
                            {
                                dirIfYOuCanWin = "right";
                                i--;
                            }
                            if(i<0)
                            {
                                dirIfYOuCanWin = "left";
                                i++;
                            }
                            if (i == 1 || i == -1)
                            {
                                result = true;
                            }

                        }while(i>1 || i<-1);
                        result = false; // bo nie wszystkie zwracają

                    }
                } 
                else if(P1BBH.TableNumberX - P2BBH.TableNumberX == 0) //They are one the same X axis. So we will be operating on fields from Y axis
                {
                    int i = P1BBH.TableNumberY - P2BBH.TableNumberY;
                    if(i == 1)
                    {
                        dirIfYOuCanWin = "up";
                        result = true;
                    }
                    else if(i == -1)
                    {
                        dirIfYOuCanWin = "down";
                        result = true;
                    }
                    else 
                    {
                        do
                        {
                            fields = SpawnField.fields;
                            fields.TrimExcess();
                            foreach(GameObject field in fields)
                            {
                                FieldScript = field.GetComponent<FieldScript>();
                                if(FieldScript.TableNumberY == P2BBH.TableNumberY + i && FieldScript.TableNumberX == P2BBH.TableNumberX)
                                {
                                    if(FieldScript.isTaken)
                                    {
                                        dirIfYOuCanWin = "null";
                                        return false;
                                    }
                                }
                            }
                            if(i>0)
                            {
                                dirIfYOuCanWin = "up";
                                i--;
                            }
                            if(i<0)
                            {
                                dirIfYOuCanWin = "down";
                                i++;
                            }

                        }while(i>1 || i<-1);
                        if (i == 1 || i == -1)
                            {
                                result = true;
                            }

                    }
                }
                else 
                {
                    result = false;
                } 

            }
            else
            {
                result = false;
            }
            // ---------------------------- Tu miał być nowy koda. Do przemyślenia i wypracowania-------------
            // int i = 1;
            // bool winMoveFound = false;
            // do 
            // {
            //     if(P1BBH.TableNumberX + i == P2BBH.TableNumberX && P1BBH.TableNumberY == P2BBH.TableNumberY)
            //     {
            //         int j = i-1;
            //         int l = j;
            //         do
            //         {
            //             fields = SpawnField.fields;
            //             fields.TrimExcess();
            //             foreach(GameObject field in fields)
            //             {
            //                 FieldScript = field.GetComponent<FieldScript>();
            //                 if(FieldScript.TableNumberX == P1BBH.TableNumberX + j && FieldScript.TableNumberY == P1BBH.TableNumberY) 
            //                 {
            //                     if(FieldScript.isTaken == false)
            //                     {
            //                         l--;
            //                     }
            //                 }
            //             }
            //             if()
            //             {
            //                winMoveFound = true;
            //                return true;
            //             }
            //         } while (j >= 1);
            //         return true;
            //     }
            // } while(i <= 4);

            //------------------------ Tu niżej był sgarszy kod-----------------------
            // if(P1BBH.TableNumberX + 1 == P2BBH.TableNumberX && P1BBH.TableNumberY == P2BBH.TableNumberY)
            // {
            //     dirIfYOuCanWin = "left";
            //     return true;
            // }
            // else if(P1BBH.TableNumberX - 1 == P2BBH.TableNumberX && P1BBH.TableNumberY == P2BBH.TableNumberY)
            // {
            //     dirIfYOuCanWin = "right";
            //     return true;
            // }
            // else if(P1BBH.TableNumberX == P2BBH.TableNumberX && P1BBH.TableNumberY + 1 == P2BBH.TableNumberY)
            // {
            //     dirIfYOuCanWin = "down";
            //     return true;
            // }
            // else if(P1BBH.TableNumberX == P2BBH.TableNumberX && P1BBH.TableNumberY - 1 == P2BBH.TableNumberY)
            // {
            //     dirIfYOuCanWin = "up";
            //     return true;
            // }
            // else
            // {
            //     return false;
            // }
        }
        else
        {
            result = false;
        }
        return result;
    }
    
    

    IEnumerator ThinkAndMove()
    {
        yield return new WaitForSeconds(1f);
        int randDir = Random.Range(1, 5);
 
        blocks = SpawnBlock.blocks;
        blocks.TrimExcess();
        foreach(GameObject block in blocks)
        {
            LocalVSBlockBehaviourScript BlockBehaviourScript = block.GetComponent<LocalVSBlockBehaviourScript>();
            if(BlockBehaviourScript.OwnerID == 0 || BlockBehaviourScript.OwnerID == 2)
            {
                if (randDir == 1){BlockBehaviourScript.dir = "right";}
                else if (randDir == 2){BlockBehaviourScript.dir = "left";}  
                else if (randDir == 3){BlockBehaviourScript.dir = "up";}  
                else if (randDir == 4){BlockBehaviourScript.dir = "down";}  
            }
        }
    }

    IEnumerator FinishPlayer()
    {
        yield return new WaitForSeconds(2f);
        blocks = SpawnBlock.blocks;
        blocks.TrimExcess();
        foreach(GameObject block in blocks)
        {
            LocalVSBlockBehaviourScript BlockBehaviourScript = block.GetComponent<LocalVSBlockBehaviourScript>();
            if(BlockBehaviourScript.OwnerID == 0 || BlockBehaviourScript.OwnerID == 2)
            {
                BlockBehaviourScript.dir = dirIfYOuCanWin;
            }
        }
    }


}
