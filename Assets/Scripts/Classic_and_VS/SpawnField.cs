using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnField : MonoBehaviour
{
    //Ten skrypt spawnuje pola po których będa się poruszały kafelki.
    [SerializeField] GameObject field;
    [SerializeField] GameObject wall;
    [SerializeField] GameObject fieldBackground;
    [SerializeField] public int PodajSzerokoscPlanszy;
    [SerializeField] public int PodajWysokoscPlanszy;
    
    
    public List<GameObject> fields = new List<GameObject>(); // Lista pól

    GameObject CustomSetter;
    

    


    void Start() //Tutaj spawnowane są pola. Od razu po pojawieniu nadaje się im pozycję fizyczną i pozycję w tabeli pól.
    {
       if(GameObject.Find("CustomSetter"))
       {
        CustomSetter = GameObject.Find("CustomSetter");
        PodajSzerokoscPlanszy = CustomSetter.GetComponent<CustomSetterScript>().X;
        PodajWysokoscPlanszy = CustomSetter.GetComponent<CustomSetterScript>().Y;
       }
       else
       {
        PodajSzerokoscPlanszy = 6;
        PodajWysokoscPlanszy = 6;
       }
       int y = 0;
       int x = 0;
       while (y < PodajWysokoscPlanszy)
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
                GameObject aFieldBackground = Instantiate(fieldBackground, new Vector3(x*32, y*32, 1), Quaternion.identity);
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


    private void OnDestroy() {
        if(CustomSetter != null)
        {
            GameObject.Destroy(CustomSetter);
        }
    }

    
}