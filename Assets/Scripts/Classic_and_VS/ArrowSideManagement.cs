using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSideManagement : MonoBehaviour
{


[SerializeField] GameObject FieldSpawner;
[SerializeField] SpawnField SpawnField;
[SerializeField] GameObject CustomSetter;
[SerializeField] GameObject RightArrow;
[SerializeField] GameObject LeftArrow;
[SerializeField] GameObject UpArrow;
[SerializeField] GameObject DownArrow;
GameObject BlockSpawner;
SpawnBlock SpawnBlock;
LocalVsSpawnBlock LocalVsSpawnBlock;   
BlockBehaviourScript BlockBehaviourScript;
LocalVSBlockBehaviourScript LocalVSBlockBehaviourScript;
public List<GameObject> blocks;

void Start()
{
    BlockSpawner = GameObject.Find("BlockSpawner");
    if(BlockSpawner != null)
    {
        SpawnBlock = BlockSpawner.GetComponent<SpawnBlock>();
    }
    else 
    {
        BlockSpawner = GameObject.Find("LocalVSBlockSpawner");
        LocalVsSpawnBlock = BlockSpawner.GetComponent<LocalVsSpawnBlock>();
    }


    if(GameObject.Find("CustomSetter"))
    {
        // RightArrow.GetComponent<RectTransform>().position = new Vector3(0, 0 ,0);
        CustomSetter = GameObject.Find("CustomSetter");
        int X = (CustomSetter.GetComponent<CustomSetterScript>().X);
        int Y = (CustomSetter.GetComponent<CustomSetterScript>().Y);
        if(X>6 || Y>6)
        {
            //Tutaj popracować nad tym. Dodać jakąś operację modulo
            RightArrow.GetComponent<RectTransform>().position = new Vector3((X-1)*32+3  , (Y-1)*16 ,0);
            LeftArrow.GetComponent<RectTransform>().position = new Vector3(-3, (Y-1)*16, 0);
            UpArrow.GetComponent<RectTransform>().position = new Vector3((X-1)*16, (Y-1)*32+3 ,0);
            DownArrow.GetComponent<RectTransform>().position = new Vector3((X-1)*16, -2 ,0);
            
        }
    }
     
}

public void changeDirForRight()
{
    sendDirToBlocks("right");
}

public void changeDirForLeft()
{
    sendDirToBlocks("left");
}

public void changeDirForUp()
{
    sendDirToBlocks("up");
}

public void changeDirForDown()
{
    sendDirToBlocks("down");
}

public void sendDirToBlocks(string dir)
{
    if(SpawnBlock != null)
    {
        blocks = SpawnBlock.blocks;
        foreach(GameObject block in blocks)
        {
            BlockBehaviourScript = block.GetComponent<BlockBehaviourScript>();
            if (dir == "right"){BlockBehaviourScript.dir = "right";}
            else if (dir == "left"){BlockBehaviourScript.dir = "left";}  
            else if (dir == "up"){BlockBehaviourScript.dir = "up";}  
            else if (dir == "down"){BlockBehaviourScript.dir = "down";} 
        }
    }
    else
    {
        blocks = LocalVsSpawnBlock.blocks;
        foreach(GameObject block in blocks)
        {
            LocalVSBlockBehaviourScript = block.GetComponent<LocalVSBlockBehaviourScript>();
            if (dir == "right"){LocalVSBlockBehaviourScript.dir = "right";}
            else if (dir == "left"){LocalVSBlockBehaviourScript.dir = "left";}  
            else if (dir == "up"){LocalVSBlockBehaviourScript.dir = "up";}  
            else if (dir == "down"){LocalVSBlockBehaviourScript.dir = "down";} 
        }
    }
    blocks.TrimExcess();

}
}
