using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehaviourScript : MonoBehaviour
{
    [SerializeField] public int TableNumberX;
    [SerializeField] public int TableNumberY;

    [SerializeField] public float positionX;
    [SerializeField] public float positionY;


    [SerializeField] GameObject FieldSpawner;
    SpawnField SpawnField;


    public List<GameObject> fields;
    FieldScript FieldScript;

    
    
    void Start()
    {



    }
    
    public void AfterSpawn(int x, int y)
    {
        FieldSpawner = GameObject.Find("FieldSpawner");
        SpawnField = FieldSpawner.GetComponent<SpawnField>();
        fields = SpawnField.fields;
        Debug.Log("AfterSpawn");
        TableNumberX = x;
        TableNumberY = y;
        foreach (GameObject field in fields)
        {
            Debug.Log("foreach");
            FieldScript = field.gameObject.GetComponent<FieldScript>();
            if(FieldScript.TableNumberX == TableNumberX && FieldScript.TableNumberY == TableNumberY)
            {
                Debug.Log("znalazłem");
                positionX = FieldScript.positionX;
                positionY = FieldScript.positionY;
                FieldScript.isTaken = true; //Póki co, jest to potrzebne do testów. W dalszym etapie produkcji, pola chyba muszą same sprawdzać czy są zajęte
            }
        }
        this.gameObject.transform.position = new Vector2(positionX, positionY);
    }



}
