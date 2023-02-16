using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpawnBlock : MonoBehaviour
{
    public GameObject gameOverPanel;
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

    int idleCounter;
    int finishedSearchingCounter;
    int willMoveCounter;
    int finishedMovingCounter;
    
    int blockID = 0;
    public int randomX;
    public int randomY;

    //----------Te potencjalnie można wyrzucić
    int movedBlockCounter;
    
    int unmovableBlockCounter;
    int readyBlockCounter;
    public int finishedMoveCounter;
    //------------------------------------------



    void Start()
    {
        gameOverPanel.gameObject.SetActive(false);
        ScoreCounter = GameObject.Find("ScoreCounter");
        ScoreCounterScript = ScoreCounter.GetComponent<ScoreCounterScript>();

        SpawnField = FieldSpawner.GetComponent<SpawnField>();
        fields = SpawnField.fields;

        randomX = Random.Range(1, SpawnField.PodajSzerokoscPlanszy-1);
        randomY = Random.Range(1, SpawnField.PodajWysokoscPlanszy-1);

        block = Instantiate(block2);
        blocks.Add(block);
        block.gameObject.name = "block" + blockID;
        blockID++;

 
        



        // SpawnNewBlock();   
         
    }

    void Update()
    {
        // Debug.Log(blocks.Count);
        // try
        // {
            blocks.TrimExcess();
            idleCounter = 0;
            finishedSearchingCounter = 0;
            willMoveCounter = 0;
            finishedMovingCounter = 0;
            foreach(GameObject block in blocks)
            {
                if (block != null)
                {
                    //Sprawdzamy statusy bloków
                    BlockBehaviourScript = block.GetComponent<BlockBehaviourScript>();
                    if (BlockBehaviourScript.idle == true) {idleCounter++;}
                    if (BlockBehaviourScript.finishedSearching == true) {finishedSearchingCounter++;}
                    if (BlockBehaviourScript.willMove == true || BlockBehaviourScript.willBeDestroyed == true) {willMoveCounter++;}
                    if (BlockBehaviourScript.finishedMoving == true) {finishedMovingCounter++;}
                }
            }

            if(idleCounter == blocks.Count)
            {
                CheckForGameOver();
                foreach(GameObject block in blocks)
                {
                    //Mówimy blokom, że mogą się zacząć szukać swoich nowych pozycji
                    BlockBehaviourScript = block.GetComponent<BlockBehaviourScript>();
                    BlockBehaviourScript.executeWaitingForDir();
                }
            }
           
            if(finishedSearchingCounter == blocks.Count && willMoveCounter > 0)
            {
                foreach(GameObject block in blocks)
                {
                    //Mówimy blokom, że mogą zacząć wykonywać ruch
                    BlockBehaviourScript = block.GetComponent<BlockBehaviourScript>();
                    BlockBehaviourScript.executeMove();
                }
            }
            else if(finishedSearchingCounter == blocks.Count && willMoveCounter == 0)
            {
                foreach(GameObject block in blocks)
                {
                    BlockBehaviourScript = block.GetComponent<BlockBehaviourScript>();
                    BlockBehaviourScript.dir = "empty";
                    BlockBehaviourScript.finishedSearching = false;
                    BlockBehaviourScript.idle = true;

                }
            }
            
            if(finishedMovingCounter == blocks.Count)
            {
                foreach(GameObject block in blocks)
                {
                    //Mówimy blokom, że skoro skończyły się ruszać to mogą się zniszczyć jeśli potrzebują i mają się przygotować do następnej kolejki
                    BlockBehaviourScript = block.GetComponent<BlockBehaviourScript>();
                    BlockBehaviourScript.executeLevelUp();
                    BlockBehaviourScript.finishedMoving = false;
                    BlockBehaviourScript.idle = true;
                    BlockBehaviourScript.dir = "empty";
                }
                SpawnNewBlock();
            }
        // }
        // catch{}
    }
    
    public void SpawnNewBlock()
    {
        // Debug.Log("SpawnNewBlock");
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
                            // CheckForGameOver();
                        }
                    }

                }
                
            }
    }

    void ClearAfterSpawn()
    {
        // Debug.Log("ClearAfterSpawn");
        foreach (GameObject field in fields)
        {
           FieldScript = field.gameObject.GetComponent<FieldScript>();
           FieldScript.checkedForSpawnPurpose = false;
        }
    }

    public void CheckForGameOver()
    {
        Debug.Log("CheckForGameOver");
        blocks.TrimExcess();
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
        if(canMove == 0) 
        {
            Debug.Log("TRUE GAME OVER");
            gameOverPanel.gameObject.SetActive(true);
        }
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
        block.GetComponent<BlockBehaviourScript>().dir = "empty";
        block.gameObject.name = "block" + blockID;
        blockID++; 


    }

    public void RemoveBlockFromList(GameObject block)
    {
        // Debug.Log("RemoveBlockFromList");
        blocks.Remove(block);
    }
}


    

