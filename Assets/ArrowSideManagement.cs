using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSideManagement : MonoBehaviour
{
    
GameObject BlockSpawner;
SpawnBlock SpawnBlock;
public List<GameObject> blocks;
string dir;

void Start()
{
    BlockSpawner = GameObject.Find("BlockSpawner");
    SpawnBlock = BlockSpawner.GetComponent<SpawnBlock>(); 
}

public void changeDirForRight()
{
    dir ="right";
    sendDirToBlocks("right");
}

public void changeDirForLeft()
{
    dir ="left";
    sendDirToBlocks("left");
}

public void changeDirForUp()
{
    dir ="up";
    sendDirToBlocks("up");
}

public void changeDirForDown()
{
    dir ="down";
    sendDirToBlocks("down");
}

public void sendDirToBlocks(string direction)
{
    blocks = SpawnBlock.blocks;
    blocks.TrimExcess();
    foreach(GameObject block in blocks)
    {
        BlockBehaviourScript BlockBehaviourScript = block.GetComponent<BlockBehaviourScript>();
        if (direction == "right"){BlockBehaviourScript.dir = "right";}
        else if (direction == "left"){BlockBehaviourScript.dir = "left";}  
        else if (direction == "up"){BlockBehaviourScript.dir = "up";}  
        else if (direction == "down"){BlockBehaviourScript.dir = "down";} 
    }
}
}
