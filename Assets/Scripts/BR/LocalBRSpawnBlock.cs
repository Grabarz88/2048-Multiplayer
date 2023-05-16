using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalBRSpawnBlock : MonoBehaviour
{
GameObject block;
FieldScript FieldScript;
SpawnFieldBR SpawnField;
[SerializeField] GameObject Camera;
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
public List<GameObject> P1Fields;
public List<GameObject> P2Fields;
public List<GameObject> P3Fields;
public List<GameObject> P4Fields;
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
public int fieldIndicator = 0;

int player2FieldCorrection = 0;
int player3FieldCorrection = 0;
int player4FieldCorrection = 0;

int turnsToStartBR = 0;


void Start()
{
    ObjectToRememberColors = GameObject.Find("ObjectToRememberColors");
    ScriptToRememberColors = ObjectToRememberColors.GetComponent<ScriptToRememberBRColors>();
    isPlayer1Playing = ScriptToRememberColors.isPlayer1Playing;
    isPlayer2Playing = ScriptToRememberColors.isPlayer2Playing;
    isPlayer3Playing = ScriptToRememberColors.isPlayer3Playing;
    isPlayer4Playing = ScriptToRememberColors.isPlayer4Playing;
    SpawnField = FieldSpawner.GetComponent<SpawnFieldBR>();
    fields = SpawnField.fields;
    randomX = Random.Range(1, 5);
    randomY = Random.Range(1, 5);
    if(isPlayer1Playing == true)
    {
        turnsToStartBR = turnsToStartBR + 21;
        P1Fields = SpawnField.P1Fields;
        Player1Color = ScriptToRememberColors.Player1Color;
        randomX = Random.Range(1, 5);
        randomY = Random.Range(1, 5);
        InstantiateThisColorWithThisOwner(Player1Color, 2, 1, randomX, randomY);
        fieldIndicator = 6;
        LookForPlaceToSpawnBlockAndPlaceIt(1);
        
    }
    if(isPlayer2Playing == true)
    {
        turnsToStartBR = turnsToStartBR + 21;
        P2Fields = SpawnField.P2Fields;
        Player2Color = ScriptToRememberColors.Player2Color;
        randomX = Random.Range(1, 5);
        randomX = randomX + fieldIndicator;
        randomY = Random.Range(1, 5);
        randomY = randomY + fieldIndicator;
        InstantiateThisColorWithThisOwner(Player2Color, 2, 2, randomX, randomY);
        player2FieldCorrection = fieldIndicator*6;
        fieldIndicator = fieldIndicator + 6;
        LookForPlaceToSpawnBlockAndPlaceIt(2);
    }
    if(isPlayer3Playing == true)
    {
        turnsToStartBR = turnsToStartBR + 21;
        P3Fields = SpawnField.P3Fields;
        Player3Color = ScriptToRememberColors.Player3Color;
        randomX = Random.Range(1, 5);
        randomX = randomX + fieldIndicator;
        randomY = Random.Range(1, 5);
        randomY = randomY + fieldIndicator;
        InstantiateThisColorWithThisOwner(Player3Color, 2, 3, randomX, randomY);
        player3FieldCorrection = fieldIndicator*6;
        fieldIndicator = fieldIndicator + 6;
        LookForPlaceToSpawnBlockAndPlaceIt(3);
    }
    if(isPlayer4Playing == true)
    {
        turnsToStartBR = turnsToStartBR + 21;
        P4Fields = SpawnField.P4Fields;
        Player4Color = ScriptToRememberColors.Player4Color;
        randomX = Random.Range(1, 5);
        randomX = randomX + fieldIndicator;
        randomY = Random.Range(1, 5);
        randomY = randomY + fieldIndicator;
        InstantiateThisColorWithThisOwner(Player4Color, 2, 4, randomX, randomY);
        player4FieldCorrection = fieldIndicator*6;
        LookForPlaceToSpawnBlockAndPlaceIt(4);
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
                LookForPlaceToSpawnBlockAndPlaceIt(2);
                // TurnPlayerNumber.text = "2";
                Waiting = false;
            }
            else if(isPlayer3Playing)
            {
                // TurnColorImg.color = Player3Color;
                Player1Turn = false;
                Player3Turn = true;
                LookForPlaceToSpawnBlockAndPlaceIt(3);
                // TurnPlayerNumber.text = "3";
                Waiting = false;
            }
            else if(isPlayer4Playing)
            {
                // TurnColorImg.color = Player4Color;
                Player1Turn = false;
                Player4Turn = true;
                LookForPlaceToSpawnBlockAndPlaceIt(4);
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
                LookForPlaceToSpawnBlockAndPlaceIt(3);
                // TurnPlayerNumber.text = "3";
                Waiting = false;
            }
            else if(isPlayer4Playing)
            {
                // TurnColorImg.color = Player4Color;
                Player2Turn = false;
                Player4Turn = true;
                LookForPlaceToSpawnBlockAndPlaceIt(4);
                // TurnPlayerNumber.text = "4";
                Waiting = false;
            }
            else if(isPlayer1Playing)
            {
                // TurnColorImg.color = Player1Color;
                Player2Turn = false;
                Player1Turn = true;
                LookForPlaceToSpawnBlockAndPlaceIt(1);
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
                LookForPlaceToSpawnBlockAndPlaceIt(4);
                // TurnPlayerNumber.text = "4";
                Waiting = false;
            }
            else if(isPlayer1Playing)
            {
                // TurnColorImg.color = Player1Color;
                Player3Turn = false;
                Player1Turn = true;
                LookForPlaceToSpawnBlockAndPlaceIt(1);
                // TurnPlayerNumber.text = "1";
                Waiting = false;
            }
            else if(isPlayer2Playing)
            {
                // TurnColorImg.color = Player2Color;
                Player3Turn = false;
                Player2Turn = true;
                LookForPlaceToSpawnBlockAndPlaceIt(2);
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
                LookForPlaceToSpawnBlockAndPlaceIt(1);
                // TurnPlayerNumber.text = "1";
                Waiting = false;
            }
            else if(isPlayer2Playing)
            {
                // TurnColorImg.color = Player2Color;
                Player4Turn = false;
                Player2Turn = true;
                LookForPlaceToSpawnBlockAndPlaceIt(2);
                // TurnPlayerNumber.text = "2";
                Waiting = false;
            }
            else if(isPlayer3Playing)
            {
                // TurnColorImg.color = Player3Color;
                Player4Turn = false;
                Player3Turn = true;
                LookForPlaceToSpawnBlockAndPlaceIt(3);
                // TurnPlayerNumber.text = "3";
                Waiting = false;
            }
        }
    }     
}



