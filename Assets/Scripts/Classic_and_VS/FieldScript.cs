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

    public bool checkedForSpawnPurpose = false;
    
    void Start() 
    {        
        // BlockSpawner = GameObject.Find("BlockSpawner"); 
        // Pamiętaj tu zmienić na BlockSpawner(clone) jak już zrobisz jakieś menu albo coś    
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
    
    


    
}
