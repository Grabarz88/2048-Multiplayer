using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalBRSpawnBlock : MonoBehaviour
{
GameObject block;
FieldScript FieldScript;
SpawnFieldBR SpawnFieldBR;
[SerializeField] GameObject FieldSpawner;
public GameObject gameOverPanel;
[SerializeField] Text winnerAnnouncemenet;
[SerializeField] Text TurnPlayerNumber;
[SerializeField] public GameObject TurnColorPanel;
[SerializeField] Image TurnColorImg;
GameObject ObjectToRememberColors;
ScriptToRememberBRColors ScriptToRememberColors;
public LocalBRBlockBehaviourScript BlockBehaviourScript;

int blockID = 0;
int Player1Color = 0;
int Player2Color = 0;
int Player3Color = 0;
int Player4Color = 0;
public List<GameObject> fields;
public List<GameObject> blocks;
public List<GameObject> P1Blocks;
public List<GameObject> P2Blocks;
public List<GameObject> P3Blocks;

public List<GameObject> P4Blocks;
public List<GameObject> NeutralBlocks;
public bool Player1Turn = true;
public bool Player2Turn = false;
public bool Player3Turn = false;
public bool Player4Turn = false;
public bool Waiting = false;

public bool isPlayer1Playing;
public bool isPlayer2Playing;
public bool isPlayer3Playing;
public bool isPlayer4Playing;

public bool isPreparingFaze = true;
public bool isBRFaze = false;

int idleCounter;
int finishedSearchingCounter;
int willMoveCounter;
int finishedMovingCounter;

public int randomX;
public int randomY;



void Start()
{
    ObjectToRememberColors = GameObject.Find("ObjectToRememberColors");
    ScriptToRememberColors = ObjectToRememberColors.GetComponent<ScriptToRememberBRColors>();
    isPlayer1Playing = ScriptToRememberColors.isPlayer1Playing;
    isPlayer2Playing = ScriptToRememberColors.isPlayer2Playing;
    isPlayer3Playing = ScriptToRememberColors.isPlayer3Playing;
    isPlayer4Playing = ScriptToRememberColors.isPlayer4Playing;
    if(isPlayer1Playing == true)
    {
        Player1Color = ScriptToRememberColors.Player1Color;
        InstantiateThisColorWithThisOwner(Player1Color, 2, 1);
        randomX = Random.Range(1, 5);
        randomY = Random.Range(1, 5);
        block = GameObject.Find("block" + (blockID-1));
        BlockBehaviourScript = block.GetComponent<LocalBRBlockBehaviourScript>();
        BlockBehaviourScript.AfterSpawn(randomX, randomY);
    }
    if(isPlayer2Playing == true)
    {
        Player2Color = ScriptToRememberColors.Player2Color;
        InstantiateThisColorWithThisOwner(Player2Color, 2, 2);
        randomX = Random.Range(1, 5);
        randomX = randomX + 36;
        randomY = Random.Range(1, 5);
        randomY = randomY + 36;
        block = GameObject.Find("block" + (blockID-1));
        BlockBehaviourScript = block.GetComponent<LocalBRBlockBehaviourScript>();
        BlockBehaviourScript.AfterSpawn(randomX, randomY);
    }
    if(isPlayer3Playing == true)
    {
        Player3Color = ScriptToRememberColors.Player3Color;
        InstantiateThisColorWithThisOwner(Player3Color, 2, 3);
        randomX = Random.Range(1, 5);
        randomX = randomX + 72;
        randomY = Random.Range(1, 5);
        randomY = randomY + 72;
        block = GameObject.Find("block" + (blockID-1));
        BlockBehaviourScript = block.GetComponent<LocalBRBlockBehaviourScript>();
        BlockBehaviourScript.AfterSpawn(randomX, randomY);
    }
    if(isPlayer4Playing == true)
    {
        Player4Color = ScriptToRememberColors.Player4Color;
        InstantiateThisColorWithThisOwner(Player4Color, 2, 4);
        randomX = Random.Range(1, 5);
        randomX = randomX + 108;
        randomY = Random.Range(1, 5);
        randomY = randomY + 108;
        block = GameObject.Find("block" + (blockID-1));
        BlockBehaviourScript = block.GetComponent<LocalBRBlockBehaviourScript>();
        BlockBehaviourScript.AfterSpawn(randomX, randomY);
    }

}

void Update()
{
    blocks.TrimExcess();
    idleCounter = 0;
    finishedSearchingCounter = 0;
    willMoveCounter = 0;
    finishedMovingCounter = 0;
    foreach(GameObject block in blocks)
    {
        if (block != null)
        {
            //Sprawdzamy statusy bloków
            BlockBehaviourScript = block.GetComponent<LocalBRBlockBehaviourScript>();
            if (BlockBehaviourScript.idle == true) {idleCounter++;}
            if (BlockBehaviourScript.finishedSearching == true) {finishedSearchingCounter++;}
            if (BlockBehaviourScript.willMove == true || BlockBehaviourScript.willBeDestroyed == true) {willMoveCounter++;}
            if (BlockBehaviourScript.finishedMoving == true) {finishedMovingCounter++;}
        }
    }

    if(idleCounter == blocks.Count)
    {
       Debug.Log("Wszystkie są Idle");
        // CheckForGameOver();
        foreach(GameObject block in blocks)
        {
            //Mówimy blokom, że mogą się zacząć szukać swoich nowych pozycji
            BlockBehaviourScript = block.GetComponent<LocalBRBlockBehaviourScript>();
            if(Player1Turn == true && Waiting == false)
            {
                if(BlockBehaviourScript.OwnerID == 1)
                {
                    BlockBehaviourScript.executeWaitingForDir();
                }
                
            }
            if (Player2Turn == true && Waiting == false)
            {
                if(BlockBehaviourScript.OwnerID == 2)
                {
                    BlockBehaviourScript.executeWaitingForDir();
                }                   
            }
            if (Player3Turn == true && Waiting == false)
            {
                if(BlockBehaviourScript.OwnerID == 3)
                {
                    BlockBehaviourScript.executeWaitingForDir();
                }                   
            }
            if (Player4Turn == true && Waiting == false)
            {
                if(BlockBehaviourScript.OwnerID == 4)
                {
                    BlockBehaviourScript.executeWaitingForDir();
                }                   
            }
        }
        Waiting = true;

    }
    
    if(((Player1Turn == true && finishedSearchingCounter == P1Blocks.Count) || (Player2Turn == true && finishedSearchingCounter == P2Blocks.Count) || (Player3Turn == true && finishedSearchingCounter == P3Blocks.Count) || (Player4Turn == true && finishedSearchingCounter == P4Blocks.Count)) && willMoveCounter > 0)
    {
        foreach(GameObject block in blocks)
        {
            //Mówimy blokom, że mogą zacząć wykonywać ruch
            BlockBehaviourScript = block.GetComponent<LocalBRBlockBehaviourScript>();
            if(Player1Turn == true)                
            {
                if(BlockBehaviourScript.OwnerID == 1)
                {
                    BlockBehaviourScript.executeMove();
                }
            }
            if (Player2Turn == true)
            {
                if(BlockBehaviourScript.OwnerID == 2)
                {
                    BlockBehaviourScript.executeMove();
                }                    
            }
            if (Player3Turn == true)
            {
                if(BlockBehaviourScript.OwnerID == 3)
                {
                    BlockBehaviourScript.executeMove();
                }                    
            }
            if (Player4Turn == true)
            {
                if(BlockBehaviourScript.OwnerID == 4)
                {
                    BlockBehaviourScript.executeMove();
                }                    
            }
            
        }
    }
    else if(((Player1Turn == true && finishedSearchingCounter == P1Blocks.Count) || (Player2Turn == true && finishedSearchingCounter == P2Blocks.Count) || (Player3Turn == true && finishedSearchingCounter == P3Blocks.Count) || (Player4Turn == true && finishedSearchingCounter == P4Blocks.Count)) && willMoveCounter == 0)
    {
        foreach(GameObject block in blocks)
        {
            BlockBehaviourScript = block.GetComponent<LocalBRBlockBehaviourScript>();
            BlockBehaviourScript.dir = "empty";
            BlockBehaviourScript.finishedSearching = false;
            BlockBehaviourScript.idle = true;
            Waiting = false;

        }
    }
    
    if((Player1Turn == true && finishedMovingCounter == P1Blocks.Count) || (Player2Turn == true && finishedMovingCounter == P2Blocks.Count) || (Player3Turn == true && finishedMovingCounter == P3Blocks.Count) || (Player4Turn == true && finishedMovingCounter == P4Blocks.Count))
    {
        foreach(GameObject block in blocks)
        {
            //Mówimy blokom, że skoro skończyły się ruszać to mogą się zniszczyć jeśli potrzebują i mają się przygotować do następnej kolejki
            BlockBehaviourScript = block.GetComponent<LocalBRBlockBehaviourScript>();
            BlockBehaviourScript.executeLevelUp();
            BlockBehaviourScript.finishedMoving = false;
            BlockBehaviourScript.idle = true;
            BlockBehaviourScript.dir = "empty";
        }
        // SpawnNewBlock();
        if(Player1Turn == true)
        {
            if(isPlayer2Playing)
            {
                // TurnColorImg.color = Player2Color;
                Player1Turn = false;
                Player2Turn = true;
                // TurnPlayerNumber.text = "2";
                Waiting = false;
            }
            else if(isPlayer3Playing)
            {
                // TurnColorImg.color = Player3Color;
                Player1Turn = false;
                Player3Turn = true;
                // TurnPlayerNumber.text = "3";
                Waiting = false;
            }
            else if(isPlayer4Playing)
            {
                // TurnColorImg.color = Player4Color;
                Player1Turn = false;
                Player4Turn = true;
                // TurnPlayerNumber.text = "4";
                Waiting = false;
            }
        }
        else if (Player2Turn == true)
        {
            if(isPlayer3Playing)
            {
                // TurnColorImg.color = Player3Color;
                Player2Turn = false;
                Player3Turn = true;
                // TurnPlayerNumber.text = "3";
                Waiting = false;
            }
            else if(isPlayer4Playing)
            {
                // TurnColorImg.color = Player4Color;
                Player2Turn = false;
                Player4Turn = true;
                // TurnPlayerNumber.text = "4";
                Waiting = false;
            }
            else if(isPlayer1Playing)
            {
                // TurnColorImg.color = Player1Color;
                Player2Turn = false;
                Player1Turn = true;
                // TurnPlayerNumber.text = "1";
                Waiting = false;
            }
        }
        else if (Player3Turn == true)
        {
            if(isPlayer4Playing)
            {
                // TurnColorImg.color = Player4Color;
                Player3Turn = false;
                Player4Turn = true;
                // TurnPlayerNumber.text = "4";
                Waiting = false;
            }
            else if(isPlayer1Playing)
            {
                // TurnColorImg.color = Player1Color;
                Player3Turn = false;
                Player1Turn = true;
                // TurnPlayerNumber.text = "1";
                Waiting = false;
            }
            else if(isPlayer2Playing)
            {
                // TurnColorImg.color = Player2Color;
                Player3Turn = false;
                Player2Turn = true;
                // TurnPlayerNumber.text = "2";
                Waiting = false;
            }
        }
        else if (Player4Turn == true)
        {
            if(isPlayer1Playing)
            {
                // TurnColorImg.color = Player1Color;
                Player4Turn = false;
                Player1Turn = true;
                // TurnPlayerNumber.text = "1";
                Waiting = false;
            }
            else if(isPlayer2Playing)
            {
                // TurnColorImg.color = Player2Color;
                Player4Turn = false;
                Player2Turn = true;
                // TurnPlayerNumber.text = "2";
                Waiting = false;
            }
            else if(isPlayer3Playing)
            {
                // TurnColorImg.color = Player3Color;
                Player4Turn = false;
                Player3Turn = true;
                // TurnPlayerNumber.text = "3";
                Waiting = false;
            }
        }
    }     
}



public void InstantiateThisColorWithThisOwner(int color, int value, int owner)
{
    if(isPreparingFaze)
    {
        string value_string;
        string color_string = "n";
        value_string = value.ToString();
        if(color == 1){color_string = "b";}
        else if(color == 2){color_string = "r";}
        else if(color == 3){color_string = "g";}
        else if(color == 4){color_string = "p";}
        else if(color == 5){color_string = "s";}
        GameObject block = (GameObject)Instantiate(Resources.Load("Local/BR/" + value_string + color_string));
        blocks.Add(block);
        if(owner == 1){P1Blocks.Add(block);}
        else if(owner == 2){P2Blocks.Add(block);}
        else if(owner == 3){P3Blocks.Add(block);}
        else if(owner == 4){P4Blocks.Add(block);}
        block.gameObject.GetComponent<LocalBRBlockBehaviourScript>().OwnerID = owner;
        block.gameObject.name = "block" + blockID;
        blockID++;
    }
    else if(isBRFaze)
    {
        
    }
}


public void BlockLevelUp(int x, int y, int value) //#TODO This function can be optimized.
    {
        // if(value == 2){block = Instantiate(block4); ScoreCounterScript.AddPoints(4);}
        // else if(value == 4){block = Instantiate(block8); ScoreCounterScript.AddPoints(8);}
        // else if(value == 8){block = Instantiate(block16); ScoreCounterScript.AddPoints(16);}
        // else if(value == 16){block = Instantiate(block32); ScoreCounterScript.AddPoints(32);}
        // else if(value == 32){block = Instantiate(block64); ScoreCounterScript.AddPoints(64);}
        // else if(value == 64){block = Instantiate(block128); ScoreCounterScript.AddPoints(128);}
        // else if(value == 128){block = Instantiate(block256); ScoreCounterScript.AddPoints(256);}
        // else if(value == 256){block = Instantiate(block512); ScoreCounterScript.AddPoints(512);}
        // else if(value == 512){block = Instantiate(block1024); ScoreCounterScript.AddPoints(1024);}
        // else if(value == 1024){block = Instantiate(block2048); ScoreCounterScript.AddPoints(2048);}
        // else if(value == 2048){block = Instantiate(block4096); ScoreCounterScript.AddPoints(4096);}
        
        // InstantiateThisColorWithThisOwner();
        blocks.Add(block);
        block.GetComponent<BlockBehaviourScript>().AfterSpawn(x, y);
        block.GetComponent<BlockBehaviourScript>().dir = "empty";
        block.gameObject.name = "block" + blockID;
        blockID++; 


    }



















[SerializeField] GameObject NeutralBlock2;
[SerializeField] GameObject NeutralBlock4;
[SerializeField] GameObject NeutralBlock8;
[SerializeField] GameObject NeutralBlock16;
[SerializeField] GameObject NeutralBlock32;
[SerializeField] GameObject NeutralBlock64;
[SerializeField] GameObject NeutralBlock128;
[SerializeField] GameObject NeutralBlock256;
[SerializeField] GameObject NeutralBlock512;
[SerializeField] GameObject NeutralBlock1024;
[SerializeField] GameObject NeutralBlock2048;
[SerializeField] GameObject NeutralBlock4096;
[SerializeField] GameObject NeutralBlock8192;
[SerializeField] GameObject NeutralBlock16384;
[SerializeField] GameObject NeutralBlock32768;
[SerializeField] GameObject NeutralBlock65536;


[SerializeField] GameObject BlueBlock2;
[SerializeField] GameObject BlueBlock4;
[SerializeField] GameObject BlueBlock8;
[SerializeField] GameObject BlueBlock16;
[SerializeField] GameObject BlueBlock32;
[SerializeField] GameObject BlueBlock64;
[SerializeField] GameObject BlueBlock128;
[SerializeField] GameObject BlueBlock256;
[SerializeField] GameObject BlueBlock512;
[SerializeField] GameObject BlueBlock1024;
[SerializeField] GameObject BlueBlock2048;
[SerializeField] GameObject BlueBlock4096;
[SerializeField] GameObject BlueBlock8192;
[SerializeField] GameObject BlueBlock16384;
[SerializeField] GameObject BlueBlock32768;
[SerializeField] GameObject BlueBlock65536;

[SerializeField] GameObject RedBlock2;
[SerializeField] GameObject RedBlock4;
[SerializeField] GameObject RedBlock8;
[SerializeField] GameObject RedBlock16;
[SerializeField] GameObject RedBlock32;
[SerializeField] GameObject RedBlock64;
[SerializeField] GameObject RedBlock128;
[SerializeField] GameObject RedBlock256;
[SerializeField] GameObject RedBlock512;
[SerializeField] GameObject RedBlock1024;
[SerializeField] GameObject RedBlock2048;
[SerializeField] GameObject RedBlock4096;
[SerializeField] GameObject RedBlock8192;
[SerializeField] GameObject RedBlock16384;
[SerializeField] GameObject RedBlock32768;
[SerializeField] GameObject RedBlock65536;

[SerializeField] GameObject GreenBlock2;
[SerializeField] GameObject GreenBlock4;
[SerializeField] GameObject GreenBlock8;
[SerializeField] GameObject GreenBlock16;
[SerializeField] GameObject GreenBlock32;
[SerializeField] GameObject GreenBlock64;
[SerializeField] GameObject GreenBlock128;
[SerializeField] GameObject GreenBlock256;
[SerializeField] GameObject GreenBlock512;
[SerializeField] GameObject GreenBlock1024;
[SerializeField] GameObject GreenBlock2048;
[SerializeField] GameObject GreenBlock4096;
[SerializeField] GameObject GreenBlock8192;
[SerializeField] GameObject GreenBlock16384;
[SerializeField] GameObject GreenBlock32768;
[SerializeField] GameObject GreenBlock65536;

[SerializeField] GameObject PinkBlock2;
[SerializeField] GameObject PinkBlock4;
[SerializeField] GameObject PinkBlock8;
[SerializeField] GameObject PinkBlock16;
[SerializeField] GameObject PinkBlock32;
[SerializeField] GameObject PinkBlock64;
[SerializeField] GameObject PinkBlock128;
[SerializeField] GameObject PinkBlock256;
[SerializeField] GameObject PinkBlock512;
[SerializeField] GameObject PinkBlock1024;
[SerializeField] GameObject PinkBlock2048;
[SerializeField] GameObject PinkBlock4096;
[SerializeField] GameObject PinkBlock8192;
[SerializeField] GameObject PinkBlock16384;
[SerializeField] GameObject PinkBlock32768;
[SerializeField] GameObject PinkBlock65536;

[SerializeField] GameObject SilverBlock2;
[SerializeField] GameObject SilverBlock4;
[SerializeField] GameObject SilverBlock8;
[SerializeField] GameObject SilverBlock16;
[SerializeField] GameObject SilverBlock32;
[SerializeField] GameObject SilverBlock64;
[SerializeField] GameObject SilverBlock128;
[SerializeField] GameObject SilverBlock256;
[SerializeField] GameObject SilverBlock512;
[SerializeField] GameObject SilverBlock1024;
[SerializeField] GameObject SilverBlock2048;
[SerializeField] GameObject SilverBlock4096;
[SerializeField] GameObject SilverBlock8192;
[SerializeField] GameObject SilverBlock16384;
[SerializeField] GameObject SilverBlock32768;
[SerializeField] GameObject SilverBlock65536;





}
