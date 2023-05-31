using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class LocalBRBlockBehaviourScript : MonoBehaviour
{

    [SerializeField] public int TableNumberX;
    [SerializeField] public int TableNumberY;
	public int TableNumberX_toCheck;
	public int TableNumberY_toCheck;

	[SerializeField] public float positionX;
    [SerializeField] public float positionY;


    [SerializeField] GameObject FieldSpawner;
    [SerializeField] GameObject BlockSpawner;
    [SerializeField] GameObject ObjectToRememberColors;
    SpawnFieldBR SpawnField;
    LocalBRSpawnBlock SpawnBlock;
    LocalBRBlockBehaviourScript NextBlockBehaviourScript;
    ScriptToRememberBRColors ScriptToRememberColors;

    public List<GameObject> fields;
    public List<GameObject> blocks;
    FieldScript FieldScript;

    [SerializeField] public int OwnerID;
    [SerializeField] public long value;
    public string dir = "empty";
	[SerializeField] public bool idle = true;
	[SerializeField] public bool waitingForDir = false;
	[SerializeField] public bool searching = false;
	[SerializeField] public bool finishedSearching = false;
	[SerializeField] public bool willMove = false;
	[SerializeField] public bool willBeDestroyed = false;
    [SerializeField] public bool willBeLevelingUp = false;
    [SerializeField] public bool moving = false;
	[SerializeField] public bool finishedMoving = false;
	public float pace = 1; // This variable is used to ensure, all blocks begin and finish move in the exactly same time, nevertheless the distance from start to finish
    public bool EnemyIsComputer;


    public GameObject settingsButton;
    
    public bool isPauseActive = false;

	Vector2 targetFieldPosition;


    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name != "LocalBR"){Destroy(gameObject);}
        
        dir = "empty";
		FieldSpawner = GameObject.Find("FieldSpawner"); 
    	SpawnField = FieldSpawner.GetComponent<SpawnFieldBR>();
        fields = SpawnField.fields; 
		BlockSpawner = GameObject.Find("BlockSpawner"); 
		SpawnBlock = BlockSpawner.GetComponent<LocalBRSpawnBlock>();
        ObjectToRememberColors = GameObject.Find("ObjectToRememberColors");
        ScriptToRememberColors = ObjectToRememberColors.GetComponent<ScriptToRememberBRColors>();
        // EnemyIsComputer = ScriptToRememberColors.EnemyIsComputer();

        // settingsButton = GameObject.Find("settings_button");
        // ShowSettingsPanel = settingsButton.GetComponent<ShowSettingsPanel>(); 
    }

    
    void Update()
    {
        
        TableNumberX_toCheck = TableNumberX;
		TableNumberY_toCheck = TableNumberY;
		// if(ShowSettingsPanel.isPauseActive == true)
		// {
		// 	isPauseActive = true;
		// }
		// else
		// {
			isPauseActive = false;
		// }


		if (isPauseActive == false)
		{

            if(waitingForDir == true)
			{
				if (Input.GetButtonDown("D") || Input.GetButtonDown("MoveRight")) { dir = "right"; }
				if (Input.GetButtonDown("A") || Input.GetButtonDown("MoveLeft")) { dir = "left"; }
				if (Input.GetButtonDown("W") || Input.GetButtonDown("MoveUp")) { dir = "up"; }
				if (Input.GetButtonDown("S") || Input.GetButtonDown("MoveDown")) { dir = "down"; }

                // if (SpawnBlock.Player2Turn == true && EnemyIsComputer == false && Input.GetButtonDown("MoveRight")) { dir = "right"; }
				// if (SpawnBlock.Player2Turn == true && EnemyIsComputer == false && Input.GetButtonDown("MoveLeft")) { dir = "left"; }
				// if (SpawnBlock.Player2Turn == true && EnemyIsComputer == false && Input.GetButtonDown("MoveUp")) { dir = "up"; }
				// if (SpawnBlock.Player2Turn == true && EnemyIsComputer == false && Input.GetButtonDown("MoveDown")) { dir = "down"; }

				if(dir != "empty") 
				{
					waitingForDir = false; 
					searching = true;
				}
			}

			if (searching == true)
			{
				if (dir == "right") { TableNumberX_toCheck++; }
				else if (dir == "left") { TableNumberX_toCheck--; }
				else if (dir == "up") { TableNumberY_toCheck++; }
				else if (dir == "down") { TableNumberY_toCheck--; }

				foreach (GameObject field in fields)
				{
					FieldScript = field.GetComponent<FieldScript>();
					
					if (FieldScript.TableNumberX == TableNumberX_toCheck && FieldScript.TableNumberY == TableNumberY_toCheck)
					{
						if (FieldScript.isWall == true)
						{
							//Natrafiliśmy na ścianę
							searching = false;
							finishedSearching = true;

						}

						else if (FieldScript.isWall == false && FieldScript.isTaken == false)
						{
							//Znaleźliśmy wolne pole i je zajmujemy
							willMove = true;
							TableNumberX = TableNumberX_toCheck;
							TableNumberY = TableNumberY_toCheck;
							targetFieldPosition = new Vector2(FieldScript.positionX, FieldScript.positionY);

							TakeNewField(TableNumberX, TableNumberY);
							ReleaseOldField(TableNumberX, TableNumberY, dir);
							
                            setPace();
                        }

						else if (FieldScript.isTaken == true && dir != "empty")
						{
							//Pole jest zajęte
							blocks = SpawnBlock.blocks;
                            foreach (GameObject block in blocks)
                            {
                                //Szukamy, który blok znajduje się na danym polu
                                if (block != null)
                                {
                                    NextBlockBehaviourScript = block.GetComponent<LocalBRBlockBehaviourScript>();
                                    if (TableNumberX_toCheck == NextBlockBehaviourScript.TableNumberX && TableNumberY_toCheck == NextBlockBehaviourScript.TableNumberY && block != this.gameObject)
                                    {
                                        //Znaleźliśmy blok zajmujący pole
                                        if((OwnerID !=0 && NextBlockBehaviourScript.OwnerID == 0) || (OwnerID == 0) || (OwnerID == NextBlockBehaviourScript.OwnerID))
                                        {
                                            //Blok zajmujący pole jest NEUTRALNY lub nasz lub my jesteśmy blokiem neutralnym
                                            if (NextBlockBehaviourScript.finishedSearching == true)
                                            {
                                                //Zajmujący pole blok, skończył szukanie swojej pozycji
                                                if (NextBlockBehaviourScript.willBeDestroyed == true)
                                                {
                                                    //Zajmujący pole blok zostanie zniszczony
                                                    searching = false;
                                                    finishedSearching = true;
                                                }
                                                else
                                                {
                                                    if (NextBlockBehaviourScript.value == value)
                                                    {
                                                        //Nastąpiło zderzenie z bliźniaczym blokiem
                                                        targetFieldPosition = new Vector2(FieldScript.positionX, FieldScript.positionY);
                                                        setPace();
                                                        TableNumberX = TableNumberX_toCheck;
                                                        TableNumberY = TableNumberY_toCheck;
                                                        ReleaseOldField(TableNumberX, TableNumberY, dir);
                                                    
                                                        searching = false;
                                                        finishedSearching = true;
                                                        willMove = true;
                                                        willBeDestroyed = true;
                                                        willBeLevelingUp = true;

                                                        NextBlockBehaviourScript.willBeDestroyed = true;
                                                        if(OwnerID == 0 && NextBlockBehaviourScript.OwnerID != 0)
                                                        {
                                                            //Jestem neutralnym blokiem, który zderzył się z blokiem ruszającego się gracza o tej samej wartościu lub jesteśmy oboje blokami ruszającego się gracza
                                                            OwnerID = NextBlockBehaviourScript.OwnerID;
                                                            SpawnBlock.NeutralBlocks.Remove(this.gameObject);
                                                            if(OwnerID == 1)
                                                            {
                                                                SpawnBlock.P1Blocks.Add(this.gameObject);
                                                            }
                                                            else if(OwnerID == 2)
                                                            {
                                                                SpawnBlock.P2Blocks.Add(this.gameObject);
                                                            }
                                                            else if(OwnerID == 3)
                                                            {
                                                                SpawnBlock.P3Blocks.Add(this.gameObject);
                                                            }
                                                            else if(OwnerID == 4)
                                                            {
                                                                SpawnBlock.P4Blocks.Add(this.gameObject);
                                                            }
                                                            
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //Nastąpiło zderzenie z blokiem o innej wartości
                                                        searching = false;
                                                        finishedSearching = true;
                                                    }
                                                }
                                            }
                                            else if(NextBlockBehaviourScript.idle == true)
                                            {
                                                //Jako blok neutralny zderzyliśmy się z blokiem gracza, który nie wykonuje swojego ruchu. Więc stajemy przed nim.
                                                searching = false;
                                                finishedSearching = true;
                                            }
                                        }
                                        else if(NextBlockBehaviourScript.OwnerID != OwnerID && NextBlockBehaviourScript.OwnerID != 0 && OwnerID != 0)
                                        {
                                            //Zderzenie między blokami dwóch graczy.
                                            if((NextBlockBehaviourScript.finishedSearching == true || NextBlockBehaviourScript.idle == true) && NextBlockBehaviourScript.willBeDestroyed == false)
                                            {
                                                //Nastąpiło zderzenie z blokiem przeciwnika
                                                if(NextBlockBehaviourScript.value > value)
                                                {
                                                    //Nie przesuwamy bloku przeciwnika, tylko się zatrzymujemy na nim
                                                    searching = false;
                                                    finishedSearching = true;
                                                }
                                                if(NextBlockBehaviourScript.value == value)
                                                {
                                                    // Łączymy się z blokiem przeciwnika
                                                    
                                                    // if(OwnerID == 1)
                                                    // {
                                                    //     SpawnBlock.P1Blocks.Add(block);
                                                    // }
                                                    // else if(OwnerID == 2)
                                                    // {
                                                    //     SpawnBlock.P2Blocks.Add(block);
                                                    // }
                                                    // else if(OwnerID == 3)
                                                    // {
                                                    //     SpawnBlock.P3Blocks.Add(block);
                                                    // }
                                                    // else if(OwnerID == 4)
                                                    // {
                                                    //     SpawnBlock.P4Blocks.Add(block);
                                                    // }


                                                    // if(NextBlockBehaviourScript.OwnerID == 0)
                                                    // {
                                                    //     SpawnBlock.NeutralBlocks.Remove(block);
                                                    // }
                                                    // else if(NextBlockBehaviourScript.OwnerID == 1)
                                                    // {
                                                    //     SpawnBlock.P1Blocks.Remove(block);
                                                    // }
                                                    // else if(NextBlockBehaviourScript.OwnerID == 2)
                                                    // {
                                                    //     SpawnBlock.P2Blocks.Remove(block);
                                                    // }
                                                    // else if(NextBlockBehaviourScript.OwnerID == 3)
                                                    // {
                                                    //     SpawnBlock.P3Blocks.Remove(block);
                                                    // }
                                                    // else if(NextBlockBehaviourScript.OwnerID == 4)
                                                    // {
                                                    //     SpawnBlock.P4Blocks.Remove(block);
                                                    // }
                                                    
                                                    // NextBlockBehaviourScript.OwnerID = OwnerID;
                                                    // NextBlockBehaviourScript.dir = dir;
                                                    // NextBlockBehaviourScript.idle = false;
                                                    // NextBlockBehaviourScript.finishedSearching = true;       
                                                    targetFieldPosition = new Vector2(FieldScript.positionX, FieldScript.positionY);
                                                    setPace();
                                                    TableNumberX = TableNumberX_toCheck;
                                                    TableNumberY = TableNumberY_toCheck;
                                                    ReleaseOldField(TableNumberX, TableNumberY, dir);

                                                            
                                                    searching = false;
                                                    finishedSearching = true;
                                                    willBeDestroyed = true;
                                                    willBeLevelingUp = true;
                                                    NextBlockBehaviourScript.willBeDestroyed = true;  
                                                    


                                                }
                                                if(NextBlockBehaviourScript.value < value)
                                                {
                                                    targetFieldPosition = new Vector2(FieldScript.positionX, FieldScript.positionY);
                                                    setPace();
                                                    TableNumberX = TableNumberX_toCheck;
                                                    TableNumberY = TableNumberY_toCheck;
                                                    ReleaseOldField(TableNumberX, TableNumberY, dir);
                                                
                                                    searching = false;
                                                    finishedSearching = true;
                                                    willMove = true;
                                                    NextBlockBehaviourScript.willBeDestroyed = true;
                                                    
                                                }
                                            }
                                        }
                                    }
                                }
                            }
						}
					}
				}
			}
			else if(moving == true)
			{
				//Wykonujemy ruch
				if(Math.Abs(transform.position.x - targetFieldPosition.x) < 0.4 && Math.Abs(transform.position.y - targetFieldPosition.y) < 0.4)
				{
					moving = false;
					finishedMoving = true;
				}
				transform.position = Vector2.MoveTowards(transform.position, targetFieldPosition, (pace * 0.1f));
			}
		}   
    }

    public void executeWaitingForDir()
	{
        waitingForDir = true;
		idle = false;
	}

	public void executeSearching() //SpawnBlock wywołuje kiedy możemy szukać swojej pozycji, tak aby wszystkie bloki zrobiły to na raz
    { //Po dodaniu executeWaitingForDir, ta funkcja nie będzie używana, bo bloki same będą ogłaszały, że już mogę
		searching = true;
		waitingForDir = false;
    }

    public void executeMove() //SpawnBlock wywołuje kiedy możemy się ruszyć, tak aby wszystkie bloki zrobiły to na raz
	{
		finishedSearching = false;
		if(willMove == true)
		{
			willMove = false;
			moving = true;
		}
		else if(willMove == false)
		{
			finishedMoving = true; 
		}
		
	}

	public void executeLevelUp() //SpawnBlock wywołuje kiedy mamy się zniszczyć, tak aby wszystkie bloki zrobiły to na raz
	{
		if(willBeDestroyed == true)
		{
			Destroy(gameObject);
		}
	}
	
	public void setPace() // Setting pace
	{
		
		if (Math.Abs(targetFieldPosition.x - transform.position.x) > 0.5f)
			{
				pace = Math.Abs(transform.position.x - targetFieldPosition.x);
			}
		if (Math.Abs(targetFieldPosition.y - transform.position.y) > 0.5f)
			{
				pace = Math.Abs(transform.position.y - targetFieldPosition.y);
			}
		
	}

    public void AfterSpawn(int x, int y)
    {
		FieldSpawner = GameObject.Find("FieldSpawner");
		SpawnField = FieldSpawner.GetComponent<SpawnFieldBR>();
		fields = SpawnField.fields;
	    TableNumberX = x;
        TableNumberY = y;
        foreach (GameObject field in fields)
        {
			FieldScript = field.gameObject.GetComponent<FieldScript>();
            if(FieldScript.TableNumberX == TableNumberX && FieldScript.TableNumberY == TableNumberY)
            {
				positionX = FieldScript.positionX;
                positionY = FieldScript.positionY;
                FieldScript.isTaken = true;
                FieldScript.GiveInfoAboutYourself(OwnerID, value);
            }
        }
        this.gameObject.transform.position = new Vector2(positionX, positionY);
	}

    public void ReleaseOldField(int x, int y, string blockDir) //Ten x i y są niewykorzystane. To na pewno będzie działało?
    {
		foreach (GameObject previousField in fields) 
        {
            FieldScript = previousField.gameObject.GetComponent<FieldScript>();
            if(blockDir == "right")
            {
                if(FieldScript.TableNumberX == TableNumberX-1 && FieldScript.TableNumberY == TableNumberY){FieldScript.isTaken = false; FieldScript.blockValue = 0; FieldScript.blockOwnerID = -1;}
            }
            else if(blockDir == "left")
            {
                if(FieldScript.TableNumberX == TableNumberX+1 && FieldScript.TableNumberY == TableNumberY){FieldScript.isTaken = false;  FieldScript.blockValue = 0; FieldScript.blockOwnerID = -1;}
            }
            else if(blockDir == "up")
            {
                if(FieldScript.TableNumberX == TableNumberX && FieldScript.TableNumberY == TableNumberY-1){FieldScript.isTaken = false;  FieldScript.blockValue = 0; FieldScript.blockOwnerID = -1;}
            }
            else if(blockDir == "down")
            {
                if(FieldScript.TableNumberX == TableNumberX && FieldScript.TableNumberY == TableNumberY+1){FieldScript.isTaken = false;  FieldScript.blockValue = 0; FieldScript.blockOwnerID = -1;}    
            }
        }
    }

        public void TakeNewField(int x, int y)
    {
		foreach (GameObject nextField in fields)
        {
            FieldScript = nextField.gameObject.GetComponent<FieldScript>();
            if(FieldScript.TableNumberX == TableNumberX && FieldScript.TableNumberY == TableNumberY)
            {
                FieldScript.isTaken = true;
                FieldScript.GiveInfoAboutYourself(OwnerID, value);
            }
        }
    }

    public void RefreshFields()
    {
        fields.TrimExcess();
        fields = SpawnField.fields; 
    }

	private void OnDestroy() 
	{
		if(willBeLevelingUp == true) 
		{
			SpawnBlock.BlockLevelUp(value, OwnerID, TableNumberX, TableNumberY);
		}
		SpawnBlock.blocks.Remove(gameObject);
        if(OwnerID == 0)
        {
            SpawnBlock.NeutralBlocks.Remove(this.gameObject);
        }
        else if(OwnerID == 1)
        {
            SpawnBlock.P1Blocks.Remove(this.gameObject);
        }
        else if(OwnerID == 2)
        {
            SpawnBlock.P2Blocks.Remove(this.gameObject);
        }
        else if(OwnerID == 3)
        {
            SpawnBlock.P3Blocks.Remove(this.gameObject);
        }
        else if(OwnerID == 4)
        {
            SpawnBlock.P4Blocks.Remove(this.gameObject);
        }
	}
}
