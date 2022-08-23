using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnField : MonoBehaviour
{
    //Ten skrypt spawnuje pola po których będa się poruszały kafelki.
    [SerializeField] GameObject field;
    
    
    public List<GameObject> fields = new List<GameObject>(); // Lista pól

    
    

    


    void Start() //Tutaj spawnowane są pola. Od razu po pojawieniu nadaje się im pozycję fizyczną i pozycję w tabeli pól.
    {
        GameObject field1 = Instantiate(field);
        fields.Add(field1);
        field1.GetComponent<FieldScript>().PositionSetter(-8, 0);
        field1.GetComponent<FieldScript>().TableNumberSetter(0, 0);

        GameObject field2 = Instantiate(field);
        fields.Add(field2);
        field2.GetComponent<FieldScript>().PositionSetter(0, 0);
        field2.GetComponent<FieldScript>().TableNumberSetter(1, 0);
        GameObject field3 = Instantiate(field);
        fields.Add(field3);
        field3.GetComponent<FieldScript>().PositionSetter(8, 0);
        field3.GetComponent<FieldScript>().TableNumberSetter(2, 0);

    }

    
}
