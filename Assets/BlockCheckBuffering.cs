using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCheckBuffering : MonoBehaviour
{
   
   [SerializeField] GameObject BlockSpawner;
   BlockBehaviourScript BlockBehaviourScript;
   public List<GameObject> blocks;
   


    void Update()
    {
        blocks = BlockSpawner.GetComponent<SpawnBlock>().blocks;
        int blockCounter = 0;
        foreach(GameObject block in blocks)
        {
                if(block != null)
                {
                    BlockBehaviourScript = block.GetComponent<BlockBehaviourScript>();
                    if (BlockBehaviourScript.isUnMovable() == true) {blockCounter++;}
                }
           
        }
        if (blockCounter == blocks.Count) {BlockSpawner.GetComponent<SpawnBlock>().SpawnNewBlocks();}
    }
}
