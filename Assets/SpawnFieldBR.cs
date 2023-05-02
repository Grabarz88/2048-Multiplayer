using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFieldBR : MonoBehaviour
{
    [SerializeField] GameObject field;
    [SerializeField] GameObject wall;
    [SerializeField] GameObject fieldBackground;
    [SerializeField] public int PodajSzerokoscPlanszy;
    [SerializeField] public int PodajWysokoscPlanszy;

    public List<GameObject> fields = new List<GameObject>();

    GameObject ObjectToRememberColors;
    ScriptToRememberBRColors ScriptToRememberBRColors;


    void Start()
    {
        ObjectToRememberColors = GameObject.Find("ObjectToRememberColors");
        ScriptToRememberBRColors = ObjectToRememberColors.GetComponent<ScriptToRememberBRColors>();

        if(ScriptToRememberBRColors.isPlayer1Playing == true)
        {
            int y = 0;
            int x = 0;
            while (y < 6)
            {
                while (x < 6)
                {
                    if (x == 0 || x == 5 || y == 0 || y == 5) 
                    {
                        GameObject aField = Instantiate(wall);
                        fields.Add(aField);
                        aField.GetComponent<FieldScript>().TableNumberSetter(x, y);
                        aField.GetComponent<FieldScript>().PositionSetter(x*32-100, y*32+100);
                        x++; 
                    }
                else
                    {
                        GameObject aField = Instantiate(field);
                        GameObject aFieldBackground = Instantiate(fieldBackground, new Vector3(x*32-100, y*32+100, 1), Quaternion.identity);
                        fields.Add(aField);
                        aField.GetComponent<FieldScript>().TableNumberSetter(x, y);
                        aField.GetComponent<FieldScript>().PositionSetter(x*32-100, y*32+100);
                        x++; 
                    }
                }
            x = 0;
            y++;
            }
        }

        if(ScriptToRememberBRColors.isPlayer2Playing == true)
        {
            int y = 0;
            int x = 0;
            while (y < 6)
            {
                while (x < 6)
                {
                    if (x == 0 || x == 5 || y == 0 || y == 5) 
                    {
                        GameObject aField = Instantiate(wall);
                        fields.Add(aField);
                        aField.GetComponent<FieldScript>().TableNumberSetter(x+36, y+36);
                        aField.GetComponent<FieldScript>().PositionSetter(x*32+100, y*32+100);
                        x++; 
                    }
                else
                    {
                        GameObject aField = Instantiate(field);
                        GameObject aFieldBackground = Instantiate(fieldBackground, new Vector3(x*32+100, y*32+100, 1), Quaternion.identity);
                        fields.Add(aField);
                        aField.GetComponent<FieldScript>().TableNumberSetter(x+36, y+36);
                        aField.GetComponent<FieldScript>().PositionSetter(x*32+100, y*32+100);
                        x++; 
                    }
                }
            x = 0;
            y++;
            }
        }


        if(ScriptToRememberBRColors.isPlayer3Playing == true)
        {
            int y = 0;
            int x = 0;
            while (y < 6)
            {
                while (x < 6)
                {
                    if (x == 0 || x == 5 || y == 0 || y == 5) 
                    {
                        GameObject aField = Instantiate(wall);
                        fields.Add(aField);
                        aField.GetComponent<FieldScript>().TableNumberSetter(x+72, y+72);
                        aField.GetComponent<FieldScript>().PositionSetter(x*32-100, y*32-100);
                        x++; 
                    }
                else
                    {
                        GameObject aField = Instantiate(field);
                        GameObject aFieldBackground = Instantiate(fieldBackground, new Vector3(x*32-100, y*32-100, 1), Quaternion.identity);
                        fields.Add(aField);
                        aField.GetComponent<FieldScript>().TableNumberSetter(x+72, y+72);
                        aField.GetComponent<FieldScript>().PositionSetter(x*32-100, y*32-100);
                        x++; 
                    }
                }
            x = 0;
            y++;
            }
        }

        if(ScriptToRememberBRColors.isPlayer4Playing == true)
        {
            int y = 0;
            int x = 0;
            while (y < 6)
            {
                while (x < 6)
                {
                    if (x == 0 || x == 5 || y == 0 || y == 5) 
                    {
                        GameObject aField = Instantiate(wall);
                        fields.Add(aField);
                        aField.GetComponent<FieldScript>().TableNumberSetter(x+104, y+104);
                        aField.GetComponent<FieldScript>().PositionSetter(x*32+100, y*32-100);
                        x++; 
                    }
                else
                    {
                        GameObject aField = Instantiate(field);
                        GameObject aFieldBackground = Instantiate(fieldBackground, new Vector3(x*32+100, y*32-100, 1), Quaternion.identity);
                        fields.Add(aField);
                        aField.GetComponent<FieldScript>().TableNumberSetter(x+104, y+104);
                        aField.GetComponent<FieldScript>().PositionSetter(x*32+100, y*32-100);
                        x++; 
                    }
                }
            x = 0;
            y++;
            }
        }
    }
}

