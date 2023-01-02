using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounterScript : MonoBehaviour
{
    
    [SerializeField] int score;
    void Start()
    {
      score = 0;  
    }

    


    public void AddPoints(int value)
    {
        score = score + value;
        // Debug.Log(score);
    }

}
