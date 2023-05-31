using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LocalBRSpawnBlock : MonoBehaviour
{
GameObject block;
FieldScript FieldScript;
SpawnFieldBR SpawnField;
[SerializeField] GameObject Camera;
[SerializeField] GameObject FieldSpawner;
public GameObject gameOverPanel;
[SerializeField] Text winnerAnnouncemenet;
[SerializeField] TextMeshProUGUI TurnPlayerNumber;
[SerializeField] public GameObject TurnColorPanel;
[SerializeField] Image TurnColorImg;
[SerializeField] GameObject UI_Controller;
LocalBRPowerUps LocalBRPowerUps;
GameObject ObjectToRememberColors;
ScriptToRememberBRColors ScriptToRememberColors;
public UIControlling_BRLocal UIControlling;
public LocalBRBlockBehaviourScript BlockBehaviourScript;

int blockID = 0;
public int Player1Color = 0;
public int Player2Color = 0;
public int Player3Color = 0;
public int Player4Color = 0;
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
public List<FieldScript> FreeFields;
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

public bool isCheckingTime = true;
public bool needsToIndicatePlacing = false;
public bool placingIsFinished = false;

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

public int activePlayersCounter = 0;
public int turnsToStartBR = 0;


void Start()
{
    UIControlling = UI_Controller.GetComponent<UIControlling_BRLocal>();
    LocalBRPowerUps = GetComponent<LocalBRPowerUps>();
    TurnColorImg = TurnColorPanel.gameObject.GetComponent<Image>();
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
        activePlayersCounter++;
        LookForPlaceToSpawnBlockAndPlaceIt(1);
        ChangePanelColor(Player1Color);
        
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
        activePlayersCounter++;
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
        activePlayersCounter++;
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
        activePlayersCounter++;
        LookForPlaceToSpawnBlockAndPlaceIt(4);
    }

    UIControlling.activePlayersCounter = activePlayersCounter;
    UIControlling.ChangeTurnNumber(turnsToStartBR);
    
}

