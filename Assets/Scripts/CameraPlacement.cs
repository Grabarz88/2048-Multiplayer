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
            transform.position = new Vector3 ((CustomSetter.GetComponent<CustomSetterScript>().X-1)*16, (CustomSetter.GetComponent<CustomSetterScript>().Y-1)*16, -10);
            if(CustomSetter.GetComponent<CustomSetterScript>().X >= 6 || CustomSetter.GetComponent<CustomSetterScript>().Y >= 6)
            {
                if(CustomSetter.GetComponent<CustomSetterScript>().X >= CustomSetter.GetComponent<CustomSetterScript>().Y)
                {
                    GetComponent<Camera>().orthographicSize = (CustomSetter.GetComponent<CustomSetterScript>().X-2) * 25f;
                }
                else
                {
                    GetComponent<Camera>().orthographicSize = (CustomSetter.GetComponent<CustomSetterScript>().Y-2) * 25f;
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
