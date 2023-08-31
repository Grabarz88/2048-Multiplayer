using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineNameUpdatePanel : MonoBehaviour
{
    
    public float time = 3;
    public Vector2 startPosition;
    

   void Start()
   {
    startPosition = transform.position;
   }
    void Update()
    {
        time -= Time.deltaTime;
        if (time >= 2)
        {
            transform.position += new Vector3(-4 * Time.deltaTime, 0, 0);
        }
        else if(time <= 1)
        {
            transform.position += new Vector3(4 * Time.deltaTime, 0, 0);
        }

        if(time <=0)
        {
            this.gameObject.SetActive(false);
            time = 3;
            transform.position = startPosition;
        }
    }
}
