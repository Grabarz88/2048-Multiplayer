using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldScript : MonoBehaviour
{

    [SerializeField] public int TableNumberX;
    [SerializeField] public int TableNumberY;

    [SerializeField] public float positionX;
    [SerializeField] public float positionY;

    [SerializeField] public bool isTaken;
    [SerializeField] public bool isWall;

    [SerializeField] public GameObject FieldSpawner;
    [SerializeField] public int blockOwnerID = -1;
    [SerializeField] public long blockValue = 0;
    [SerializeField] public bool chceckedForBeingFree;
    SpawnFieldBR SpawnField;

    public bool checkedForSpawnPurpose = false;
    
    void Start() 
    {        
        // BlockSpawner = GameObject.Find("BlockSpawner"); 
        // Pamiętaj tu zmienić na BlockSpawner(clone) jak już zrobisz jakieś menu albo coś    
        FieldSpawner = GameObject.Find("FieldSpawner");
        SpawnField = FieldSpawner.GetComponent<SpawnFieldBR>();

    }


    public void TableNumberSetter(int x, int y)
    {
        TableNumberX = x;
        TableNumberY = y;
    }


    public void PositionSetter(float x, float y)
    {
        positionX = x;
        positionY = y;
        this.gameObject.transform.position = new Vector2(positionX, positionY);
        
    }
    
    public void GiveInfoAboutYourself(int owner, long value) // This function is used onyl for BR purposes of checking movement posibilities.
    {
        blockOwnerID = owner;
        blockValue = value;
    }

    public void OnDestroy()
    {
        SpawnField.fields.Remove(this.gameObject);
    }
    


    
}
