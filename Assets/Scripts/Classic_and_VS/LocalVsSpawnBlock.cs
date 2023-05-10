using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalVsSpawnBlock : MonoBehaviour
{

GameObject block;
FieldScript FieldScript;
SpawnField SpawnField;
LocalVS_AI LocalVS_AI;
[SerializeField] GameObject FieldSpawner;
[SerializeField] GameObject AI_Object;
public GameObject gameOverPanel;
[SerializeField] Text winnerAnnouncemenet;
[SerializeField] Text TurnPlayerNumber;
[SerializeField] public GameObject TurnColorPanel;
[SerializeField] Image TurnColorImg;
public LocalVSBlockBehaviourScript BlockBehaviourScript;
int blockID = 0;
int Player1Color = 0;
int Player2Color = 0;
public List<GameObject> fields;
public List<GameObject> blocks;
public List<GameObject> P1Blocks;
public List<GameObject> P2Blocks;
public List<GameObject> NeutralBlocks;
public bool Player1Turn = true;
public bool Player2Turn = false;
public bool Waiting = false;
public bool EnemyIsComputer = false;
public bool AIThinks = false;

    int idleCounter;
    int finishedSearchingCounter;
    int willMoveCounter;
    int finishedMovingCounter;
    
    public int randomX;
    public int randomY;


//ZE WZGLĘDU NA OGROMNĄ ILOŚĆ MIEJSA JAKĄ ZAJMUJĄ, DEKLARACJE BLOKÓW ZOSTAŁY UMIESZCZONE NA DOLE SKRYPTU



    void Start()
    {
        gameOverPanel.gameObject.SetActive(false);
        GameObject ObjectToRememberColors = GameObject.Find("ObjectToRememberColors");
        Player1Color = ObjectToRememberColors.GetComponent<ScriptToRememberColors>().Player1ColorGetter();
        Player2Color = ObjectToRememberColors.GetComponent<ScriptToRememberColors>().Player2ColorGetter(); 
        EnemyIsComputer = ObjectToRememberColors.GetComponent<ScriptToRememberColors>().EnemyIsComputer();
        

        SpawnField = FieldSpawner.GetComponent<SpawnField>();
        fields = SpawnField.fields;

        LocalVS_AI = AI_Object.GetComponent<LocalVS_AI>();   

        int randomX = Random.Range(2, 4);
        int randomY = Random.Range(2, 4);

        block = Instantiate(NeutralBlock2);
        NeutralBlocks.Add(block);
        blocks.Add(block);
        block.GetComponent<LocalVSBlockBehaviourScript>().AfterSpawn(randomX, randomY, 0);
        block.gameObject.name = "block" + blockID;
        blockID++;

        TurnColorImg = TurnColorPanel.gameObject.GetComponent<Image>();

        if(Player1Color == 1) //Player 1 has blue blocks
        {
            block = Instantiate(BlueBlock2);
            P1Blocks.Add(block);
            blocks.Add(block);
            block.GetComponent<LocalVSBlockBehaviourScript>().AfterSpawn(1, 4, 1);
            block.gameObject.name = "P1MainBlock";
            TurnColorImg.color = new Color32(119,221,250,255);
        }
        else if(Player1Color == 2) //Player 1 has red blocks
        {
            block = Instantiate(RedBlock2);
            P1Blocks.Add(block);
            blocks.Add(block);
            block.GetComponent<LocalVSBlockBehaviourScript>().AfterSpawn(1, 4, 1);
            block.gameObject.name = "P1MainBlock";
            TurnColorImg.color = new Color32(242,118,140,255);
        }
        else if(Player1Color == 3) //Player 1 has green blocks
        {
            block = Instantiate(GreenBlock2);
            P1Blocks.Add(block);
            blocks.Add(block);
            block.GetComponent<LocalVSBlockBehaviourScript>().AfterSpawn(1, 4, 1);
            block.gameObject.name = "P1MainBlock";
            TurnColorImg.color = new Color32(137,217,171,255);
        }
        else if(Player1Color == 4) //Player 1 has pink blocks
        {
            block = Instantiate(PinkBlock2);
            P1Blocks.Add(block);
            blocks.Add(block);
            block.GetComponent<LocalVSBlockBehaviourScript>().AfterSpawn(1, 4, 1);
            block.gameObject.name = "P1MainBlock";
            TurnColorImg.color = new Color32(236,123,222,255);
        }
        else if(Player1Color == 5) //Player 1 has silver blocks
        {
            block = Instantiate(SilverBlock2);
            P1Blocks.Add(block);
            blocks.Add(block);
            block.GetComponent<LocalVSBlockBehaviourScript>().AfterSpawn(1, 4, 1);
            block.gameObject.name = "P1MainBlock";
            TurnColorImg.color = new Color32(104,105,104,255);
        }
        blockID++;

        if(Player2Color == 1) //Player 2 has blue blocks
        {
            block = Instantiate(BlueBlock2);
            P2Blocks.Add(block);
            blocks.Add(block);
            block.GetComponent<LocalVSBlockBehaviourScript>().AfterSpawn(4, 1, 2);
            block.gameObject.name = "P2MainBlock";
        }
        else if(Player2Color == 2) //Player 2 has red blocks
        {
            block = Instantiate(RedBlock2);
            P2Blocks.Add(block);
            blocks.Add(block);
            block.GetComponent<LocalVSBlockBehaviourScript>().AfterSpawn(4, 1, 2);
            block.gameObject.name = "P2MainBlock";
        }
        else if(Player2Color == 3) //Player 2 has green blocks
        {
            block = Instantiate(GreenBlock2);
            P2Blocks.Add(block);
            blocks.Add(block);
            block.GetComponent<LocalVSBlockBehaviourScript>().AfterSpawn(4, 1, 2);
            block.gameObject.name = "P2MainBlock";
        }
        else if(Player2Color == 4) //Player 2 has pink blocks
        {
            block = Instantiate(PinkBlock2);
            P2Blocks.Add(block);
            blocks.Add(block);
            block.GetComponent<LocalVSBlockBehaviourScript>().AfterSpawn(4, 1, 2);
            block.gameObject.name = "P2MainBlock";
        }
        else if(Player2Color == 5) //Player 1 has silver blocks
        {
            block = Instantiate(SilverBlock2);
            P2Blocks.Add(block);
            blocks.Add(block);
            block.GetComponent<LocalVSBlockBehaviourScript>().AfterSpawn(4, 1, 2);
            block.gameObject.name = "P2MainBlock";
        }
        blockID++;

    }


    void Update()
    {
        
        // if (AIThinks == false)
        // {
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
                    BlockBehaviourScript = block.GetComponent<LocalVSBlockBehaviourScript>();
                    if (BlockBehaviourScript.idle == true) {idleCounter++;}
                    if (BlockBehaviourScript.finishedSearching == true) {finishedSearchingCounter++;}
                    if (BlockBehaviourScript.willMove == true || BlockBehaviourScript.willBeDestroyed == true) {willMoveCounter++;}
                    if (BlockBehaviourScript.finishedMoving == true) {finishedMovingCounter++;}
                }
            }
    
            if(idleCounter == blocks.Count)
            {
                CheckForGameOver();
                foreach(GameObject block in blocks)
                {
                    //Mówimy blokom, że mogą się zacząć szukać swoich nowych pozycji
                    BlockBehaviourScript = block.GetComponent<LocalVSBlockBehaviourScript>();
                    if(Player1Turn == true && Waiting == false)
                    {
                        if(BlockBehaviourScript.OwnerID == 1 || BlockBehaviourScript.OwnerID == 0)
                        {
                            BlockBehaviourScript.executeWaitingForDir();
                        }
                        
                    }
                    if (Player2Turn == true && Waiting == false)
                    {
                        if(BlockBehaviourScript.OwnerID == 2 || BlockBehaviourScript.OwnerID == 0)
                        {
                            BlockBehaviourScript.executeWaitingForDir();
                        }                   
                    }
                }
                Waiting = true;
                if(Player2Turn == true)
                {
                    // AIThinks = true;
                    LocalVS_AI.Player2IsMoving_AI();
                }
    
            }
            
            if(((Player1Turn == true && finishedSearchingCounter == P1Blocks.Count + NeutralBlocks.Count) || (Player2Turn == true && finishedSearchingCounter == P2Blocks.Count + NeutralBlocks.Count)) && willMoveCounter > 0)
            {
                foreach(GameObject block in blocks)
                {
                    //Mówimy blokom, że mogą zacząć wykonywać ruch
                    BlockBehaviourScript = block.GetComponent<LocalVSBlockBehaviourScript>();
                    if(Player1Turn == true)                
                    {
                        if(BlockBehaviourScript.OwnerID == 1 || BlockBehaviourScript.OwnerID == 0)
                        {
                            BlockBehaviourScript.executeMove();
                        }
                    }
                    if (Player2Turn == true)
                    {
                        if(BlockBehaviourScript.OwnerID == 2 || BlockBehaviourScript.OwnerID == 0)
                        {
                            BlockBehaviourScript.executeMove();
                        }                    
                    }
                    
                }
            }
            else if(((Player1Turn == true && finishedSearchingCounter == P1Blocks.Count + NeutralBlocks.Count) || (Player2Turn == true && finishedSearchingCounter == P2Blocks.Count + NeutralBlocks.Count)) && willMoveCounter == 0)
            {
                foreach(GameObject block in blocks)
                {
                    BlockBehaviourScript = block.GetComponent<LocalVSBlockBehaviourScript>();
                    BlockBehaviourScript.dir = "empty";
                    BlockBehaviourScript.finishedSearching = false;
                    BlockBehaviourScript.idle = true;
                    Waiting = false;
    
                }
            }
            
            if((Player1Turn == true && finishedMovingCounter == P1Blocks.Count + NeutralBlocks.Count) || (Player2Turn == true && finishedMovingCounter == P2Blocks.Count + NeutralBlocks.Count))
            {
                foreach(GameObject block in blocks)
                {
                    //Mówimy blokom, że skoro skończyły się ruszać to mogą się zniszczyć jeśli potrzebują i mają się przygotować do następnej kolejki
                    BlockBehaviourScript = block.GetComponent<LocalVSBlockBehaviourScript>();
                    BlockBehaviourScript.executeLevelUp();
                    BlockBehaviourScript.finishedMoving = false;
                    BlockBehaviourScript.idle = true;
                    BlockBehaviourScript.dir = "empty";
                }
                SpawnNewBlock();
                if(Player1Turn == true)
                {
                    if(Player2Color == 1){TurnColorImg.color = new Color32(119,221,250,255);}
                    else if(Player2Color == 2){TurnColorImg.color = new Color32(242,118,140,255);}
                    else if(Player2Color == 3){TurnColorImg.color = new Color32(137,217,171,255);}
                    else if(Player2Color == 4){TurnColorImg.color = new Color32(236,123,222,255);}
                    else if(Player2Color == 5){TurnColorImg.color = new Color32(104,105,104,255);}
                    Player1Turn = false;
                    Player2Turn = true;
                    TurnPlayerNumber.text = "2";
                    Waiting = false;
                }
                else if (Player2Turn == true)
                {
                    if(Player1Color == 1){TurnColorImg.color = new Color32(119,221,250,255);}
                    else if(Player1Color == 2){TurnColorImg.color = new Color32(242,118,140,255);}
                    else if(Player1Color == 3){TurnColorImg.color = new Color32(137,217,171,255);}
                    else if(Player1Color == 4){TurnColorImg.color = new Color32(236,123,222,255);}
                    else if(Player1Color == 5){TurnColorImg.color = new Color32(104,105,104,255);}
                    Player2Turn = false;
                    Player1Turn = true;
                    TurnPlayerNumber.text = "1";
                    Waiting = false;
                }
            }       
        // }
    }

