using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlacement : MonoBehaviour
{
    
    [SerializeField] GameObject FieldSpawner;
    [SerializeField] SpawnField SpawnField;
    [SerializeField] GameObject CustomSetter;



    void Start()
    {
        if(GameObject.Find("CustomSetter"))
        {
            CustomSetter = GameObject.Find("CustomSetter");
            transform.position = new Vector3 (CustomSetter.GetComponent<CustomSetterScript>().X*16, CustomSetter.GetComponent<CustomSetterScript>().Y*16, -10);
            if(CustomSetter.GetComponent<CustomSetterScript>().X >= 8 || CustomSetter.GetComponent<CustomSetterScript>().Y >= 8)
            {
                if(CustomSetter.GetComponent<CustomSetterScript>().X >= CustomSetter.GetComponent<CustomSetterScript>().Y)
                {
                    GetComponent<Camera>().orthographicSize = CustomSetter.GetComponent<CustomSetterScript>().X * 25f;
                }
                else
                {
                    GetComponent<Camera>().orthographicSize = CustomSetter.GetComponent<CustomSetterScript>().Y * 25f;
                }
            }
        }
        else
        {
            SpawnField = FieldSpawner.GetComponent<SpawnField>();
            transform.position = new Vector3 (SpawnField.PodajSzerokoscPlanszy*16, SpawnField.PodajWysokoscPlanszy*16, -10);
        }
        
        
                
    }

  
}
