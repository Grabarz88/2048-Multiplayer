using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalVsSpawnBlock : MonoBehaviour
{

GameObject block;
FieldScript FieldScript;
SpawnField SpawnField;
[SerializeField] GameObject FieldSpawner;
public GameObject gameOverPanel;
[SerializeField] Text winnerAnnouncemenet;
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
// bool EnemyIsComputer = true;

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

        SpawnField = FieldSpawner.GetComponent<SpawnField>();
        fields = SpawnField.fields;

        int randomX = Random.Range(2, 3);
        int randomY = Random.Range(2, 3);

        block = Instantiate(NeutralBlock2);
        NeutralBlocks.Add(block);
        blocks.Add(block);
        block.GetComponent<LocalVSBlockBehaviourScript>().AfterSpawn(randomX, randomY, 0);
        block.gameObject.name = "block" + blockID;
        blockID++;

        if(Player1Color == 1) //Ta funkcja zostanie kiedyś rozwinięta o inne kolory bloków
        {
            block = Instantiate(BlueBlock2);
            P1Blocks.Add(block);
            blocks.Add(block);
            block.GetComponent<LocalVSBlockBehaviourScript>().AfterSpawn(1, 4, 1);
            block.gameObject.name = "block" + blockID;
        }
        blockID++;

        if(Player2Color == 2) //Ta funkcja zostanie kiedyś rozwinięta o inne kolory bloków
        {
            block = Instantiate(RedBlock2);
            P2Blocks.Add(block);
            blocks.Add(block);
            block.GetComponent<LocalVSBlockBehaviourScript>().AfterSpawn(4, 1, 2);
            block.gameObject.name = "block" + blockID;
        }
        blockID++;

        TurnColorImg = TurnColorPanel.gameObject.GetComponent<Image>();
        TurnColorImg.color = new Color32(119,221,250,255);

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
                TurnColorImg.color = new Color32(242,118,140,255);
                Player1Turn = false;
                Player2Turn = true;
                Waiting = false;
            }
            else if (Player2Turn == true)
            {
                TurnColorImg.color = new Color32(119,221,250,255);
                Player2Turn = false;
                Player1Turn = true;
                Waiting = false;
            }
        }       
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
                            int randomBlock = Random.Range(1, 2);
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
            Debug.Log("TRUE GAME OVER");
            gameOverPanel.gameObject.SetActive(true);
        }
    }


public void BlockLevelUp(int x, int y, int value, int owner) //#TODO This function can be optimized.
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
            blocks.Add(block);
            NeutralBlocks.Add(block);
            block.GetComponent<LocalVSBlockBehaviourScript>().AfterSpawn(x, y, 0); 
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
                blocks.Add(block);
                P1Blocks.Add(block);
                block.GetComponent<LocalVSBlockBehaviourScript>().AfterSpawn(x, y, 1); 
            }
        }
        else if (owner == 2)
        {
            if(Player2Color == 2)
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
                blocks.Add(block);
                P2Blocks.Add(block);
                block.GetComponent<LocalVSBlockBehaviourScript>().AfterSpawn(x, y, 2);
            }
        }

        block.GetComponent<LocalVSBlockBehaviourScript>().dir = "empty";
        block.gameObject.name = "block" + blockID;
        blockID++; 


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


}