public void SpawnNewBlock()
    {
        // Debug.Log("SpawnNewBlock");
        int fieldCounter = 0;
        bool blockSpawned = false;



        while (fieldCounter < fields.Count && blockSpawned == false)
            {
                int randomPosition = Random.Range(0, fields.Count-1);
                FieldScript = fields[randomPosition].GetComponent<FieldScript>();
                if (FieldScript.checkedForSpawnPurpose == false)
                {
                    if (FieldScript.isWall == true)
                    {
                        fieldCounter++;
                        FieldScript.checkedForSpawnPurpose = true;
                    }
                    else if (FieldScript.isWall == false)
                    {
                        if(FieldScript.isTaken == true)
                        {
                            fieldCounter++;
                            FieldScript.checkedForSpawnPurpose = true;
                        }
                        else if (FieldScript.isTaken == false)
                        {
                            int randomBlock = Random.Range(1, 3);
                            if(randomBlock == 1) {block = Instantiate(NeutralBlock2);}
                            else if(randomBlock == 2) {block = Instantiate(NeutralBlock4);}
        
                            
                            blocks.Add(block);
                            NeutralBlocks.Add(block);
                            block.GetComponent<LocalVSBlockBehaviourScript>().AfterSpawn(FieldScript.TableNumberX, FieldScript.TableNumberY, 0);
                            block.gameObject.name = "block" + blockID;
                            blockID++;
                            blockSpawned = true;
                            
                            ClearAfterSpawn();
                            CheckForGameOver();
                        }
                    }

                }
                
            }
    }

    void ClearAfterSpawn()
    {
        // Debug.Log("ClearAfterSpawn");
        foreach (GameObject field in fields)
        {
           FieldScript = field.gameObject.GetComponent<FieldScript>();
           FieldScript.checkedForSpawnPurpose = false;
        }
    }

    public void CheckForGameOver() // TĄ FUNKCJĘ TRZEBA W CAŁOŚCI PRZEROBIĆ
    {
        blocks.TrimExcess();
        int canMove = 0;
        foreach(GameObject block in NeutralBlocks)
        {
            LocalVSBlockBehaviourScript BBH = block.GetComponent<LocalVSBlockBehaviourScript>();
            foreach (GameObject neighbourBlock in NeutralBlocks)
            {
                LocalVSBlockBehaviourScript nBBH = neighbourBlock.GetComponent<LocalVSBlockBehaviourScript>();
                if(BBH.value == nBBH.value)
                {
                    if(BBH.TableNumberX == nBBH.TableNumberX && BBH.TableNumberY == nBBH.TableNumberY+1){canMove++;} 
                    if(BBH.TableNumberY == nBBH.TableNumberY && BBH.TableNumberX == nBBH.TableNumberX+1){canMove++;}
                }
            }
        }
        if(Player1Turn == true)
        {
            foreach(GameObject block in P2Blocks)
            {
                LocalVSBlockBehaviourScript BBH = block.GetComponent<LocalVSBlockBehaviourScript>();
                foreach (GameObject neighbourBlock in NeutralBlocks)
                {
                    LocalVSBlockBehaviourScript nBBH = neighbourBlock.GetComponent<LocalVSBlockBehaviourScript>();
                    if(BBH.value == nBBH.value)
                    {
                        if(BBH.TableNumberX == nBBH.TableNumberX && BBH.TableNumberY == nBBH.TableNumberY+1){canMove++;} 
                        if(BBH.TableNumberX == nBBH.TableNumberX && BBH.TableNumberY == nBBH.TableNumberY-1){canMove++;} 
                        if(BBH.TableNumberY == nBBH.TableNumberY && BBH.TableNumberX == nBBH.TableNumberX+1){canMove++;}
                        if(BBH.TableNumberY == nBBH.TableNumberY && BBH.TableNumberX == nBBH.TableNumberX-1){canMove++;}
                    }
                }
            }
        }
        else if(Player2Turn == true)
        {
            foreach(GameObject block in P1Blocks)
            {
                LocalVSBlockBehaviourScript BBH = block.GetComponent<LocalVSBlockBehaviourScript>();
                foreach (GameObject neighbourBlock in NeutralBlocks)
                {
                    LocalVSBlockBehaviourScript nBBH = neighbourBlock.GetComponent<LocalVSBlockBehaviourScript>();
                    if(BBH.value == nBBH.value)
                    {
                        if(BBH.TableNumberX == nBBH.TableNumberX && BBH.TableNumberY == nBBH.TableNumberY+1){canMove++;} 
                        if(BBH.TableNumberX == nBBH.TableNumberX && BBH.TableNumberY == nBBH.TableNumberY-1){canMove++;} 
                        if(BBH.TableNumberY == nBBH.TableNumberY && BBH.TableNumberX == nBBH.TableNumberX+1){canMove++;}
                        if(BBH.TableNumberY == nBBH.TableNumberY && BBH.TableNumberX == nBBH.TableNumberX-1){canMove++;}
                    }
                }
            }
        }
        
        foreach(GameObject field in fields)
        {
            FieldScript FS = field.GetComponent<FieldScript>();
            if(FS.isTaken == false && FS.isWall == false){canMove++;}
        }
        if(canMove == 0) 
        {
            gameOverPanel.gameObject.SetActive(true);
        }
    }



