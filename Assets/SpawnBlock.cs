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
    //Następne bloki trzeba będzie dorobić i dodać

    [SerializeField] GameObject FieldSpawner;
    FieldScript FieldScript;
    SpawnField SpawnField;
    public BlockBehaviourScript BlockBehaviourScript;
    public List<GameObject> fields;
    public List<GameObject> blocks;
   
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
                    }
                }

            }
            if (unmovableBlockCounter == blocks.Count)
            {
                SpawnNewBlock();
                blocks.TrimExcess();
                foreach(GameObject block in blocks)
                {
                    block.GetComponent<BlockBehaviourScript>().unmovable = false;
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
                            GameObject block = Instantiate(block2);
                            blocks.Add(block);
                            block.GetComponent<BlockBehaviourScript>().AfterSpawn(FieldScript.TableNumberX, FieldScript.TableNumberY);
                            block.gameObject.name = "block" + blockID;
                            blockID++;
                            blockSpawned = true;
                            Debug.Log("Zespawnowałem blok");
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

    public void BlockLevelUp(int x, int y, string dir)
    {
        GameObject block = Instantiate(block4);
        blocks.Add(block);
        block.GetComponent<BlockBehaviourScript>().AfterSpawn(x, y);
        block.GetComponent<BlockBehaviourScript>().dir = dir;
        block.GetComponent<BlockBehaviourScript>().unmovable = false;
        block.gameObject.name = "block" + blockID;
        blockID++; 


    }

    public void RemoveBlockFromList(GameObject block)
    {
        blocks.Remove(block);
    }
}


    

