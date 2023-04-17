using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSideManagement : MonoBehaviour
{
    
GameObject BlockSpawner;
SpawnBlock SpawnBlock;
public List<GameObject> blocks;

void Start()
{
    BlockSpawner = GameObject.Find("BlockSpawner");
    SpawnBlock = BlockSpawner.GetComponent<SpawnBlock>(); 
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
    blocks = SpawnBlock.blocks;
    blocks.TrimExcess();
    foreach(GameObject block in blocks)
    {
        BlockBehaviourScript BlockBehaviourScript = block.GetComponent<BlockBehaviourScript>();
        if (dir == "right"){BlockBehaviourScript.dir = "right";}
        else if (dir == "left"){BlockBehaviourScript.dir = "left";}  
        else if (dir == "up"){BlockBehaviourScript.dir = "up";}  
        else if (dir == "down"){BlockBehaviourScript.dir = "down";} 
    }
}
}