public void RemoveBlockFromList(GameObject block)
    {
        // Debug.Log("RemoveBlockFromList");
        blocks.Remove(block);
    }

    
public void AnnounceGameOver(int winner)
    {
        gameOverPanel.gameObject.SetActive(true);
        string whoWon = winner.ToString();
        winnerAnnouncemenet.text = "Zwycięża Gracz " + whoWon;

    }


public void BlockLevelUp(int x, int y, long value, int owner) //#TODO This function can be optimized.
    {
        if(owner == 0)
        {
            if(value == 2){block = Instantiate(NeutralBlock4);}
            else if(value == 4){block = Instantiate(NeutralBlock8);}
            else if(value == 8){block = Instantiate(NeutralBlock16);}
            else if(value == 16){block = Instantiate(NeutralBlock32);}
            else if(value == 32){block = Instantiate(NeutralBlock64);}
            else if(value == 64){block = Instantiate(NeutralBlock128);}
            else if(value == 128){block = Instantiate(NeutralBlock256);}
            else if(value == 256){block = Instantiate(NeutralBlock512);}
            else if(value == 512){block = Instantiate(NeutralBlock1024);}
            else if(value == 1024){block = Instantiate(NeutralBlock2048);}
            else if(value == 2048){block = Instantiate(NeutralBlock4096);}   
            else if(value == 4096){block = Instantiate(NeutralBlock8192);} 
            else if(value == 8192){block = Instantiate(NeutralBlock16384);} 
            else if(value == 16384){block = Instantiate(NeutralBlock32768);}
            else if(value == 32768){block = Instantiate(NeutralBlock65536);}  
            blocks.Add(block);
            NeutralBlocks.Add(block);
            block.GetComponent<LocalVSBlockBehaviourScript>().AfterSpawn(x, y, 0); 
            block.GetComponent<LocalVSBlockBehaviourScript>().dir = "empty";
            block.gameObject.name = "block" + blockID;
            blockID++; 

        }
        if(owner == 1)
        {
            if(Player1Color == 1)
            {
                if(value == 2){block = Instantiate(BlueBlock4);}
                else if(value == 4){block = Instantiate(BlueBlock8);}
                else if(value == 8){block = Instantiate(BlueBlock16);}
                else if(value == 16){block = Instantiate(BlueBlock32);}
                else if(value == 32){block = Instantiate(BlueBlock64);}
                else if(value == 64){block = Instantiate(BlueBlock128);}
                else if(value == 128){block = Instantiate(BlueBlock256);}
                else if(value == 256){block = Instantiate(BlueBlock512);}
                else if(value == 512){block = Instantiate(BlueBlock1024);}
                else if(value == 1024){block = Instantiate(BlueBlock2048);}
                else if(value == 2048){block = Instantiate(BlueBlock4096);}  
                else if(value == 4096){block = Instantiate(BlueBlock8192);} 
                else if(value == 8192){block = Instantiate(BlueBlock16384);} 
                else if(value == 16384){block = Instantiate(BlueBlock32768);}
                else if(value == 32768){block = Instantiate(BlueBlock65536);}  
            }
            else if(Player1Color == 2)
            {  
                if(value == 2){block = Instantiate(RedBlock4);}
                else if(value == 4){block = Instantiate(RedBlock8);}
                else if(value == 8){block = Instantiate(RedBlock16);}
                else if(value == 16){block = Instantiate(RedBlock32);}
                else if(value == 32){block = Instantiate(RedBlock64);}
                else if(value == 64){block = Instantiate(RedBlock128);}
                else if(value == 128){block = Instantiate(RedBlock256);}
                else if(value == 256){block = Instantiate(RedBlock512);}
                else if(value == 512){block = Instantiate(RedBlock1024);}
                else if(value == 1024){block = Instantiate(RedBlock2048);}
                else if(value == 2048){block = Instantiate(RedBlock4096);}
                else if(value == 4096){block = Instantiate(RedBlock8192);} 
                else if(value == 8192){block = Instantiate(RedBlock16384);} 
                else if(value == 16384){block = Instantiate(RedBlock32768);}
                else if(value == 32768){block = Instantiate(RedBlock65536);}     
            }
            else if(Player1Color == 3)
            {  
                if(value == 2){block = Instantiate(GreenBlock4);}
                else if(value == 4){block = Instantiate(GreenBlock8);}
                else if(value == 8){block = Instantiate(GreenBlock16);}
                else if(value == 16){block = Instantiate(GreenBlock32);}
                else if(value == 32){block = Instantiate(GreenBlock64);}
                else if(value == 64){block = Instantiate(GreenBlock128);}
                else if(value == 128){block = Instantiate(GreenBlock256);}
                else if(value == 256){block = Instantiate(GreenBlock512);}
                else if(value == 512){block = Instantiate(GreenBlock1024);}
                else if(value == 1024){block = Instantiate(GreenBlock2048);}
                else if(value == 2048){block = Instantiate(GreenBlock4096);}
                else if(value == 4096){block = Instantiate(GreenBlock8192);} 
                else if(value == 8192){block = Instantiate(GreenBlock16384);} 
                else if(value == 16384){block = Instantiate(GreenBlock32768);}
                else if(value == 32768){block = Instantiate(GreenBlock65536);}     
            }
            else if(Player1Color == 4)
            {  
                if(value == 2){block = Instantiate(PinkBlock4);}
                else if(value == 4){block = Instantiate(PinkBlock8);}
                else if(value == 8){block = Instantiate(PinkBlock16);}
                else if(value == 16){block = Instantiate(PinkBlock32);}
                else if(value == 32){block = Instantiate(PinkBlock64);}
                else if(value == 64){block = Instantiate(PinkBlock128);}
                else if(value == 128){block = Instantiate(PinkBlock256);}
                else if(value == 256){block = Instantiate(PinkBlock512);}
                else if(value == 512){block = Instantiate(PinkBlock1024);}
                else if(value == 1024){block = Instantiate(PinkBlock2048);}
                else if(value == 2048){block = Instantiate(PinkBlock4096);}
                else if(value == 4096){block = Instantiate(PinkBlock8192);} 
                else if(value == 8192){block = Instantiate(PinkBlock16384);} 
                else if(value == 16384){block = Instantiate(PinkBlock32768);}
                else if(value == 32768){block = Instantiate(PinkBlock65536);}     
            }
            else if(Player1Color == 5)
            {  
                if(value == 2){block = Instantiate(SilverBlock4);}
                else if(value == 4){block = Instantiate(SilverBlock8);}
                else if(value == 8){block = Instantiate(SilverBlock16);}
                else if(value == 16){block = Instantiate(SilverBlock32);}
                else if(value == 32){block = Instantiate(SilverBlock64);}
                else if(value == 64){block = Instantiate(SilverBlock128);}
                else if(value == 128){block = Instantiate(SilverBlock256);}
                else if(value == 256){block = Instantiate(SilverBlock512);}
                else if(value == 512){block = Instantiate(SilverBlock1024);}
                else if(value == 1024){block = Instantiate(SilverBlock2048);}
                else if(value == 2048){block = Instantiate(SilverBlock4096);}
                else if(value == 4096){block = Instantiate(SilverBlock8192);} 
                else if(value == 8192){block = Instantiate(SilverBlock16384);} 
                else if(value == 16384){block = Instantiate(SilverBlock32768);}
                else if(value == 32768){block = Instantiate(SilverBlock65536);}     
            }
            blocks.Add(block);
            P1Blocks.Add(block);
            block.GetComponent<LocalVSBlockBehaviourScript>().AfterSpawn(x, y, 1); 
            block.gameObject.name = "P1MainBlock";
            blockID++; 
        }
        else if (owner == 2)
        {
            if(Player2Color == 1)
            {
                if(value == 2){block = Instantiate(BlueBlock4);}
                else if(value == 4){block = Instantiate(BlueBlock8);}
                else if(value == 8){block = Instantiate(BlueBlock16);}
                else if(value == 16){block = Instantiate(BlueBlock32);}
                else if(value == 32){block = Instantiate(BlueBlock64);}
                else if(value == 64){block = Instantiate(BlueBlock128);}
                else if(value == 128){block = Instantiate(BlueBlock256);}
                else if(value == 256){block = Instantiate(BlueBlock512);}
                else if(value == 512){block = Instantiate(BlueBlock1024);}
                else if(value == 1024){block = Instantiate(BlueBlock2048);}
                else if(value == 2048){block = Instantiate(BlueBlock4096);}  
                else if(value == 4096){block = Instantiate(BlueBlock8192);} 
                else if(value == 8192){block = Instantiate(BlueBlock16384);} 
                else if(value == 16384){block = Instantiate(BlueBlock32768);}
                else if(value == 32768){block = Instantiate(BlueBlock65536);}  
            }
            else if(Player2Color == 2)
            {  
                if(value == 2){block = Instantiate(RedBlock4);}
                else if(value == 4){block = Instantiate(RedBlock8);}
                else if(value == 8){block = Instantiate(RedBlock16);}
                else if(value == 16){block = Instantiate(RedBlock32);}
                else if(value == 32){block = Instantiate(RedBlock64);}
                else if(value == 64){block = Instantiate(RedBlock128);}
                else if(value == 128){block = Instantiate(RedBlock256);}
                else if(value == 256){block = Instantiate(RedBlock512);}
                else if(value == 512){block = Instantiate(RedBlock1024);}
                else if(value == 1024){block = Instantiate(RedBlock2048);}
                else if(value == 2048){block = Instantiate(RedBlock4096);}
                else if(value == 4096){block = Instantiate(RedBlock8192);} 
                else if(value == 8192){block = Instantiate(RedBlock16384);} 
                else if(value == 16384){block = Instantiate(RedBlock32768);}
                else if(value == 32768){block = Instantiate(RedBlock65536);}     
            }
            else if(Player2Color == 3)
            {  
                if(value == 2){block = Instantiate(GreenBlock4);}
                else if(value == 4){block = Instantiate(GreenBlock8);}
                else if(value == 8){block = Instantiate(GreenBlock16);}
                else if(value == 16){block = Instantiate(GreenBlock32);}
                else if(value == 32){block = Instantiate(GreenBlock64);}
                else if(value == 64){block = Instantiate(GreenBlock128);}
                else if(value == 128){block = Instantiate(GreenBlock256);}
                else if(value == 256){block = Instantiate(GreenBlock512);}
                else if(value == 512){block = Instantiate(GreenBlock1024);}
                else if(value == 1024){block = Instantiate(GreenBlock2048);}
                else if(value == 2048){block = Instantiate(GreenBlock4096);}
                else if(value == 4096){block = Instantiate(GreenBlock8192);} 
                else if(value == 8192){block = Instantiate(GreenBlock16384);} 
                else if(value == 16384){block = Instantiate(GreenBlock32768);}
                else if(value == 32768){block = Instantiate(GreenBlock65536);}     
            }
            else if(Player2Color == 4)
            {  
                if(value == 2){block = Instantiate(PinkBlock4);}
                else if(value == 4){block = Instantiate(PinkBlock8);}
                else if(value == 8){block = Instantiate(PinkBlock16);}
                else if(value == 16){block = Instantiate(PinkBlock32);}
                else if(value == 32){block = Instantiate(PinkBlock64);}
                else if(value == 64){block = Instantiate(PinkBlock128);}
                else if(value == 128){block = Instantiate(PinkBlock256);}
                else if(value == 256){block = Instantiate(PinkBlock512);}
                else if(value == 512){block = Instantiate(PinkBlock1024);}
                else if(value == 1024){block = Instantiate(PinkBlock2048);}
                else if(value == 2048){block = Instantiate(PinkBlock4096);}
                else if(value == 4096){block = Instantiate(PinkBlock8192);} 
                else if(value == 8192){block = Instantiate(PinkBlock16384);} 
                else if(value == 16384){block = Instantiate(PinkBlock32768);}
                else if(value == 32768){block = Instantiate(PinkBlock65536);}     
            }
            else if(Player2Color == 5)
            {  
                if(value == 2){block = Instantiate(SilverBlock4);}
                else if(value == 4){block = Instantiate(SilverBlock8);}
                else if(value == 8){block = Instantiate(SilverBlock16);}
                else if(value == 16){block = Instantiate(SilverBlock32);}
                else if(value == 32){block = Instantiate(SilverBlock64);}
                else if(value == 64){block = Instantiate(SilverBlock128);}
                else if(value == 128){block = Instantiate(SilverBlock256);}
                else if(value == 256){block = Instantiate(SilverBlock512);}
                else if(value == 512){block = Instantiate(SilverBlock1024);}
                else if(value == 1024){block = Instantiate(SilverBlock2048);}
                else if(value == 2048){block = Instantiate(SilverBlock4096);}
                else if(value == 4096){block = Instantiate(SilverBlock8192);} 
                else if(value == 8192){block = Instantiate(SilverBlock16384);} 
                else if(value == 16384){block = Instantiate(SilverBlock32768);}
                else if(value == 32768){block = Instantiate(SilverBlock65536);}     
            }
            blocks.Add(block);
            P2Blocks.Add(block);
            block.GetComponent<LocalVSBlockBehaviourScript>().AfterSpawn(x, y, 2);
            block.gameObject.name = "P2MainBlock";
            blockID++; 
        }

        


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
