using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnField : MonoBehaviour
{
    //Ten skrypt spawnuje pola po których będa się poruszały kafelki.
    [SerializeField] GameObject field;
    [SerializeField] GameObject wall;
    [SerializeField] public int PodajSzerokoscPlanszy;
    [SerializeField] public int PodajWysokoscPlanszy;
    
    
    public List<GameObject> fields = new List<GameObject>(); // Lista pól

    
    

    


    void Start() //Tutaj spawnowane są pola. Od razu po pojawieniu nadaje się im pozycję fizyczną i pozycję w tabeli pól.
    {
       int y = 0;
       int x = 0;
       while (y < PodajSzerokoscPlanszy)
       {
        while (x < PodajSzerokoscPlanszy)
        {
            if (x == 0 || x==PodajSzerokoscPlanszy-1 || y == 0 || y == PodajWysokoscPlanszy-1) 
                {
                GameObject aField = Instantiate(wall);
                fields.Add(aField);
                aField.GetComponent<FieldScript>().TableNumberSetter(x, y);
                aField.GetComponent<FieldScript>().PositionSetter(x*32, y*32);
                x++; 
                }
            else
                {
                GameObject aField = Instantiate(field);
                fields.Add(aField);
                aField.GetComponent<FieldScript>().TableNumberSetter(x, y);
                aField.GetComponent<FieldScript>().PositionSetter(x*32, y*32);
                x++; 
                }
 
        }
        x = 0;
        y++;
       }
       
       

    }

    
}
