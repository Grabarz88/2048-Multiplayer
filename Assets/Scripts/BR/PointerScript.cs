using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerScript : MonoBehaviour
{
    int scale;
    bool small = true;
    void Start()
    {
       gameObject.transform.localScale = new Vector3(10, 10, 1);

    }

    
    void Update()
    {
        if(small == true)
        {
            gameObject.transform.localScale += new Vector3(1, 1, 0);
            scale++;
        }
        
        if(small == false)
        {
            gameObject.transform.localScale -= new Vector3(1, 1, 0);
            scale--;
        }
        if(scale > 30)
        {
            small = false;
        }
        if (scale < 10)
        {
            small = true;
        }
        
    }
}
