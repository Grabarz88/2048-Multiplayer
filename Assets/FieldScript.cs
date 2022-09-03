using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldScript : MonoBehaviour
{

    [SerializeField] int TableNumberX;
    [SerializeField] int TableNumberY;

    [SerializeField] float positionX;
    [SerializeField] float positionY;

    [SerializeField] bool isTaken;
    [SerializeField] public bool isWall;
    
<<<<<<< HEAD
    void Start() 
    {
        BlockSpawner = GameObject.Find("BlockSpawner"); // Pamiętaj tu zmienić na BlockSpawner(clone) jak już zrobisz jakieś menu albo coś    
    }
    
    void Update()
    {
        blocks = BlockSpawner.GetComponent<SpawnBlock>().blocks;
        foreach(GameObject block in blocks)
        {
            if (block != null)
            {
            BlockBehaviourScript = block.GetComponent<BlockBehaviourScript>();
            if(BlockBehaviourScript.CurrentXTablePosition == TableNumberX && BlockBehaviourScript.CurrentYTablePosition == TableNumberY)
            {
                isTaken = true;
            }
            }
        }
    }

=======
>>>>>>> parent of c9698da (Big Update. Finding issues tomorrow)

    public void PositionSetter(float X, float Y)
    {
        positionX = X;
        positionY = Y;

        this.gameObject.transform.position = new Vector2(positionX, positionY);
    }

    public float PositionXGetter() {return positionX;}
    public float PositionYGetter() {return positionY;}

    public void TableNumberSetter(int TableX, int TableY)
    {
        TableNumberX = TableX;
        TableNumberY = TableY;
    }

    public int TableXGetter() {return TableNumberX;}
    public int TableYGetter() {return TableNumberY;}

    public bool IsTaken() {return isTaken;}
    public bool IsWall() {return isWall;}
}