public void InstantiateThisColorWithThisOwner(int color, long value, int owner, int X, int Y)
{
    // if(isPreparingFaze)
    // {
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
        block.gameObject.GetComponent<LocalBRBlockBehaviourScript>().AfterSpawn(X, Y);
        block.gameObject.name = "block" + blockID;
        blockID++;

    // }
    // else if(isBRFaze)
    // {

    // }
}

public void LookForPlaceToSpawnBlockAndPlaceIt(int owner)
{
    
    if(isPreparingFaze)
    {
    int fieldCounter = 0;
    int fieldOfPlayer = 0;
    int playerFieldCorrection = 0;
    int color = 0;
    bool blockSpawned = false;

    if(owner == 1)
    {
        fieldOfPlayer = P1Fields.Count;
        playerFieldCorrection = 0;
        color = Player1Color;

    }
    else if(owner == 2)
    {
        fieldOfPlayer = P2Fields.Count;
        playerFieldCorrection = player2FieldCorrection;
        color = Player2Color;
    }
    else if(owner == 3)
    {
        fieldOfPlayer = P3Fields.Count;
        playerFieldCorrection = player3FieldCorrection;
        color = Player3Color;
    }
    else if(owner == 4)
    {
        fieldOfPlayer = P4Fields.Count;
        playerFieldCorrection = player4FieldCorrection;
        color = Player4Color;   
    }

    while (fieldCounter < fieldOfPlayer && blockSpawned == false)
        {
            int randomPosition = Random.Range(playerFieldCorrection, playerFieldCorrection+fieldOfPlayer-1);
            
            
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

                        InstantiateThisColorWithThisOwner(color, 2, owner, FieldScript.TableNumberX, FieldScript.TableNumberY);
                      
                        
                        blockSpawned = true;
                        
                        ClearAfterSpawn();
                        CheckForGameOver(owner);
                        turnsToStartBR--;
                        Debug.Log(turnsToStartBR);
                        if(turnsToStartBR == 0)
                        {
                            Debug.Log("BR START");
                            isPreparingFaze = false;
                            SpawnField.initiateBRFaze();
                            Camera.GetComponent<CameraBRScript>().MoveForBR();
                            MoveBlocksForBRPlaces();
                            // isBRFaze = true;
                        }
                    }
                }

            }
            
        }
    }
    else if(isBRFaze)
    {
        Debug.Log("tu kiedyś coś postawię");
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

    public void CheckForGameOver(int player) // TĄ FUNKCJĘ TRZEBA W CAŁOŚCI PRZEROBIĆ
    {
        if(isPreparingFaze)
        {
            blocks.TrimExcess();
            List<GameObject> blocksToCheck = null;
            List<GameObject> fieldsToCheck = null;
            if(player == 1) {blocksToCheck = P1Blocks; fieldsToCheck = P1Fields;}
            if(player == 2) {blocksToCheck = P2Blocks; fieldsToCheck = P2Fields;}
            if(player == 3) {blocksToCheck = P3Blocks; fieldsToCheck = P3Fields;}
            if(player == 4) {blocksToCheck = P4Blocks; fieldsToCheck = P4Fields;}
            int canMove = 0;
            foreach(GameObject block in blocksToCheck)
            {
                LocalBRBlockBehaviourScript BBH = block.GetComponent<LocalBRBlockBehaviourScript>();
                foreach (GameObject neighbourBlock in blocksToCheck)
                {
                    LocalBRBlockBehaviourScript nBBH = neighbourBlock.GetComponent<LocalBRBlockBehaviourScript>();
                    if(BBH.value == nBBH.value)
                    {
                        if(BBH.TableNumberX == nBBH.TableNumberX && BBH.TableNumberY == nBBH.TableNumberY+1){canMove++;} 
                        if(BBH.TableNumberY == nBBH.TableNumberY && BBH.TableNumberX == nBBH.TableNumberX+1){canMove++;}
                    }
                }
            }
            
            
            foreach(GameObject field in fieldsToCheck)
            {
                FieldScript FS = field.GetComponent<FieldScript>();
                if(FS.isTaken == false && FS.isWall == false){canMove++;}
            }
            if(canMove == 0) 
            {
                // gameOverPanel.gameObject.SetActive(true);
                Debug.Log("GameOver for player: " + player);
            }
        }
    }


public void BlockLevelUp(long value, int owner, int x, int y) //#TODO This function can be optimized.
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
        // Debug.Log(value + " " + owner + " " + x + " " + y);
        int color = 0;
        if(owner == 1){color = Player1Color;}
        else if(owner == 2){color = Player2Color;}
        else if(owner == 3){color = Player3Color;}
        else if(owner == 4){color = Player4Color;}
        value = value*2;
        InstantiateThisColorWithThisOwner(color, value, owner, x, y);
        // blocks.Add(block);
        // block.GetComponent<BlockBehaviourScript>().AfterSpawn(x, y);
        // block.GetComponent<BlockBehaviourScript>().dir = "empty";
        // block.gameObject.name = "block" + blockID;
        // blockID++; 


    }

