using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalBRPowerUps : MonoBehaviour
{
LocalBRSpawnBlock LocalBRSpawnBlock;
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
int FreeFieldID = 0;
Vector2 spawnPlace;
int lastField;

public bool isChosing = false;

public int currentPointerOwnerID;
    
    
    void Start()
    {
        LocalBRSpawnBlock = GetComponent<LocalBRSpawnBlock>();
    }

    public IEnumerator PositionSelectorForBlockPlacing(List<FieldScript> GivenFields)
    {
        isChosing = true;
        FreeFields = GivenFields;
        isPointerInUse = true;

        positionPointer = Instantiate(Pointer); 
        FreeFieldID = 0;
        lastField = FreeFields.Count - 1;
        spawnPlace = new Vector2(FreeFields[0].positionX, FreeFields[0].positionY);        
        positionPointer.GetComponent<Transform>().position = spawnPlace;
        do
        {
            isMovingPointer();
        }
        while(isChosing == true);

        
        yield return null;

    }
    
    public void MovePointer()
    {
        spawnPlace = new Vector2(FreeFields[FreeFieldID].positionX, FreeFields[FreeFieldID].positionY);
        positionPointer.GetComponent<Transform>().position = spawnPlace;
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
        // if(isPointerInUse)
        // {
        
        //     if(Input.GetButtonDown("D") || Input.GetButtonDown("MoveRight")){FreeFieldID++;}
        //     if (Input.GetButtonDown("A") || Input.GetButtonDown("MoveLeft")){FreeFieldID--;}
        //     if(FreeFieldID <= -1){FreeFieldID = lastField;}
        //     else if(FreeFieldID >= FreeFields.Count){FreeFieldID = 0;}
        //     else{MovePointer();}

            
          
        // }
       
       
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