void Update()
{
    if(isCheckingTime == true && needsToIndicatePlacing == false)
    {
        Debug.Log("Update");
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
                if (BlockBehaviourScript.willMove == true  || BlockBehaviourScript.willBeDestroyed == true) {willMoveCounter++;}
                if (BlockBehaviourScript.finishedMoving == true) {finishedMovingCounter++;}
            }
        }

        if(idleCounter == blocks.Count)
        {
            CheckForGameOver(0);
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
                if(Waiting == false)
                {
                    if(BlockBehaviourScript.OwnerID == 0)
                    {
                        BlockBehaviourScript.executeWaitingForDir();
                    }
                }
            }
            Waiting = true;

        }
        
        if(((Player1Turn == true && finishedSearchingCounter == P1Blocks.Count + NeutralBlocks.Count) || (Player2Turn == true && finishedSearchingCounter == P2Blocks.Count + NeutralBlocks.Count) || (Player3Turn == true && finishedSearchingCounter == P3Blocks.Count + NeutralBlocks.Count) || (Player4Turn == true && finishedSearchingCounter == P4Blocks.Count + NeutralBlocks.Count)) && willMoveCounter > 0)
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

                if(BlockBehaviourScript.OwnerID == 0)
                {
                    BlockBehaviourScript.executeMove();
                } 
            }
        }
        else if(((Player1Turn == true && finishedSearchingCounter == P1Blocks.Count + NeutralBlocks.Count) || (Player2Turn == true && finishedSearchingCounter == P2Blocks.Count + NeutralBlocks.Count) || (Player3Turn == true && finishedSearchingCounter == P3Blocks.Count + NeutralBlocks.Count) || (Player4Turn == true && finishedSearchingCounter == P4Blocks.Count + NeutralBlocks.Count)) && willMoveCounter == 0)
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
        
        if((Player1Turn == true && finishedMovingCounter == P1Blocks.Count + NeutralBlocks.Count) || (Player2Turn == true && finishedMovingCounter == P2Blocks.Count + NeutralBlocks.Count) || (Player3Turn == true && finishedMovingCounter == P3Blocks.Count + NeutralBlocks.Count) || (Player4Turn == true && finishedMovingCounter == P4Blocks.Count + NeutralBlocks.Count))
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

            CheckForGameOver(0);
            if(Player1Turn == true)
            {
                CheckForGameOver(0);
                if(isPlayer2Playing)
                {
                    Player1Turn = false;
                    Player2Turn = true;
                    if(CheckForPlacingPossibilities(2) > 0)
                    {
                        needsToIndicatePlacing = true;
                        isCheckingTime = false;
                    }
                    else
                    {
                        StartCoroutine(SkipPlacingBlocksFromLackOfSpace(2));
                        if(CheckForMovementPossibilities(2))
                        {
                            LookForPlaceToSpawnBlockAndPlaceIt(2);
                        }
                        else 
                        {
                        SkipPlayerMovementFromLackOfSpace(2);
                        }
                    }
                    ChangePanelColor(Player2Color);
                    TurnPlayerNumber.text = "Player 2";
                    Waiting = false;
                }
                else if(isPlayer3Playing)
                {
                    Player1Turn = false;
                    Player3Turn = true;
                    if(CheckForPlacingPossibilities(3) > 0)
                    {
                        needsToIndicatePlacing = true;
                        isCheckingTime = false;
                    }
                    else
                    {
                        StartCoroutine(SkipPlacingBlocksFromLackOfSpace(3));
                        if(CheckForMovementPossibilities(3))
                        {
                            LookForPlaceToSpawnBlockAndPlaceIt(3);
                        }
                        else 
                        {
                        SkipPlayerMovementFromLackOfSpace(3);
                        }
                    }
                    ChangePanelColor(Player3Color);
                    TurnPlayerNumber.text = "Player 3";
                    Waiting = false;
                }
                else if(isPlayer4Playing)
                {
                    Player1Turn = false;
                    Player4Turn = true;
                    if(CheckForPlacingPossibilities(4) > 0)
                    {
                        needsToIndicatePlacing = true;
                        isCheckingTime = false;
                    }
                    else
                    {
                        StartCoroutine(SkipPlacingBlocksFromLackOfSpace(4));
                        if(CheckForMovementPossibilities(4))
                        {
                            LookForPlaceToSpawnBlockAndPlaceIt(4);
                        }
                        else 
                        {
                        SkipPlayerMovementFromLackOfSpace(4);
                        }
                    }
                    ChangePanelColor(Player4Color);
                    TurnPlayerNumber.text = "Player 4";
                    Waiting = false;
                }
            }
            else if (Player2Turn == true)
            {
                CheckForGameOver(0);
                if(isPlayer3Playing)
                {
                    Player2Turn = false;
                    Player3Turn = true;
                    if(CheckForPlacingPossibilities(3) > 0)
                    {
                        needsToIndicatePlacing = true;
                        isCheckingTime = false;
                    }
                    else
                    {
                        StartCoroutine(SkipPlacingBlocksFromLackOfSpace(3));
                        if(CheckForMovementPossibilities(3))
                        {
                            LookForPlaceToSpawnBlockAndPlaceIt(3);
                        }
                        else 
                        {
                        SkipPlayerMovementFromLackOfSpace(3);
                        }
                    }

                    ChangePanelColor(Player3Color);
                    TurnPlayerNumber.text = "Player 3";
                    Waiting = false;
                }
                else if(isPlayer4Playing)
                {
                    Player2Turn = false;
                    Player4Turn = true;
                    if(CheckForPlacingPossibilities(4) > 0)
                    {
                        needsToIndicatePlacing = true;
                        isCheckingTime = false;
                    }
                    else
                    {
                        StartCoroutine(SkipPlacingBlocksFromLackOfSpace(4));
                        if(CheckForMovementPossibilities(4))
                        {
                            LookForPlaceToSpawnBlockAndPlaceIt(4);
                        }
                        else 
                        {
                        SkipPlayerMovementFromLackOfSpace(4);
                        }
                    }
                    ChangePanelColor(Player4Color);
                    TurnPlayerNumber.text = "Player 4";
                    Waiting = false;
                }
                else if(isPlayer1Playing)
                {
                    Player2Turn = false;
                    Player1Turn = true;
                    if(CheckForPlacingPossibilities(1) > 0)
                    {
                        needsToIndicatePlacing = true;
                        isCheckingTime = false;
                    }
                    else
                    {
                        StartCoroutine(SkipPlacingBlocksFromLackOfSpace(1));
                        if(CheckForMovementPossibilities(1))
                        {
                            LookForPlaceToSpawnBlockAndPlaceIt(1);
                        }
                        else 
                        {
                        SkipPlayerMovementFromLackOfSpace(1);
                        }
                    }
                    ChangePanelColor(Player1Color);
                    TurnPlayerNumber.text = "Player 1";
                    Waiting = false;
                }
            }
            else if (Player3Turn == true)
            {
                CheckForGameOver(0);
                if(isPlayer4Playing)
                {
                    Player3Turn = false;
                    Player4Turn = true;
                    if(CheckForPlacingPossibilities(3) > 0)
                    {
                        needsToIndicatePlacing = true;
                        isCheckingTime = false;
                    }
                    else
                    {
                        StartCoroutine(SkipPlacingBlocksFromLackOfSpace(3));
                        if(CheckForMovementPossibilities(3))
                        {
                            LookForPlaceToSpawnBlockAndPlaceIt(3);
                        }
                        else 
                        {
                        SkipPlayerMovementFromLackOfSpace(3);
                        }
                    }
                    ChangePanelColor(Player4Color);
                    TurnPlayerNumber.text = "Player 4";
                    Waiting = false;
                }
                else if(isPlayer1Playing)
                {
                    Player3Turn = false;
                    Player1Turn = true;
                    if(CheckForPlacingPossibilities(1) > 0)
                    {
                        needsToIndicatePlacing = true;
                        isCheckingTime = false;
                    }
                    else
                    {
                        StartCoroutine(SkipPlacingBlocksFromLackOfSpace(1));
                        if(CheckForMovementPossibilities(1))
                        {
                            LookForPlaceToSpawnBlockAndPlaceIt(1);
                        }
                        else 
                        {
                        SkipPlayerMovementFromLackOfSpace(1);
                        }
                    }
                    ChangePanelColor(Player1Color);
                    TurnPlayerNumber.text = " Player 1";
                    Waiting = false;
                }
                else if(isPlayer2Playing)
                {
                    Player3Turn = false;
                    Player2Turn = true;
                    if(CheckForPlacingPossibilities(2) > 0)
                    {
                        needsToIndicatePlacing = true;
                        isCheckingTime = false;
                    }
                    else
                    {
                        StartCoroutine(SkipPlacingBlocksFromLackOfSpace(2));
                        if(CheckForMovementPossibilities(2))
                        {
                            LookForPlaceToSpawnBlockAndPlaceIt(2);
                        }
                        else 
                        {
                        SkipPlayerMovementFromLackOfSpace(2);
                        }
                    }
                    ChangePanelColor(Player2Color);
                    TurnPlayerNumber.text = "Player 2";
                    Waiting = false;
                }
            }
            else if (Player4Turn == true)
            {
                CheckForGameOver(0); 
                if(isPlayer1Playing)
                {
                    Player4Turn = false;
                    Player1Turn = true;
                    if(CheckForPlacingPossibilities(4) > 0)
                    {
                        needsToIndicatePlacing = true;
                        isCheckingTime = false;
                    }
                    else
                    {
                        StartCoroutine(SkipPlacingBlocksFromLackOfSpace(4));
                        if(CheckForMovementPossibilities(4))
                        {
                            LookForPlaceToSpawnBlockAndPlaceIt(4);
                        }
                        else 
                        {
                        SkipPlayerMovementFromLackOfSpace(4);
                        }
                    }
                    ChangePanelColor(Player1Color);
                    TurnPlayerNumber.text = "Player 1";
                    Waiting = false;
                }
                else if(isPlayer2Playing)
                {
                    Player4Turn = false;
                    Player2Turn = true;
                    if(CheckForPlacingPossibilities(2) > 0)
                    {
                        needsToIndicatePlacing = true;
                        isCheckingTime = false;
                    }
                    else
                    {
                        StartCoroutine(SkipPlacingBlocksFromLackOfSpace(2));
                        if(CheckForMovementPossibilities(2))
                        {
                            LookForPlaceToSpawnBlockAndPlaceIt(2);
                        }
                        else 
                        {
                        SkipPlayerMovementFromLackOfSpace(2);
                        }
                    }
                    ChangePanelColor(Player2Color);
                    TurnPlayerNumber.text = "Player 2";
                    Waiting = false;
                }
                else if(isPlayer3Playing)
                {
                    Player4Turn = false;
                    Player3Turn = true;
                    if(CheckForPlacingPossibilities(3) > 0)
                    {
                        needsToIndicatePlacing = true;
                        isCheckingTime = false;
                    }
                    else
                    {
                        StartCoroutine(SkipPlacingBlocksFromLackOfSpace(3));
                        if(CheckForMovementPossibilities(3))
                        {
                            LookForPlaceToSpawnBlockAndPlaceIt(3);
                        }
                        else 
                        {
                        SkipPlayerMovementFromLackOfSpace(3);
                        }
                    }
                    ChangePanelColor(Player3Color);
                    TurnPlayerNumber.text = "Player 3";
                    Waiting = false;
                }
            }
        }
    }
    else if(isCheckingTime == false && needsToIndicatePlacing == true)
    {
        int activePlayerID = 0;
        if(Player1Turn == true){activePlayerID = 1;}
        else if(Player2Turn == true){activePlayerID = 2;}
        else if(Player3Turn == true){activePlayerID = 3;}
        else if(Player4Turn == true){activePlayerID = 4;}
        
       
        PickPositionsToPlaceBlocks(activePlayerID);

        
        
    }
    else if(placingIsFinished == true)
    {
        int activePlayerID = 0;
        if(Player1Turn == true){activePlayerID = 1;}
        else if(Player2Turn == true){activePlayerID = 2;}
        else if(Player3Turn == true){activePlayerID = 3;}
        else if(Player4Turn == true){activePlayerID = 4;}

        isCheckingTime = true;
        needsToIndicatePlacing = false;
        placingIsFinished = false;
        if(CheckForMovementPossibilities(activePlayerID))
        {
            LookForPlaceToSpawnBlockAndPlaceIt(activePlayerID);
        }
        else 
        {
            SkipPlayerMovementFromLackOfSpace(activePlayerID);
        }
    }     
}



