using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpawnBlock : MonoBehaviour
{
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
   
    int movedBlockCounter;
    int blockID = 0;
    int unmovableBlockCounter;

    void Start()
    {
        SpawnField = FieldSpawner.GetComponent<SpawnField>();
        fields = SpawnField.fields;    
    }

    void Update()
    {
        unmovableBlockCounter = 0;
        movedBlockCounter = 0;
        try
        {
            blocks.TrimExcess();
            foreach(GameObject block in blocks)
            {
                if (block != null)
                {
                    if (block.GetComponent<BlockBehaviourScript>().unmovable == true)
                    {
                        unmovableBlockCounter++;
                        Debug.Log("unmovableBlockCounter: " + unmovableBlockCounter);
                        Debug.Log("blocks.Count: " + blocks.Count);
                        if(block.GetComponent<BlockBehaviourScript>().moved == true)
                        {
                            movedBlockCounter++;
                        }
                    }
                }

            }
            if (unmovableBlockCounter == blocks.Count && movedBlockCounter > 0)
            {
                SpawnNewBlock();
                blocks.TrimExcess();
                foreach(GameObject block in blocks)
                {
                    block.GetComponent<BlockBehaviourScript>().unmovable = false;
                    block.GetComponent<BlockBehaviourScript>().moved = false;
                    block.GetComponent<BlockBehaviourScript>().cantLevelUpNow = false;
                }
            
            
            }
            else if (unmovableBlockCounter == blocks.Count && movedBlockCounter == 0)
            {
                foreach(GameObject block in blocks)
                {
                    block.GetComponent<BlockBehaviourScript>().unmovable = false;
                    block.GetComponent<BlockBehaviourScript>().moved = false;
                    block.GetComponent<BlockBehaviourScript>().cantLevelUpNow = true;
                }
            }
        }
        catch{}
    }
    
    public void SpawnNewBlock()
    {
        Debug.Log("Rozpoczynam spawnowanie");


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
                    }
                    else if (FieldScript.isWall == false)
                    {
                        if(FieldScript.isTaken == true)
                        {
                            fieldCounter++;
                        }
                        else if (FieldScript.isTaken == false)
                        {
                            GameObject block = new GameObject();
                            int randomBlock = Random.Range(1, 2);
                            if(randomBlock == 1) {block = Instantiate(block2);}
                            else if(randomBlock == 2) {block = Instantiate(block4);}
        
                            
                            blocks.Add(block);
                            block.GetComponent<BlockBehaviourScript>().AfterSpawn(FieldScript.TableNumberX, FieldScript.TableNumberY);
                            block.gameObject.name = "block" + blockID;
                            blockID++;
                            blockSpawned = true;
                            
                            ClearAfterSpawn();
                        }
                    }

                }
                
            }
        if(fieldCounter == fields.Count)
        {
            Debug.Log("GAME OVER");
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

    public void BlockLevelUp(int x, int y, int value) //#TODO This function can be optimized.
    {
        GameObject block = new GameObject();
        if(value == 2){block = Instantiate(block4);}
        else if(value == 4){block = Instantiate(block8);}
        else if(value == 8){block = Instantiate(block16);}
        else if(value == 16){block = Instantiate(block32);}
        else if(value == 32){block = Instantiate(block64);}
        else if(value == 64){block = Instantiate(block128);}
        else if(value == 128){block = Instantiate(block256);}
        else if(value == 256){block = Instantiate(block512);}
        else if(value == 512){block = Instantiate(block1024);}
        else if(value == 1024){block = Instantiate(block2048);}
        else if(value == 2048){block = Instantiate(block4096);}
        
        
        blocks.Add(block);
        block.GetComponent<BlockBehaviourScript>().AfterSpawn(x, y);
        block.GetComponent<BlockBehaviourScript>().unmovable = true;
        block.GetComponent<BlockBehaviourScript>().moved = true;
        block.GetComponent<BlockBehaviourScript>().cantLevelUpNow = true;
        block.gameObject.name = "block" + blockID;
        blockID++; 


    }

    public void RemoveBlockFromList(GameObject block)
    {
        blocks.Remove(block);
    }
}


    

