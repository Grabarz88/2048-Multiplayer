using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LocalVSBlockBehaviourScript : MonoBehaviour
{
    
    [SerializeField] public int TableNumberX;
    [SerializeField] public int TableNumberY;
	public int TableNumberX_toCheck;
	public int TableNumberY_toCheck;

	[SerializeField] public float positionX;
    [SerializeField] public float positionY;


    [SerializeField] GameObject FieldSpawner;
    [SerializeField] GameObject BlockSpawner;
    SpawnField SpawnField;
    LocalVsSpawnBlock SpawnBlock;
    LocalVSBlockBehaviourScript NextBlockBehaviourScript;

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

	


    public GameObject pauseButton;
	public GameObject exitButton;
    public bool isPauseActive = false;

	Vector2 targetFieldPosition;

    void Start()
    {
        dir = "empty";
		FieldSpawner = GameObject.Find("FieldSpawner"); 
    	SpawnField = FieldSpawner.GetComponent<SpawnField>();
		BlockSpawner = GameObject.Find("LocalVSBlockSpawner"); 
		SpawnBlock = BlockSpawner.GetComponent<LocalVsSpawnBlock>();
    	fields = SpawnField.fields; 
		pauseButton = GameObject.Find("Restart");  
		exitButton = GameObject.Find("Back"); 
    }


    void Update()
    {
        TableNumberX_toCheck = TableNumberX;
		TableNumberY_toCheck = TableNumberY;
		if(pauseButton.GetComponent<ShowRestartPanel>().isPauseActive || exitButton.GetComponent<ShowExitPanel>().isPauseActive)
		{
			isPauseActive = true;
		}
		else
		{
			isPauseActive = false;
		}


		if (isPauseActive == false)
		{

            if(waitingForDir == true)
			{
				if (SpawnBlock.Player1Turn == true && Input.GetButtonDown("D")) { dir = "right"; }
				if (SpawnBlock.Player1Turn == true && Input.GetButtonDown("A")) { dir = "left"; }
				if (SpawnBlock.Player1Turn == true && Input.GetButtonDown("W")) { dir = "up"; }
				if (SpawnBlock.Player1Turn == true && Input.GetButtonDown("S")) { dir = "down"; }

                if (SpawnBlock.Player2Turn == true && Input.GetButtonDown("MoveRight")) { dir = "right"; }
				if (SpawnBlock.Player2Turn == true && Input.GetButtonDown("MoveLeft")) { dir = "left"; }
				if (SpawnBlock.Player2Turn == true && Input.GetButtonDown("MoveUp")) { dir = "up"; }
				if (SpawnBlock.Player2Turn == true && Input.GetButtonDown("MoveDown")) { dir = "down"; }

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
                                    NextBlockBehaviourScript = block.GetComponent<LocalVSBlockBehaviourScript>();
                                    if (TableNumberX_toCheck == NextBlockBehaviourScript.TableNumberX && TableNumberY_toCheck == NextBlockBehaviourScript.TableNumberY && block != this.gameObject)
                                    {
                                        //Znaleźliśmy blok zajmujący pole
                                        if((OwnerID !=0 && NextBlockBehaviourScript.OwnerID == 0) || (OwnerID == 0))
                                        {
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
                                                        if(NextBlockBehaviourScript.OwnerID != 0)
                                                        {
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
                                            //Nastąpiło zderzenie z blokiem przeciwnika
                                            if(NextBlockBehaviourScript.value > value)
                                            {
                                                //Nie przesuwamy bloku przeciwnika, tylko się zatrzymujemy na nim
                                                searching = false;
                                                finishedSearching = true;
                                            }
                                            if(NextBlockBehaviourScript.value == value)
                                            {
                                                
                                                // if(OwnerID == 1)
                                                // {
                                                //     SpawnBlock.P1Blocks.Add(block);
                                                // }
                                                // if(OwnerID == 2)
                                                // {
                                                //     SpawnBlock.P2Blocks.Add(block);
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
                                                
                                                // NextBlockBehaviourScript.OwnerID = OwnerID;
                                                // NextBlockBehaviourScript.dir = dir;
                                                // NextBlockBehaviourScript.idle = false;
                                                // NextBlockBehaviourScript.searching = true;                   
                                                searching = false;
                                                finishedSearching = true;

                                            }
                                            if(NextBlockBehaviourScript.value < value)
                                            {

				                                transform.position = new Vector2(FieldScript.positionX, FieldScript.positionY);
                                                Destroy(block);
                                                SpawnBlock.AnnounceGameOver(OwnerID);
                                                // Debug.Log("Wygrywa gracz " + OwnerID);
                                                // if(NextBlockBehaviourScript.finishedSearching == false)
                                                // {
                                                //     NextBlockBehaviourScript.dir = dir;
                                                //     NextBlockBehaviourScript.idle = false;
                                                //     NextBlockBehaviourScript.searching = true;
                                                // }
                                                
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
				transform.position = Vector2.MoveTowards(transform.position, targetFieldPosition, (pace * 0.2f));
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


    public void AfterSpawn(int x, int y, int owner)
    {
		FieldSpawner = GameObject.Find("FieldSpawner");
		SpawnField = FieldSpawner.GetComponent<SpawnField>();
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
            }
        }
        this.gameObject.transform.position = new Vector2(positionX, positionY);
        OwnerID = owner;

		
	}

        public void ReleaseOldField(int x, int y, string blockDir) //Ten x i y są niewykorzystane. To na pewno będzie działało?
    {
		foreach (GameObject previousField in fields) 
        {
            FieldScript = previousField.gameObject.GetComponent<FieldScript>();
            if(blockDir == "right")
            {
                if(FieldScript.TableNumberX == TableNumberX-1 && FieldScript.TableNumberY == TableNumberY){FieldScript.isTaken = false;}
            }
            else if(blockDir == "left")
            {
                if(FieldScript.TableNumberX == TableNumberX+1 && FieldScript.TableNumberY == TableNumberY){FieldScript.isTaken = false;}
            }
            else if(blockDir == "up")
            {
                if(FieldScript.TableNumberX == TableNumberX && FieldScript.TableNumberY == TableNumberY-1){FieldScript.isTaken = false;}
            }
            else if(blockDir == "down")
            {
                if(FieldScript.TableNumberX == TableNumberX && FieldScript.TableNumberY == TableNumberY+1){FieldScript.isTaken = false;}    
            }
        }
    }

        public void TakeNewField(int x, int y)
    {
		foreach (GameObject nextField in fields)
        {
            FieldScript = nextField.gameObject.GetComponent<FieldScript>();
            if(FieldScript.TableNumberX == TableNumberX && FieldScript.TableNumberY == TableNumberY){FieldScript.isTaken = true;}
        }
    }

	private void OnDestroy() 
	{
		if(willBeLevelingUp == true) 
		{
			SpawnBlock.BlockLevelUp(TableNumberX, TableNumberY, value, OwnerID);
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
	}

}
