using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpawnBlock : MonoBehaviour
{
    GameObject block;
    [SerializeField] GameObject block2;
    [SerializeField] GameObject block4;
    [SerializeField] GameObject block8;
    [SerializeField] GameObject block16;
    [SerializeField] GameObject block32;
    [SerializeField] GameObject block64;
    [SerializeField] GameObject block128;
    [SerializeField] GameObject block256;
    [SerializeField] GameObject block512;
    [SerializeField] GameObject block1024;
    [SerializeField] GameObject block2048;
    [SerializeField] GameObject block4096;
    //Następne bloki trzeba będzie dorobić i dodać

    [SerializeField] GameObject FieldSpawner;
    FieldScript FieldScript;
    SpawnField SpawnField;
    public BlockBehaviourScript BlockBehaviourScript;
    public List<GameObject> fields;
    public List<GameObject> blocks;
   
   [SerializeField] GameObject ScoreCounter;
   ScoreCounterScript ScoreCounterScript;

    int movedBlockCounter;
    int blockID = 0;
    int unmovableBlockCounter;
    int readyBlockCounter;
    int finishedMoveCounter;

    public int randomX;
    public int randomY;


    void Start()
    {
        ScoreCounter = GameObject.Find("ScoreCounter");
        ScoreCounterScript = ScoreCounter.GetComponent<ScoreCounterScript>();

        SpawnField = FieldSpawner.GetComponent<SpawnField>();
        fields = SpawnField.fields;

        randomX = Random.Range(1, SpawnField.PodajSzerokoscPlanszy-1);
        randomY = Random.Range(1, SpawnField.PodajWysokoscPlanszy-1);

        block = Instantiate(block2);
        blocks.Add(block);
        // block.GetComponent<BlockBehaviourScript>().AfterSpawn(randomX, randomY);
        block.gameObject.name = "block" + blockID;
        blockID++;

 
        



        // SpawnNewBlock();   
         
    }

    void Update()
    {
        unmovableBlockCounter = 0;
        movedBlockCounter = 0;
        finishedMoveCounter = 0;
        readyBlockCounter = 0;
        try
        {
            blocks.TrimExcess();
            foreach(GameObject block in blocks)
            {
                if (block != null)
                {
                    BlockBehaviourScript = block.GetComponent<BlockBehaviourScript>();
                    if (BlockBehaviourScript.unmovable == true)
                    {
                        unmovableBlockCounter++;
                        if(BlockBehaviourScript.moved == true)
                        {
                            movedBlockCounter++;
                        }
                    }

                    if (BlockBehaviourScript.unmovable == true && BlockBehaviourScript.readyToMove == true)
                    {
                        readyBlockCounter++;
                        // Debug.Log("Ten jest gotowy");
                    }

                    if(BlockBehaviourScript.unmovable == true && BlockBehaviourScript.finishedMove == true)
                    {
                        finishedMoveCounter++;
                    }

                }

            }
            if (unmovableBlockCounter == blocks.Count && movedBlockCounter > 0)
            {
                
                

              
                if(readyBlockCounter == blocks.Count)
                {
                    // Debug.Log("Wszystkie bloki gotowe do ruchu");
                    foreach(GameObject block in blocks)
                    {
                        block.GetComponent<BlockBehaviourScript>().executeMove();
                        // Debug.Log("Execute move");
                    }
                }
            }

            else if (unmovableBlockCounter == blocks.Count && movedBlockCounter == 0)
            {
                foreach(GameObject block in blocks)
                {
                    block.GetComponent<BlockBehaviourScript>().unmovable = false;
                    block.GetComponent<BlockBehaviourScript>().moved = false;
                    block.GetComponent<BlockBehaviourScript>().readyToMove = false;
                    block.GetComponent<BlockBehaviourScript>().readyToBeDestroyed = false;
                    block.GetComponent<BlockBehaviourScript>().moveExecuting = false;
                    block.GetComponent<BlockBehaviourScript>().finishedMove = false;
                    block.GetComponent<BlockBehaviourScript>().dir = "null";
                }
            }

            if(finishedMoveCounter == blocks.Count)
                    {
                        // Debug.Log("Wszyscy skończyli");
                        foreach(GameObject block in blocks)
                        {
                            block.GetComponent<BlockBehaviourScript>().executeLevelUp();
                            // Debug.Log("Execute Level Up");
                        }

                        SpawnNewBlock();
                        blocks.TrimExcess();
                        foreach(GameObject block in blocks)
                        {
                            Debug.Log("X: " + (block.GetComponent<BlockBehaviourScript>().TableNumberX) + "  Y: " + (block.GetComponent<BlockBehaviourScript>().TableNumberY));
                            // Debug.Log("Clear all blocks");
                            block.GetComponent<BlockBehaviourScript>().unmovable = false;
                            block.GetComponent<BlockBehaviourScript>().moved = false;
                            block.GetComponent<BlockBehaviourScript>().readyToMove = false;
                            block.GetComponent<BlockBehaviourScript>().readyToBeDestroyed = false;
                            block.GetComponent<BlockBehaviourScript>().moveExecuting = false;
                            block.GetComponent<BlockBehaviourScript>().finishedMove = false;
                            block.GetComponent<BlockBehaviourScript>().dir = "null";
                        }



                    }
        }
        catch{}
    }
    
    public void SpawnNewBlock()
    {
        int fieldCounter = 0;
        bool blockSpawned = false;



        while (fieldCounter < fields.Count && blockSpawned == false)
            {
                int randomPosition = Random.Range(0, fields.Count-1);
                FieldScript = fields[randomPosition].GetComponent<FieldScript>();
                if (FieldScript.checkedForSpawnPurpose == false)
                {
                    if (FieldScript.isWall == true)
                    {
                        fieldCounter++;
                        FieldScript.checkedForSpawnPurpose = true;
                    }
                    else if (FieldScript.isWall == false)
                    {
                        if(FieldScript.isTaken == true)
                        {
                            fieldCounter++;
                            FieldScript.checkedForSpawnPurpose = true;
                        }
                        else if (FieldScript.isTaken == false)
                        {
                            int randomBlock = Random.Range(1, 2);
                            if(randomBlock == 1) {block = Instantiate(block2);}
                            else if(randomBlock == 2) {block = Instantiate(block4);}
        
                            
                            blocks.Add(block);
                            block.GetComponent<BlockBehaviourScript>().AfterSpawn(FieldScript.TableNumberX, FieldScript.TableNumberY);
                            block.gameObject.name = "block" + blockID;
                            blockID++;
                            blockSpawned = true;
                            
                            ClearAfterSpawn();
                            CheckForGameOver();
                        }
                    }

                }
                
            }
    }

    void ClearAfterSpawn()
    {
        foreach (GameObject field in fields)
        {
           FieldScript = field.gameObject.GetComponent<FieldScript>();
           FieldScript.checkedForSpawnPurpose = false;
        }
    }

    void CheckForGameOver()
    {
        int canMove = 0;
        foreach(GameObject block in blocks)
        {
            BlockBehaviourScript BBH = block.GetComponent<BlockBehaviourScript>();
            foreach (GameObject neighbourBlock in blocks)
            {
                BlockBehaviourScript nBBH = neighbourBlock.GetComponent<BlockBehaviourScript>();
                if(BBH.value == nBBH.value)
                {
                    if(BBH.TableNumberX == nBBH.TableNumberX && BBH.TableNumberY == nBBH.TableNumberY+1){canMove++;}
                    if(BBH.TableNumberY == nBBH.TableNumberY && BBH.TableNumberX == nBBH.TableNumberX+1){canMove++;}
                }
            }
        }
        foreach(GameObject field in fields)
        {
            FieldScript FS = field.GetComponent<FieldScript>();
            if(FS.isTaken == false && FS.isWall == false){canMove++;}
        }
        if(canMove == 0) {Debug.Log("TRUE GAME OVER");}
    }

    public void BlockLevelUp(int x, int y, int value) //#TODO This function can be optimized.
    {
        
        if(value == 2){block = Instantiate(block4); ScoreCounterScript.AddPoints(4);}
        else if(value == 4){block = Instantiate(block8); ScoreCounterScript.AddPoints(8);}
        else if(value == 8){block = Instantiate(block16); ScoreCounterScript.AddPoints(16);}
        else if(value == 16){block = Instantiate(block32); ScoreCounterScript.AddPoints(32);}
        else if(value == 32){block = Instantiate(block64); ScoreCounterScript.AddPoints(64);}
        else if(value == 64){block = Instantiate(block128); ScoreCounterScript.AddPoints(128);}
        else if(value == 128){block = Instantiate(block256); ScoreCounterScript.AddPoints(256);}
        else if(value == 256){block = Instantiate(block512); ScoreCounterScript.AddPoints(512);}
        else if(value == 512){block = Instantiate(block1024); ScoreCounterScript.AddPoints(1024);}
        else if(value == 1024){block = Instantiate(block2048); ScoreCounterScript.AddPoints(2048);}
        else if(value == 2048){block = Instantiate(block4096); ScoreCounterScript.AddPoints(4096);}
        
        
        blocks.Add(block);
        block.GetComponent<BlockBehaviourScript>().AfterSpawn(x, y);
        block.GetComponent<BlockBehaviourScript>().unmovable = false;
        block.GetComponent<BlockBehaviourScript>().moved = false;
        block.GetComponent<BlockBehaviourScript>().cantLevelUpNow = false;
        block.gameObject.name = "block" + blockID;
        blockID++; 


    }

    public void RemoveBlockFromList(GameObject block)
    {
        blocks.Remove(block);
    }
}


    

