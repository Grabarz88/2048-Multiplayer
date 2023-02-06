using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlockBehaviourScript : MonoBehaviour
{
    [SerializeField] public int TableNumberX;
    [SerializeField] public int TableNumberY;

    [SerializeField] public float positionX;
    [SerializeField] public float positionY;


    [SerializeField] GameObject FieldSpawner;
    [SerializeField] GameObject BlockSpawner;
    SpawnField SpawnField;
    SpawnBlock SpawnBlock;
    BlockBehaviourScript NextBlockBehaviourScript;

    public List<GameObject> fields;
    public List<GameObject> blocks;
    FieldScript FieldScript;

    [SerializeField] public int value;
	[SerializeField] public int reserveValue;
    public string dir;
    [SerializeField] public bool unmovable;
    [SerializeField] public bool moved = false; // This variable is used to ensure that blocks won't spawn during "vidmo move". That means, if we press the button in direction that won't make any blocks move.
	[SerializeField] public bool readyToMove = false;
	[SerializeField] public bool readyToBeDestroyed = false; 
	[SerializeField] public bool justLeveledUp = false;
	[SerializeField] public bool moveExecuting = false;
	[SerializeField] public bool finishedMove = false;
	public float pace = 1; // This variable is used to ensure, all blocks begin and finish move in the exactly same time, nevertheless the distance from start to finish

	


    public GameObject pauseButton;
	public GameObject exitButton;
    public bool isPauseActive = false;

	Vector2 targetFieldPosition;
    
    void Start()
    {
    FieldSpawner = GameObject.Find("FieldSpawner");
    SpawnField = FieldSpawner.GetComponent<SpawnField>();
	BlockSpawner = GameObject.Find("BlockSpawner"); 
	SpawnBlock = BlockSpawner.GetComponent<SpawnBlock>();
    fields = SpawnField.fields;
    dir = "empty";  
	pauseButton = GameObject.Find("Restart");  
	exitButton = GameObject.Find("Back");  
	

	if (this.gameObject.name == "block0")
	{
		int randomX;
		int randomY;
		randomX = SpawnBlock.randomX;
		randomY = SpawnBlock.randomY;
		AfterSpawn(randomX, randomY);

	}
    }



    void Update()
    {
        // Debug.Log("BBH Update");
		//Zaczynamy od warunku sprawdzającego czy gra jest w pauzie. Btw, to się da zoptymalizować
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
			//Zbieranie kierunku w którym mają się poruszać bloki
	        if(Input.GetButtonDown("MoveRight")){dir = "right";}
	        if(Input.GetButtonDown("MoveLeft")){dir = "left";}
	        if(Input.GetButtonDown("MoveUp")){dir = "up";}
	        if(Input.GetButtonDown("MoveDown")){dir = "down";}
	
	        if(unmovable == false && justLeveledUp == false)//Upewniamy się, że bloki jeszcze nie znalazły jeszcze miejsca, w którym muszą się zatrzymać
	        {
	            // Debug.Log("BBH unmovable false check");
				//Wskazujemy, jakie pole będziemy teraz sprawdzali.
				if(dir == "right"){TableNumberX++;} 
	            else if(dir == "left"){TableNumberX--;}
	            else if(dir == "up"){TableNumberY++;}
	            else if(dir == "down"){TableNumberY--;}
	
	            foreach(GameObject field in fields) //Będziemy sprawdzali wszystkie pola tak długo aż znajdziemy odpowiednie.
	            {
	                // Debug.Log("BBH foreach field start");

					FieldScript = field.GetComponent<FieldScript>(); //Żeby to sprawdzić wykorzystujemy skrypt FieldScript, który przechowuje dane o wartościach pól
	                if(FieldScript.TableNumberX == TableNumberX && FieldScript.TableNumberY == TableNumberY) //Dalszą część warunku wykonujemy, tylko jeśli pole ma poszukiwane przez nas wartości.
	                {
	                    // Debug.Log("BBH found field to chceck");
						if(FieldScript.isWall == true) // Sprawdzamy, czy sprawdzane pole jest ścianą.
	                    {
	                        // Debug.Log("BBH found field is wall");
							//Jeśli pole jest ścianą, to wycofujemy zmianę wartości i deklarujemy, że nie blok nie będzie mógł się dalej poruszyć.
							if(dir == "right"){TableNumberX--;}
	                        else if(dir == "left"){TableNumberX++;}
	                        else if(dir == "up"){TableNumberY--;}
	                        else if(dir == "down"){TableNumberY++;}
							unmovable = true;
							readyToMove = true; 
							// Debug.Log("Ściana");
							
	                    }
	                    else if(FieldScript.isTaken == true && dir != "empty") // Jeżeli pole nie jest ścianą, to sprawdzamy czy jest zajęte przez jakiś blok. Dla pewności sprawdzamy też, czy nie zostało sprawdzone pole na którym aktualnie stoi blok sprawdzający.
	                    {
	                        //Ponieważ pole jest zajęte, to musimy znaleźć tablicę bloków. Pozwoli nam to na wyszukanie bloku w którym weszliśmy w kolizję.
							// Debug.Log("BBH found field is taken and dir != empty");
	                        blocks = SpawnBlock.blocks;
	                        try //Ze względu na okazjonalne zmiany tablicy bloków, trzeba być gotowym na ignorowanie błędów
	                        {
	                        foreach(GameObject block in blocks) //Będziemy sprawdzali każdy blok, tak długo aż znajdziemy ten z którym nastąpiła kolizja.
	                        {
	                            // Debug.Log("BBH after found field foreach block start");
								if(block != null) // Sprawdzamy tylko nie puste bloki.
	                            {
	                                NextBlockBehaviourScript = block.GetComponent<BlockBehaviourScript>(); //Odpytanie odbędzie się poprzez skrypt BlovkBehaviourScript bloku z którym doszło do kolizji
	                                // Sprawdzamy, czy pozycja znalezionego bloku odpowiada naszej szukanej pozycji, czy zakończył już ruch deklaracją unmovable oraz czy jego wartość odpowiada wartości naszego bloku.
									if(TableNumberX == NextBlockBehaviourScript.TableNumberX && TableNumberY == NextBlockBehaviourScript.TableNumberY && block != this.gameObject && NextBlockBehaviourScript.unmovable == true && NextBlockBehaviourScript.value == value && NextBlockBehaviourScript.readyToBeDestroyed == false)
	                                {
										// Debug.Log("BBH same value collision");
										//Po znalezieniu takiego bloku, oznaczamy się jako gotowe do ruchu, gotowe do zniszczenia, zwalniamy pole na którym jesteśmy i informujemy uderzony blok o fuzji.
										readyToMove = true;
										readyToBeDestroyed = true;
										NextBlockBehaviourScript.value = -1; // W ten sposób upewnimy się, że inne bloki się z nim nie połączą po zderzeniu i że blok będzie wiedział, że ma tu postawić wyższy blok w OnDestroy()
										NextBlockBehaviourScript.readyToBeDestroyed = true;
										ReleaseOldField(TableNumberX, TableNumberY, dir); //Musimy znaleźć stary kafelek i zadeklarować, że nie jest już zajęty.
										moved = true;
							  
										
	                                }
	                                else if(TableNumberX == NextBlockBehaviourScript.TableNumberX && TableNumberY == NextBlockBehaviourScript.TableNumberY && block != this.gameObject && NextBlockBehaviourScript.unmovable == false)
	                                {
	                                    // Debug.Log("BBH collision with block who didn't finished moving");
										//Przypadek w którym nastąpiło zderzenie, ale uderzony kafelek może się jeszcze poruszać
	                                    if(dir == "right"){TableNumberX--;} //Wycofujemy zwiększenie wartości. Następna iteracja Update na powrót ją zwiększy i znowu sprawdzi, czy następne pole jest już wolne
	                                    else if(dir == "left"){TableNumberX++;}
	                                    else if(dir == "up"){TableNumberY--;}
	                                    else if(dir == "down"){TableNumberY++;} 
										
	                                }
	                                else if(TableNumberX == NextBlockBehaviourScript.TableNumberX && TableNumberY == NextBlockBehaviourScript.TableNumberY && block != this.gameObject && NextBlockBehaviourScript.unmovable == true && NextBlockBehaviourScript.value != reserveValue)
	                                {
	                                    // Debug.Log("BBH collision of different blocks");
										//Przypadek w którym nastąpiło zderzenie kafelków o różnych wartościach
	                                    if(dir == "right"){TableNumberX--;}
	                                    else if(dir == "left"){TableNumberX++;}
	                                    else if(dir == "up"){TableNumberY--;}
	                                    else if(dir == "down"){TableNumberY++;}
										readyToMove = true;
	                                    unmovable = true;
										
										
	                                }
	                            }
	                        }
	                        }
	                        catch{}
	                    }
	                    else if(FieldScript.isTaken == false) //Jeśli pole nie jest zajęte to zwyczajnie na nie wchodzimy
	                    {
	                        // Debug.Log("BBH field is free to enter");
							moved = true;

							TakeNewField(TableNumberX, TableNumberY); //Deklarujemy że jesteśmy na tym kafelku.
	                        ReleaseOldField(TableNumberX, TableNumberY, dir); //Musimy znaleźć stary kafelek i zadeklarować, że nie jest już zajęty.
							
	                    }
	                }
	            }
	        }
			else if (unmovable == true) //Jeżeli zadeklarowaliśmy, że nie możemy się ruszyć, to sprawdzamy, na jakiej pozycji mamy się pojawić
			{
				// Debug.Log("BBH unmovable true moving ");
				// Debug.Log("Nie mogę się dalej ruszyć");
				foreach(GameObject field in fields)
	            {
	                Debug.Log("BBH unmovable true moing foreach");
					FieldScript = field.GetComponent<FieldScript>();
	                if(FieldScript.TableNumberX == TableNumberX && FieldScript.TableNumberY == TableNumberY)
	                {
						Debug.Log("BBH setting target");
						targetFieldPosition = new Vector2(FieldScript.positionX, FieldScript.positionY); //tutaj zapamiętujemy pozycję do której mamy dojść
						
							
						
					}
				}	
			}

			if(unmovable == true && moveExecuting == true) //Jeśli SpawnBlock da komendę na wykonanie ruchu, to to zrobimy.
			{
				// Debug.Log("Krok wykonany");
				
				// Debug.Log("BBH moving line");
				transform.position = Vector2.MoveTowards(transform.position, targetFieldPosition, (pace * 0.2f)); //Tutaj blok jest przesuwany

				//Sprawdzamy czy blok jest wystarczjąco blisko swojej pozyji docelowej
				if(Math.Abs(transform.position.x - targetFieldPosition.x) < 1 && Math.Abs(transform.position.y - targetFieldPosition.y) < 1)
				{
					// Debug.Log("BBH came to position");
					moveExecuting = false; //Jeżeli blok jest już wystarczjąco blisko, to wstrzymujemy dalszy ruch.
					finishedMove = true;
					
					// Debug.Log("moveExecuting = false");
				}
			}
			
        }

		setPace(); // We are evoking function to set pace variable	

    }
   

    public void executeMove() //SpawnBlock wywołuje kiedy możemy się ruszyć, tak aby wszystkie bloki zrobiły to na raz
	{
		// Debug.Log("BBH executeMove");
		moveExecuting = true;
		// Debug.Log("moveExecuting = true");
	}

	public void executeLevelUp() //SpawnBlock wywołuje kiedy mamy się zniszczyć, tak aby wszystkie bloki zrobiły to na raz
	{
		// Debug.Log("BBH executeLevelUp");
		if(readyToBeDestroyed == true)
		{
			Destroy(gameObject);
		}
	}
	
	public void setPace() // Setting pace
	{
		// Debug.Log("BBH setPace");
		if(moveExecuting == false)
		{
			if (Math.Abs(targetFieldPosition.x - transform.position.x) > 1.5f)
				{
					pace = Math.Abs(transform.position.x - targetFieldPosition.x);
				}
			if (Math.Abs(targetFieldPosition.y - transform.position.y) > 1.5f)
				{
					pace = Math.Abs(transform.position.y - targetFieldPosition.y);
				}
		}		
	}
	public void AfterSpawn(int x, int y)
    {
		// Debug.Log("BBH AfterSpawn");
		FieldSpawner = GameObject.Find("FieldSpawner"); //Niepotrzebne. Robimy to wcześniej
        SpawnField = FieldSpawner.GetComponent<SpawnField>(); //Niepotrzebne. Robimy to wcześniej
        fields = SpawnField.fields; //Niepotrzebne. Robimy to wcześniej
        TableNumberX = x;
        TableNumberY = y;
        foreach (GameObject field in fields)
        {
            FieldScript = field.gameObject.GetComponent<FieldScript>();
            if(FieldScript.TableNumberX == TableNumberX && FieldScript.TableNumberY == TableNumberY)
            {
                positionX = FieldScript.positionX;
                positionY = FieldScript.positionY;
                FieldScript.isTaken = true; //Póki co, jest to potrzebne do testów. W dalszym etapie produkcji, pola chyba muszą same sprawdzać czy są zajęte
            }
        }
        this.gameObject.transform.position = new Vector2(positionX, positionY);
        TakeNewField(TableNumberX, TableNumberY); //Wykorzystanie tego tutaj jest bez sensu, skoro wyżej w tej funkcji ręcznie ustawiamy zajętość.
		moved = false;
	}

    public void ReleaseOldField(int x, int y, string blockDir) //Ten x i y są niewykorzystane. To na pewno będzie działało?
    {
        // Debug.Log("BBH ReleaseOldField");
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
        // Debug.Log("BBH TakeNewField");
		foreach (GameObject nextField in fields)
        {
            FieldScript = nextField.gameObject.GetComponent<FieldScript>();
            if(FieldScript.TableNumberX == TableNumberX && FieldScript.TableNumberY == TableNumberY){FieldScript.isTaken = true;}
        }
    }

	private void OnDestroy() 
	{
		// Debug.Log("BBH OnDestroy");
		foreach (GameObject thisFieldToRelease in fields)
		{
			if(thisFieldToRelease != null) // Ten warunek jest zabezpieczeniem przed pojawianiem się błędów przy zamykaniu gry
			{
				FieldScript = thisFieldToRelease.gameObject.GetComponent<FieldScript>();
			}
			if(FieldScript.TableNumberX == TableNumberX && FieldScript.TableNumberY == TableNumberY)
			{
				FieldScript.isTaken = false;
			}
		}

		if(value == -1)
		{
			SpawnBlock.BlockLevelUp(TableNumberX, TableNumberY, reserveValue);
		}
		SpawnBlock.blocks.Remove(gameObject);	
	}

}