public void MoveBlocksForBRPlaces()
{
    int x = 0;
    int y = 0;
    
    foreach(GameObject P1Block in P1Blocks)
    {
        LocalBRBlockBehaviourScript BBH = P1Block.GetComponent<LocalBRBlockBehaviourScript>();
        x = BBH.TableNumberX;
        y = BBH.TableNumberY + 16;
        BBH.RefreshFields();
        BBH.AfterSpawn(x, y);
    }
    foreach(GameObject P2Block in P2Blocks)
    {
        LocalBRBlockBehaviourScript BBH = P2Block.GetComponent<LocalBRBlockBehaviourScript>();
        if(BBH.TableNumberX >=1 && BBH.TableNumberX <=4){x = BBH.TableNumberX + 16;}
        else if(BBH.TableNumberX >=7 && BBH.TableNumberX <=10){x = BBH.TableNumberX + 10;}
        if(BBH.TableNumberY >= 1 && BBH.TableNumberY <= 4){y = BBH.TableNumberY + 16;}
        else if(BBH.TableNumberY >=7 && BBH.TableNumberY <=10){y = BBH.TableNumberY + 10;}
        BBH.RefreshFields();
        BBH.AfterSpawn(x, y);
    }
    foreach(GameObject P3Block in P3Blocks)
    {
        LocalBRBlockBehaviourScript BBH = P3Block.GetComponent<LocalBRBlockBehaviourScript>();
        if(BBH.TableNumberX >=1 && BBH.TableNumberX <=4){x = BBH.TableNumberX;}
        else if(BBH.TableNumberX >=7 && BBH.TableNumberX <=10){x = BBH.TableNumberX -6;}
        else if(BBH.TableNumberX >=13 && BBH.TableNumberX <=16){x = BBH.TableNumberX -12;}
        if(BBH.TableNumberY >= 1 && BBH.TableNumberY <= 4){y = BBH.TableNumberY;}
        else if(BBH.TableNumberY >=7 && BBH.TableNumberY <=10){y = BBH.TableNumberY - 6;}
        else if(BBH.TableNumberY >=13 && BBH.TableNumberY <=16){y = BBH.TableNumberY -12;}
        BBH.RefreshFields();
        BBH.AfterSpawn(x, y);
    }
    foreach(GameObject P4Block in P4Blocks)
    {
        LocalBRBlockBehaviourScript BBH = P4Block.GetComponent<LocalBRBlockBehaviourScript>();
        if(BBH.TableNumberX >=7 && BBH.TableNumberX <=10){x = BBH.TableNumberX +10;}
        else if(BBH.TableNumberX >=13 && BBH.TableNumberX <=16){x = BBH.TableNumberX +4;}
        else if(BBH.TableNumberX >=19 && BBH.TableNumberX <=22){x = BBH.TableNumberX -2;}
        if(BBH.TableNumberY >=7 && BBH.TableNumberY <=10){y = BBH.TableNumberY - 6;}
        else if(BBH.TableNumberY >=13 && BBH.TableNumberY <=16){y = BBH.TableNumberY -12;}
        else if(BBH.TableNumberY >=19 && BBH.TableNumberY <=22){y = BBH.TableNumberY -18;}
        BBH.RefreshFields();
        BBH.AfterSpawn(x, y);
    }
}

}
