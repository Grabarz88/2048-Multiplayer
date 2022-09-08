using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldBackgroundScript : MonoBehaviour
{
    
    [SerializeField] GameObject FieldSpawner;
    [SerializeField] SpawnField SpawnField;



    void Start()
    {   
        SpawnField = FieldSpawner.GetComponent<SpawnField>();
        transform.position = new Vector3 ((SpawnField.PodajSzerokoscPlanszy-1)*17, (SpawnField.PodajWysokoscPlanszy-1)*13, 0);  
        transform.localScale = new Vector3 (SpawnField.PodajSzerokoscPlanszy*8f, SpawnField.PodajSzerokoscPlanszy*8f, 1);
    }


}
