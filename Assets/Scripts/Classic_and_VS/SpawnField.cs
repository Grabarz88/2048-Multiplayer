using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is used to spawn fields for both Classic and Custom modes. It also starts procedure of sending score to Highscore Leaderboard.
public class SpawnField : MonoBehaviour
{
    //Those variables are used to spawn fields.
    [SerializeField] GameObject field;
    [SerializeField] GameObject wall;
    [SerializeField] GameObject fieldBackground;
    [SerializeField] public int PodajSzerokoscPlanszy;
    [SerializeField] public int PodajWysokoscPlanszy;
    public List<GameObject> fields = new List<GameObject>();
    GameObject CustomSetter;

    //This variable is used to comunicate with object managing HighScore Leaderboard in online services.
    [SerializeField] GameObject HighScoreManager; 
    

    void Start() //Fields are getting placed here. 
    {
       //We check for presence of object coming from CustomSettings scene.
       if(GameObject.Find("CustomSetter"))
       {
        //If it is present, we get the player's desired board size.
        CustomSetter = GameObject.Find("CustomSetter");
        PodajSzerokoscPlanszy = CustomSetter.GetComponent<CustomSetterScript>().X;
        PodajWysokoscPlanszy = CustomSetter.GetComponent<CustomSetterScript>().Y;
       }
       else
       {
        //In lack of Custom Setter, the script will generate board of classic size.
        PodajSzerokoscPlanszy = 6;
        PodajWysokoscPlanszy = 6;
        //Playing classic mode allows us to send our score to HighScore Leaderboard in online services.
        HighScoreManager.SetActive(true);
       }

       int y = 0;
       int x = 0;
       //This loop will spawn fields, assign them their positions and create individual row and column ID.
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
           //We need to make sure our set preferences will be deleted while exiting from Custom mode.
            GameObject.Destroy(CustomSetter);
        }
    }

    
}