public void InstantiateThisColorWithThisOwner(int color, long value, int owner, int X, int Y)
{
    string value_string;
    string color_string = "n";
    value_string = value.ToString();
    if(color == 0){color_string = "n";}
    else if(color == 1){color_string = "b";}
    else if(color == 2){color_string = "r";}
    else if(color == 3){color_string = "g";}
    else if(color == 4){color_string = "p";}
    else if(color == 5){color_string = "s";}
    GameObject block = (GameObject)Instantiate(Resources.Load("Local/BR/" + value_string + color_string));
    blocks.Add(block);
    if(owner == 0){NeutralBlocks.Add(block);}
    else if(owner == 1){P1Blocks.Add(block);}
    else if(owner == 2){P2Blocks.Add(block);}
    else if(owner == 3){P3Blocks.Add(block);}
    else if(owner == 4){P4Blocks.Add(block);}
    block.gameObject.GetComponent<LocalBRBlockBehaviourScript>().OwnerID = owner;
    block.gameObject.GetComponent<LocalBRBlockBehaviourScript>().AfterSpawn(X, Y);
    block.gameObject.name = "block" + blockID;
    blockID++;
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
                        UIControlling.ChangeTurnNumber(turnsToStartBR);
                        if(turnsToStartBR == 0)
                        {
                            Debug.Log("BR START");
                            isPreparingFaze = false;
                            SpawnField.initiateBRFaze();
                            Camera.GetComponent<CameraBRScript>().MoveForBR();
                            UIControlling.MoveForBR();
                            MoveBlocksForBRPlaces();
                            fields = SpawnField.fields;
                            fields.TrimExcess();
                            isBRFaze = true;
                        }
                    }
                }

            }
            
        }
    }
    else if(isBRFaze)
    {
        int fieldCounter = 0;
        int fieldOfPlayer = 0;
        int color = 0;
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

                        InstantiateThisColorWithThisOwner(0, 2, 0, FieldScript.TableNumberX, FieldScript.TableNumberY);
                      
                        
                        blockSpawned = true;
                        
                        ClearAfterSpawn();
                        CheckForGameOver(owner);
                        turnsToStartBR--;
                        UIControlling.ChangeTurnNumber(turnsToStartBR);
                       
                    }
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

