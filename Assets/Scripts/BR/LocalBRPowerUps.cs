using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalBRPowerUps : MonoBehaviour
{
    LocalBRSpawnBlock LocalBRSpawnBlock;
    [SerializeField] GameObject Pointer;
    void Start()
    {
        LocalBRSpawnBlock = GetComponent<LocalBRSpawnBlock>();
    }

    
    void Update()
    {
        
    }
}
