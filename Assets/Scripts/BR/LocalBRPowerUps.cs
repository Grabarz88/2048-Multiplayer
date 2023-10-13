using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalBRPowerUps : MonoBehaviour
{
LocalBRSpawnBlock SpawnBlock;
[SerializeField] public GameObject Pointer; 
public GameObject positionPointer; 
public List<GameObject> fields;
public List<GameObject> blocks;
public List<GameObject> P1Blocks;
public List<GameObject> P2Blocks;
public List<GameObject> P3Blocks;
public List<GameObject> P4Blocks;
public List<GameObject> NeutralBlocks;

public bool isPointerInUse = false;
public List<FieldScript> FreeFields;
public List<FieldScript> SortedFields;
public int FreeFieldID = 0;
public int blocksLeftToPlace = 2;
Vector2 spawnPlace;
int lastField;

public bool isChosing = false;
 GameObject previouslyPlacedBlock;
public int currentPointerOwnerID;
public int colorToUse;

[SerializeField] GameObject SettingsButton;

bool needToChangePointerPlace = false;
    
    
    void Start()
    {
        SpawnBlock = GetComponent<LocalBRSpawnBlock>();
    }

    public void PositionSelectorForBlockPlacing(List<FieldScript> GivenFields, int player, int color)
    {
        Debug.Log("Power Up P1Blocks: " + SpawnBlock.P1Blocks.Count);
        Debug.Log("Power UP P2Blocks: " + SpawnBlock.P2Blocks.Count);
        blocksLeftToPlace = 2;
        previouslyPlacedBlock = null;
        currentPointerOwnerID = player;
        colorToUse = color;
        isChosing = true;
        FreeFields = new List<FieldScript>(GivenFields);
        SortedFields.Clear();
        int sortingX = 1;
        int sortingY = 1;
        int sortingID = 0;
        
        do
        {
            do
            {
                do
                {
                    foreach(FieldScript field in FreeFields)
                    {
                        if(field.TableNumberX == sortingX)
                        {
                            if(field.TableNumberY == sortingY)
                            {
                                SortedFields.Add(field);
                                sortingID++;
                            }
                        }
                    }
                    sortingY++;
                }while(sortingY < 21);
                sortingX++;
                sortingY = 1;
            }while(sortingX <21);
        }while(sortingID < FreeFields.Count);


        
        isPointerInUse = true;
        positionPointer = Instantiate(Pointer); 
        FreeFieldID = 0;
        lastField = SortedFields.Count - 1;
        spawnPlace = new Vector2(SortedFields[0].positionX, SortedFields[0].positionY);        
        positionPointer.GetComponent<Transform>().position = spawnPlace;
        

        // komenda na zakończenie działania pointera 
        // SpawnBlock.placingIsFinished = true;
    }
    
   

    public void isMovingPointer()
    {
        if(Input.GetButtonDown("D") || Input.GetButtonDown("MoveRight")){FreeFieldID++;}
        if (Input.GetButtonDown("A") || Input.GetButtonDown("MoveLeft")){FreeFieldID--;}
        if(FreeFieldID <= -1){FreeFieldID = lastField;}
        else if(FreeFieldID >= FreeFields.Count){FreeFieldID = 0;}
        else{MovePointer();}
    }
    
    void Update()
    {
    
        if(isChosing == true && isPointerInUse == true)
        {
            if(Input.GetButtonDown("D") || Input.GetButtonDown("MoveRight"))
            {
                PointerGoRight();

                // isChosing = false;
                // SpawnBlock.placingIsFinished = true;
            }
            if(Input.GetButtonDown("A") || Input.GetButtonDown("MoveLeft"))
            {
                PoninterGoLeft();
            }

            if(needToChangePointerPlace == true)
            {
                MovePointer();
                needToChangePointerPlace = false;
            }

            if(Input.GetButtonDown("Enter") || Input.GetButtonDown("Spacebar"))
            {
                PointerPlaceBlock();
            }




        }
        // if(isPointerInUse)
        // {
        
        //     if(Input.GetButtonDown("D") || Input.GetButtonDown("MoveRight")){FreeFieldID++;}
        //     if (Input.GetButtonDown("A") || Input.GetButtonDown("MoveLeft")){FreeFieldID--;}
        //     if(FreeFieldID <= -1){FreeFieldID = lastField;}
        //     else if(FreeFieldID >= FreeFields.Count){FreeFieldID = 0;}
        //     else{MovePointer();}

            
          
        // }
       
       
    }

     public void MovePointer()
    {
        spawnPlace = new Vector2(SortedFields[FreeFieldID].positionX,SortedFields[FreeFieldID].positionY);
        positionPointer.GetComponent<Transform>().position = spawnPlace;
    }

    public void PointerGoRight()
    {
        if(SettingsButton.GetComponent<ShowSettingsPanel>().isPauseActive == false)
        {
            FreeFieldID++;
            if(FreeFieldID >= FreeFields.Count){FreeFieldID = 0;}
            needToChangePointerPlace = true;
        }
    }

    public void PointerPlaceBlock()
    {
        if(SettingsButton.GetComponent<ShowSettingsPanel>().isPauseActive == false)
        {
            if(blocksLeftToPlace == 1)
            {
                previouslyPlacedBlock = GameObject.Find("block" + (SpawnBlock.blockID-1));
                if (previouslyPlacedBlock.GetComponent<LocalBRBlockBehaviourScript>().TableNumberX == SortedFields[FreeFieldID].TableNumberX && previouslyPlacedBlock.GetComponent<LocalBRBlockBehaviourScript>().TableNumberY == SortedFields[FreeFieldID].TableNumberY)
                {
                    Destroy(previouslyPlacedBlock);
                    SpawnBlock.InstantiateThisColorWithThisOwner(colorToUse, 4, currentPointerOwnerID, SortedFields[FreeFieldID].TableNumberX, SortedFields[FreeFieldID].TableNumberY);
                }
                else
                {
                    SpawnBlock.InstantiateThisColorWithThisOwner(colorToUse, 2, currentPointerOwnerID, SortedFields[FreeFieldID].TableNumberX, SortedFields[FreeFieldID].TableNumberY);
                }
            }
            else
            { 
                SpawnBlock.InstantiateThisColorWithThisOwner(colorToUse, 2, currentPointerOwnerID, SortedFields[FreeFieldID].TableNumberX, SortedFields[FreeFieldID].TableNumberY);
            }
            blocksLeftToPlace--;
            if(blocksLeftToPlace <= 0)
            {
                Destroy(positionPointer);
                isChosing = false;
                isPointerInUse = false;
                SpawnBlock.placingIsFinished = true;
            }
        }
    }

    public void PoninterGoLeft()
    {
        if(SettingsButton.GetComponent<ShowSettingsPanel>().isPauseActive == false)
        {
        FreeFieldID--;
        if(FreeFieldID <= -1){FreeFieldID = lastField;}
        needToChangePointerPlace = true;
        }
    }

    
    // public IEnumerator PositionSelectorForBlockPlacing(List<FieldScript> FreeFields)
    // {
    //     GameObject positionPointer = Instantiate(Pointer); 
    //     int FreeFieldID = 0;
    //     int lastField = FreeFields.Count - 1;
    //     Vector2 spawnPlace = new Vector2(FreeFields[0].positionX, FreeFields[0].positionY);        
    //     positionPointer.GetComponent<Transform>().position = spawnPlace;

    //     bool placingFinished = false;
    //     do
    //     {
    //         if(Input.GetButtonDown("D") || Input.GetButtonDown("MoveRight")){FreeFieldID++;}
    //         if (Input.GetButtonDown("A") || Input.GetButtonDown("MoveLeft")){FreeFieldID++;}
    //         if(FreeFieldID <= -1){FreeFieldID = lastField;}
    //         if(FreeFieldID >= FreeFields.Count){FreeFieldID = 0;}

    //         spawnPlace = new Vector2(FreeFields[FreeFieldID].positionX, FreeFields[FreeFieldID].positionY);
    //         positionPointer.GetComponent<Transform>().position = spawnPlace;
    //         Debug.Log("Ja pracuję");

    //     }while(placingFinished == false);
        
    //     Debug.Log("Koniec pracy w PowerUpach dla gracza");
        
    //     yield return null;
    // }
}
