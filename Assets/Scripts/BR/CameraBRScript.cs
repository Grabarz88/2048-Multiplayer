using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBRScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MoveForBR()
    {
        GetComponent<Camera>().orthographicSize = 450;
    }
}
