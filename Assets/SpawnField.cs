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
       int y = 0;
       int x = 0;
       while (y < 6)
       {
        while (x < 6)
        {
            GameObject aField = Instantiate(field);
            fields.Add(aField);
            aField.GetComponent<FieldScript>().TableNumberSetter(x, y);
            aField.GetComponent<FieldScript>().PositionSetter(x*8, y*8);
            if (x == 0 || x==5 || y == 0 || y == 5) {aField.GetComponent<FieldScript>().isWall = true;}
            x++;  
        }
        x = 0;
        y++;
       }
       
       
    //    GameObject field0 = Instantiate(field);
    //     fields.Add(field0);
    //     field0.GetComponent<FieldScript>().PositionSetter(-24, 0);
    //     field0.GetComponent<FieldScript>().TableNumberSetter(-1, 0);


    //    GameObject field1 = Instantiate(field);
    //     fields.Add(field1);
    //     field1.GetComponent<FieldScript>().PositionSetter(-16, 0);
    //     field1.GetComponent<FieldScript>().TableNumberSetter(0, 0);

    //     GameObject field2 = Instantiate(field);
    //     fields.Add(field2);
    //     field2.GetComponent<FieldScript>().PositionSetter(-8, 0);
    //     field2.GetComponent<FieldScript>().TableNumberSetter(1, 0);

    //     GameObject field3 = Instantiate(field);
    //     fields.Add(field3);
    //     field3.GetComponent<FieldScript>().PositionSetter(0, 0);
    //     field3.GetComponent<FieldScript>().TableNumberSetter(2, 0);

    //     GameObject field4 = Instantiate(field);
    //     fields.Add(field4);
    //     field4.GetComponent<FieldScript>().PositionSetter(8, 0);
    //     field4.GetComponent<FieldScript>().TableNumberSetter(3, 0);

    //     GameObject field5 = Instantiate(field);
    //     fields.Add(field5);
    //     field5.GetComponent<FieldScript>().PositionSetter(16, 0);
    //     field5.GetComponent<FieldScript>().TableNumberSetter(4, 0);

    //     GameObject field6 = Instantiate(field);
    //     fields.Add(field6);
    //     field6.GetComponent<FieldScript>().PositionSetter(24, 0);
    //     field6.GetComponent<FieldScript>().TableNumberSetter(5, 0);


    //     GameObject field7 = Instantiate(field);
    //     fields.Add(field7);
    //     field7.GetComponent<FieldScript>().PositionSetter(-24, 8);
    //     field7.GetComponent<FieldScript>().TableNumberSetter(-1, 1);


    //    GameObject field8 = Instantiate(field);
    //     fields.Add(field8);
    //     field8.GetComponent<FieldScript>().PositionSetter(-16, 8);
    //     field8.GetComponent<FieldScript>().TableNumberSetter(0, 1);

    //     GameObject field9 = Instantiate(field);
    //     fields.Add(field9);
    //     field9.GetComponent<FieldScript>().PositionSetter(-8, 8);
    //     field9.GetComponent<FieldScript>().TableNumberSetter(1, 1);

    //     GameObject field10 = Instantiate(field);
    //     fields.Add(field10);
    //     field10.GetComponent<FieldScript>().PositionSetter(0, 8);
    //     field10.GetComponent<FieldScript>().TableNumberSetter(2, 1);

    //     GameObject field11 = Instantiate(field);
    //     fields.Add(field11);
    //     field11.GetComponent<FieldScript>().PositionSetter(8, 8);
    //     field11.GetComponent<FieldScript>().TableNumberSetter(3, 1);

    //     GameObject field12 = Instantiate(field);
    //     fields.Add(field12);
    //     field12.GetComponent<FieldScript>().PositionSetter(16, 8);
    //     field12.GetComponent<FieldScript>().TableNumberSetter(4, 1);

    //     GameObject field13 = Instantiate(field);
    //     fields.Add(field13);
    //     field13.GetComponent<FieldScript>().PositionSetter(24, 8);
    //     field13.GetComponent<FieldScript>().TableNumberSetter(5, 1);


    }

    
}
