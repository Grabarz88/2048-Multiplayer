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
                    // We check if blocks are neighbours to themselfs
                    int i = P1BBH.TableNumberX - P2BBH.TableNumberX;
                    if(i == 1)
                    {
                        dirIfYOuCanWin = "right";
                        return true;
                    }
                    else if(i == -1)
                    {
                        dirIfYOuCanWin = "left";
                        return true;
                    }
                    else // There is at least one field between P1 and P2 block
                    {
                        fields = SpawnField.fields;
                        fields.TrimExcess();
                        
                        if(i>0) // Case when the P1 block is on the right side of P2 block
                        {
                            i--;
                            foreach (GameObject field in fields)
                            {
                                FieldScript = field.GetComponent<FieldScript>();
                                if(FieldScript.TableNumberY == P2BBH.TableNumberY && FieldScript.TableNumberX == P2BBH.TableNumberX + i)
                                {
                                    if(FieldScript.isTaken)
                                    {
                                        dirIfYOuCanWin = "null";
                                        return false;
                                    }
                                    else
                                    {
                                        dirIfYOuCanWin = "right";
                                        result = true;
                                    }
                                }   
                            }
                            if(i>1)
                            {
                                i--;
                                foreach (GameObject field in fields)
                                {
                                    FieldScript = field.GetComponent<FieldScript>();
                                    if(FieldScript.TableNumberY == P2BBH.TableNumberY && FieldScript.TableNumberX == P2BBH.TableNumberX + i)
                                    {
                                        if(FieldScript.isTaken)
                                        {
                                            return false;
                                        }
                                        else 
                                        {
                                            return true;
                                        }
                                    }
                                }
                            return result;
                            }
                            else 
                            {
                                return true;
                            }
                        return result;
                        }
                        else if(i<0) // Case when the P1 block is on the left side of P2 block
                        {
                            i++;
                            foreach (GameObject field in fields)
                            {
                                FieldScript = field.GetComponent<FieldScript>();
                                if(FieldScript.TableNumberY == P2BBH.TableNumberY && FieldScript.TableNumberX == P2BBH.TableNumberX + i)
                                {
                                    if(FieldScript.isTaken)
                                    {
                                        dirIfYOuCanWin = "null";
                                        return false;
                                    }
                                    else
                                    {
                                        dirIfYOuCanWin = "left";
                                        result = true;
                                    }
                                }   
                            }
                            if(i<-1)
                            {
                                i++;
                                foreach (GameObject field in fields)
                                {
                                    FieldScript = field.GetComponent<FieldScript>();
                                    if(FieldScript.TableNumberY == P2BBH.TableNumberY && FieldScript.TableNumberX == P2BBH.TableNumberX + i)
                                    {
                                        if(FieldScript.isTaken)
                                        {
                                            return false;
                                        }
                                        else 
                                        {
                                            return true;
                                        }
                                    }
                                }
                            return result;
                            }
                            else 
                            {
                                return true;
                            }
                        return result;
                        }
                        else
                        {
                            return false;
                        }
                    return result;
                    }
                return result;
                }
                else if(P1BBH.TableNumberX - P2BBH.TableNumberX == 0) //They are one the same X axis. So we will be operating on fields from Y axis
                {
                    // We check if blocks are neighbours to themselfs
                    int i = P1BBH.TableNumberY - P2BBH.TableNumberY;
                    if(i == 1)
                    {
                        dirIfYOuCanWin = "up";
                        return true;
                    }
                    else if(i == -1)
                    {
                        dirIfYOuCanWin = "down";
                        return true;
                    }
                    else // There is at least one field between P1 and P2 block
                    {
                        fields = SpawnField.fields;
                        fields.TrimExcess();
                        
                        if(i>0) // Case when the P1 block is higher than P2 block
                        {
                            i--;
                            foreach (GameObject field in fields)
                            {
                                FieldScript = field.GetComponent<FieldScript>();
                                if(FieldScript.TableNumberX == P2BBH.TableNumberX && FieldScript.TableNumberY == P2BBH.TableNumberY + i)
                                {
                                    if(FieldScript.isTaken)
                                    {
                                        dirIfYOuCanWin = "null";
                                        return false;
                                    }
                                    else
                                    {
                                        dirIfYOuCanWin = "up";
                                        result = true;
                                    }
                                }   
                            }
                            if(i>1)
                            {
                                i--;
                                foreach (GameObject field in fields)
                                {
                                    FieldScript = field.GetComponent<FieldScript>();
                                    if(FieldScript.TableNumberX == P2BBH.TableNumberX && FieldScript.TableNumberY == P2BBH.TableNumberY + i)
                                    {
                                        if(FieldScript.isTaken)
                                        {
                                            return false;
                                        }
                                        else 
                                        {
                                            return true;
                                        }
                                    }
                                }
                            return result;
                            }
                            else 
                            {
                                return true;
                            }
                        return result;
                        }
                        else if(i<0) // Case when the P1 block is lower P2 block
                        {
                            i++;
                            foreach (GameObject field in fields)
                            {
                                FieldScript = field.GetComponent<FieldScript>();
                                if(FieldScript.TableNumberX == P2BBH.TableNumberX && FieldScript.TableNumberY == P2BBH.TableNumberY + i)
                                {
                                    if(FieldScript.isTaken)
                                    {
                                        dirIfYOuCanWin = "null";
                                        return false;
                                    }
                                    else
                                    {
                                        dirIfYOuCanWin = "down";
                                        result = true;
                                    }
                                }   
                            }
                            if(i<-1)
                            {
                                i++;
                                foreach (GameObject field in fields)
                                {
                                    FieldScript = field.GetComponent<FieldScript>();
                                    if(FieldScript.TableNumberX == P2BBH.TableNumberX && FieldScript.TableNumberY == P2BBH.TableNumberY + i)
                                    {
                                        if(FieldScript.isTaken)
                                        {
                                            return false;
                                        }
                                        else 
                                        {
                                            return true;
                                        }
                                        return result;
                                    }
                                }
                            return result;
                            }
                            else 
                            {
                                return true;
                            }
                        return result;
                        }
                        else
                        {
                            return false;
                        }
                    return result;
                    }
                return result;
                }
                else
                {
                    return false;
                }
            return result;
            }
            else
            {
                return false;
            }
        return result;
        }
        else
        {
            return false;
        }
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
