using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlacement : MonoBehaviour
{
    
    [SerializeField] GameObject FieldSpawner;
    [SerializeField] SpawnField SpawnField;



    void Start()
    {
        SpawnField = FieldSpawner.GetComponent<SpawnField>();
        transform.position = new Vector3 (SpawnField.PodajSzerokoscPlanszy*16, SpawnField.PodajWysokoscPlanszy*16, -10);        
    }

  
}
