using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    SpawnField SpawnField;
    FieldScript FieldScript;
    public BlockBehaviourScript BlockBehaviourScript;
    public List<GameObject> fields;
    public List<GameObject> blocks;
    int placableMinX;
    int placableMaxX;
    int placableMinY;
    int placableMaxY;
    
    bool busy;
    bool updating;
    int allBlocksNumber = 1;

    void Start()
    {
        SpawnField = FieldSpawner.GetComponent<SpawnField>();
        fields = SpawnField.fields;
        
        
        GameObject block0 = Instantiate(block2);
        blocks.Add(block0);
        block0.GetComponent<BlockBehaviourScript>().AfterSpawn(2, 2);

        

        // BARDZO SŁABY POMYSŁ NA ZNAJDYWANIE PRZEDZIAŁU DOSTĘPNYCH PÓL
        //-------------------------------------------------------------
        // placableMinX = SpawnField.PodajSzerokoscPlanszy;
        // placableMaxX = 0;
        // placableMinY = SpawnField.PodajWysokoscPlanszy;
        // placableMaxY = 0;
        // foreach(GameObject field in Fields)
        // {
        //     if (field.GetComponent<FieldScript>().isWall == false)
        //     {
        //         int thisFieldX = field.GetComponent<FieldScript>().PositionXGetter();
        //         int thisFieldY = field.GetComponent<FieldScript>().PositionYGetter();
        //         if (thisFieldX < placableMinX)
        //         {
        //             placableMinX = thisFieldX;
        //         }
        //         if (thisFieldX > placableMaxX)
        //         {
        //             placableMaxX = thisFieldX;
        //         }
        //         if (thisFieldY < placableMinY)
        //         {
        //             placableMinY = thisFieldY;
        //         }
        //         if (thisFieldY > placableMaxY)
        //         {
        //             placableMaxY = thisFieldY;
        //         }
        //      }
        //  }
        //-------------------------------------------------------------    
            
    
        }

        //ZRÓB KOD, KTÓRY BĘDZIE LOSOWAŁ LICZBĘ Z PRZEDZIAŁU OD 0 DO OSTATNIEJ POZYCJI W LIŚCIE PÓL/
        //POTEM SPRAWDZAJ, CZY DANE POLE JEST ZAJĘTE LUB ŚCIANĄ.
        //JEDNOCZEŚNIE Z KAŻDYM LOSOWANIEM RÓB +1 DO LICZNIKA SPRAWDZONYCH PÓL.
        //JEŻELI ZNAJDZIESZ DOWOLNE WOLNE POLE, WYZERUJ LICZNIK I POSTAW TAM KAFELEK.
        //JEŻELI LICZNIK DOJDZIE DO OSTATNIEJ POZYCJI W LIŚCIE ZAKOŃCZ GRĘ.
        
        
        void SpawnNewBlocks()
        {
            busy = true;
            foreach(GameObject block in blocks)
            {
                BlockBehaviourScript = block.GetComponent<BlockBehaviourScript>();
                BlockBehaviourScript.moving = false;
                BlockBehaviourScript.unmovable = false;
            }

            int fieldCounter = 0;
            bool blockSpawned = false;

            while (fieldCounter < fields.Count && blockSpawned == false)
            {
                int randomPosition = Random.Range(0, fields.Count-1);
                FieldScript = fields[randomPosition].GetComponent<FieldScript>();
                if (FieldScript.checkedForSpawnPurpose == false)
                {
                    if (FieldScript.IsWall() == true)
                    {
                        fieldCounter++;
                    }
                    else if (FieldScript.IsWall() == false)
                    {
                        if(FieldScript.IsTaken() == true)
                        {
                            fieldCounter++;
                        }
                        else if (FieldScript.IsTaken() == false)
                        {
                            GameObject block = Instantiate(block2);
                            blocks.Add(block);
                            block.GetComponent<BlockBehaviourScript>().AfterSpawn(FieldScript.TableXGetter(), FieldScript.TableYGetter());
                            block.gameObject.name = "block" + allBlocksNumber;
                            allBlocksNumber++;
                            blockSpawned = true;

                        }
                    }

                }
                
            }
            busy = false;
        }



    
    public void checkSpawnReady()
    {
        int blockCounter = 0;
        foreach(GameObject block in blocks)
        {
            BlockBehaviourScript = block.GetComponent<BlockBehaviourScript>();
            if (BlockBehaviourScript.isUnMovable() == true) {blockCounter++;}
        }
        if (blockCounter == blocks.Count) {SpawnNewBlocks();}
    }

    public void levelUp(int X, int Y, int previousValue)
    {
        busy = true;
        foreach (GameObject block in blocks)
        {
            BlockBehaviourScript = block.GetComponent<BlockBehaviourScript>();
            if (BlockBehaviourScript.CurrentXTablePosition == X && BlockBehaviourScript.CurrentYTablePosition == Y)
            {
                Destroy(block);
            }
            
            // if(value == 2){Instantiate(block4);}
        }
        busy = false;
    }

    }


    