public void CheckForGameOver(int player)
    {
        if(isPreparingFaze && player != 0)
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
                if(player == 1)
                {
                    Debug.Log("Gracz 1 przegrywa");
                    UIControlling.AnnounceELiminatedPlayer(1);
                    isPlayer1Playing = false;
                    activePlayersCounter--;
                }
                else if(player == 2)
                {
                    Debug.Log("Gracz 2 przegrywa");
                    UIControlling.AnnounceELiminatedPlayer(2);
                    isPlayer2Playing = false;
                    activePlayersCounter--;
                }
                else if(player == 3)
                {
                    Debug.Log("Gracz 3 przegrywa");
                    UIControlling.AnnounceELiminatedPlayer(3);
                    isPlayer3Playing = false;
                    activePlayersCounter--;
                }
                else if(player == 4)
                {
                    Debug.Log("Gracz 4 przegrywa");
                    UIControlling.AnnounceELiminatedPlayer(4);
                    isPlayer4Playing = false;
                    activePlayersCounter--;
                }
            }
        }
        else if(isBRFaze)
        {
            player = 0;
            blocks.TrimExcess();
            if(isPlayer1Playing)
            {
                P1Blocks.TrimExcess();
                if(P1Blocks.Count == 0){
                    Debug.Log("Gracz 1 przegrywa");
                    isPlayer1Playing = false;
                    activePlayersCounter--;
                    UIControlling.AnnounceELiminatedPlayer(1);
                }
            }
            if(isPlayer2Playing)
            {
                P2Blocks.TrimExcess();
                if(P2Blocks.Count == 0){
                    Debug.Log("Gracz 2 przegrywa");
                    isPlayer2Playing = false;
                    activePlayersCounter--;
                    UIControlling.AnnounceELiminatedPlayer(2);
                }
            }
            if(isPlayer3Playing)
            {
                P3Blocks.TrimExcess();
                if(P3Blocks.Count == 0){
                    Debug.Log("Gracz 3 przegrywa");
                    isPlayer3Playing = false;
                    activePlayersCounter--;
                    UIControlling.AnnounceELiminatedPlayer(3);
                }
            }
            if(isPlayer4Playing)
            {
                P4Blocks.TrimExcess();
                if(P4Blocks.Count == 0){
                    Debug.Log("Gracz 4 przegrywa");
                    isPlayer4Playing = false;
                    activePlayersCounter--;
                    UIControlling.AnnounceELiminatedPlayer(4);
                }
            }
        }

        int stillPlayingCounter = 0;
        if(isPlayer1Playing){stillPlayingCounter++;}
        if(isPlayer2Playing){stillPlayingCounter++;}
        if(isPlayer3Playing){stillPlayingCounter++;}
        if(isPlayer4Playing){stillPlayingCounter++;}
        if(stillPlayingCounter == 1)
        {
            Debug.Log("Gra zakończona wygraną gracza");
        }
        if(stillPlayingCounter == 0)
        {
            Debug.Log("Gra zakończona remisem");
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

public void ChangePanelColor(int color)
    {
        if(color == 1){TurnColorImg.color = new Color32(119,221,250,255);}
        else if(color == 2){TurnColorImg.color = new Color32(242,118,140,255);}
        else if(color == 3){TurnColorImg.color = new Color32(137,217,171,255);}
        else if(color == 4){TurnColorImg.color = new Color32(236,123,222,255);}
        else if(color == 5){TurnColorImg.color = new Color32(104,105,104,255);}

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

public int CheckForPlacingPossibilities(int player)
{
    int placesToPlaceBlock = 0;
    List<GameObject> BlocksOfPlayers = new List<GameObject>();
    if(player == 1){BlocksOfPlayers = P1Blocks;}
    else if(player == 2){BlocksOfPlayers = P2Blocks;}
    else if(player == 3){BlocksOfPlayers = P3Blocks;}
    else if(player == 4){BlocksOfPlayers = P4Blocks;}
    int x;
    int y;
    foreach(GameObject block in BlocksOfPlayers)
    {
        LocalBRBlockBehaviourScript thisBBH = block.GetComponent<LocalBRBlockBehaviourScript>();
        x = thisBBH.TableNumberX;
        y = thisBBH.TableNumberY;
        

        foreach(GameObject field in fields)
        {
            
            FieldScript thisFieldScript = field.GetComponent<FieldScript>();
            if((thisFieldScript.TableNumberX == x+1 && thisFieldScript.TableNumberY == y) || (thisFieldScript.TableNumberX == x-1 && thisFieldScript.TableNumberY == y) || (thisFieldScript.TableNumberX == x && thisFieldScript.TableNumberY == y+1) || (thisFieldScript.TableNumberX == x && thisFieldScript.TableNumberY == y-1))
            {
                if(thisFieldScript.isWall == false && thisFieldScript.isTaken == false && thisFieldScript.chceckedForBeingFree == false)
                {
                    thisFieldScript.chceckedForBeingFree = true;
                    // Pamiętaj później dodać funkcję, która zwolni powyższy parametr z pól.
                    FreeFields.Add(thisFieldScript);
                    placesToPlaceBlock++;
                }
            }
        }
    }

    foreach(GameObject fieldToForget in fields)
    {
        fieldToForget.GetComponent<FieldScript>().chceckedForBeingFree = false;
    }

    return placesToPlaceBlock;
}


public bool CheckForMovementPossibilities(int player)
{
    bool ismovementPossible = false;
    List<GameObject> BlocksOfPlayers = new List<GameObject>();
    if(player == 1){BlocksOfPlayers = P1Blocks;}
    else if(player == 2){BlocksOfPlayers = P2Blocks;}
    else if(player == 3){BlocksOfPlayers = P3Blocks;}
    else if(player == 4){BlocksOfPlayers = P4Blocks;}
    int x;
    int y;
    foreach(GameObject block in BlocksOfPlayers)
    {
        LocalBRBlockBehaviourScript thisBBH = block.GetComponent<LocalBRBlockBehaviourScript>();
        x = thisBBH.TableNumberX;
        y = thisBBH.TableNumberY;
        

        foreach(GameObject field in fields)
        {
            
            FieldScript thisFieldScript = field.GetComponent<FieldScript>();
            if((thisFieldScript.TableNumberX == x+1 && thisFieldScript.TableNumberY == y) || (thisFieldScript.TableNumberX == x-1 && thisFieldScript.TableNumberY == y) || (thisFieldScript.TableNumberX == x && thisFieldScript.TableNumberY == y+1) || (thisFieldScript.TableNumberX == x && thisFieldScript.TableNumberY == y-1))
            {
                if(thisFieldScript.isWall == false && thisFieldScript.isTaken == false && thisFieldScript.chceckedForBeingFree == false)
                {
                    thisFieldScript.chceckedForBeingFree = true;
                    ismovementPossible = true;
                    return true;
                }


                
                //Skrypt poniżej mówi że nie można się ruszyć, w sytuacji w której żaden z bloków gracza nie ma bezpośredniego sąsiada.
                else if(thisFieldScript.isWall == false && thisFieldScript.isTaken == true && ismovementPossible == false)
                {
                    int thisFieldOwner = thisFieldScript.blockOwnerID;
                    long thisFieldBlockValue = thisFieldScript.blockValue;

                    if(thisFieldBlockValue == thisBBH.value)
                    {
                        ismovementPossible = true;
                        return true;
                    }

                    else if(thisFieldOwner != 0 && thisFieldOwner != player && thisFieldBlockValue <= thisBBH.value)
                    {
                        ismovementPossible = true;
                        return true;
                    }

                    else if(thisFieldOwner == 0)
                    {
                        bool isMovementHereImpossible = false;
                        int multiplierX = 0;
                        int multiplierY = 0;
                        int thisFieldX = thisFieldScript.TableNumberX;
                        int thisFieldY = thisFieldScript.TableNumberY;
                        long referencePrevoiusValue = thisFieldBlockValue;

                        if(thisFieldX > x){multiplierX++;}
                        else if (thisFieldX < x){multiplierX--;}
                        else if(thisFieldY > y){multiplierY++;}
                        else if(thisFieldY < y){multiplierY--;}

                        do
                        {
                            
                            
                            foreach(GameObject nextField in fields)
                            {
                                FieldScript nextFieldScript = nextField.GetComponent<FieldScript>();
                                if(nextFieldScript.TableNumberX == thisFieldX + multiplierX && nextFieldScript.TableNumberY == thisFieldY + multiplierY)
                                {
                                    if(nextFieldScript.isWall == true)
                                    {
                                        isMovementHereImpossible = true;

                                
                                    }
                                    else if(nextFieldScript.isTaken == false)
                                    {
                                        ismovementPossible = true;
                                        return true;
                                    }
                                    else if(nextFieldScript.blockOwnerID != 0)
                                    {
                                        isMovementHereImpossible = true;
                                        
                                    }
                                    else if(nextFieldScript.blockOwnerID == 0)
                                    {
                                        if(nextFieldScript.blockValue == referencePrevoiusValue)
                                        {
                                            ismovementPossible = true;
                                            return true;
                                        }
                                        else
                                        {
                                            referencePrevoiusValue = nextFieldScript.blockValue;
                                            if(multiplierX > 0){multiplierX++;}
                                            else if(multiplierX < 0){multiplierX--;}
                                            else if(multiplierY > 0){multiplierY++;}
                                            else if(multiplierY < 0){multiplierY--;}

                                        }
                                    }
                                }
                            }
                        }while(ismovementPossible == false && isMovementHereImpossible == false);
                        
                    }
                }
                

            }
           

        }
    }
    return false;
}

public void PickPositionsToPlaceBlocks(int player)
    {
        isCheckingTime = false;
        needsToIndicatePlacing = false;
        if(isBRFaze)
        {
            LocalBRPowerUps.PositionSelectorForBlockPlacing(FreeFields);
            FreeFields.Clear();
        }
        else
        {
            FreeFields.Clear();
            placingIsFinished = true;

        }
    }

public IEnumerator SkipPlacingBlocksFromLackOfSpace(int player)
    {
        placingIsFinished = true;
        yield return null;
    }

public void SkipPlayerMovementFromLackOfSpace(int player)
{
    if(isBRFaze)
    {
        UIControlling.AnnounceMoveSkip(player);
    }
    else
    {
        LookForPlaceToSpawnBlockAndPlaceIt(player);
    }
}


}
