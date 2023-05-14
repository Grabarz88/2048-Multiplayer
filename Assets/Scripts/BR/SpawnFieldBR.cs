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
    public int fieldIndicator = 0;

    public List<GameObject> fields = new List<GameObject>();
    public List<GameObject> fieldsBackground = new List<GameObject>();
    public List<GameObject> P1Fields = new List<GameObject>();
    public List<GameObject> P2Fields = new List<GameObject>();
    public List<GameObject> P3Fields = new List<GameObject>();
    public List<GameObject> P4Fields = new List<GameObject>();

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
                        P1Fields.Add(aField);
                        aField.GetComponent<FieldScript>().TableNumberSetter(x, y);
                        aField.GetComponent<FieldScript>().PositionSetter(x*32-100, y*32+100);
                        x++; 
                    }
                else
                    {
                        GameObject aField = Instantiate(field);
                        GameObject aFieldBackground = Instantiate(fieldBackground, new Vector3(x*32-100, y*32+100, 1), Quaternion.identity);
                        fields.Add(aField);
                        fieldsBackground.Add(aFieldBackground);
                        P1Fields.Add(aField);
                        aField.GetComponent<FieldScript>().TableNumberSetter(x, y);
                        aField.GetComponent<FieldScript>().PositionSetter(x*32-100, y*32+100);
                        x++; 
                    }
                }
            x = 0;
            y++;
            }
            fieldIndicator = 6;
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
                        P2Fields.Add(aField);
                        aField.GetComponent<FieldScript>().TableNumberSetter(x+fieldIndicator, y+fieldIndicator);
                        aField.GetComponent<FieldScript>().PositionSetter(x*32+100, y*32+100);
                        x++; 
                    }
                else
                    {
                        GameObject aField = Instantiate(field);
                        GameObject aFieldBackground = Instantiate(fieldBackground, new Vector3(x*32+100, y*32+100, 1), Quaternion.identity);
                        fields.Add(aField);
                        fieldsBackground.Add(aFieldBackground);
                        P2Fields.Add(aField);
                        aField.GetComponent<FieldScript>().TableNumberSetter(x+fieldIndicator, y+fieldIndicator);
                        aField.GetComponent<FieldScript>().PositionSetter(x*32+100, y*32+100);
                        x++; 
                    }
                }
            x = 0;
            y++;
            }
            fieldIndicator = fieldIndicator + 6;
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
                        P3Fields.Add(aField);
                        aField.GetComponent<FieldScript>().TableNumberSetter(x+fieldIndicator, y+fieldIndicator);
                        aField.GetComponent<FieldScript>().PositionSetter(x*32-100, y*32-100);
                        x++; 
                    }
                else
                    {
                        GameObject aField = Instantiate(field);
                        GameObject aFieldBackground = Instantiate(fieldBackground, new Vector3(x*32-100, y*32-100, 1), Quaternion.identity);
                        fields.Add(aField);
                        fieldsBackground.Add(aFieldBackground);
                        P3Fields.Add(aField);
                        aField.GetComponent<FieldScript>().TableNumberSetter(x+fieldIndicator, y+fieldIndicator);
                        aField.GetComponent<FieldScript>().PositionSetter(x*32-100, y*32-100);
                        x++; 
                    }
                }
            x = 0;
            y++;
            }
            fieldIndicator = fieldIndicator + 6;
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
                        P4Fields.Add(aField);
                        aField.GetComponent<FieldScript>().TableNumberSetter(x+fieldIndicator, y+fieldIndicator);
                        aField.GetComponent<FieldScript>().PositionSetter(x*32+100, y*32-100);
                        x++; 
                    }
                else
                    {
                        GameObject aField = Instantiate(field);
                        GameObject aFieldBackground = Instantiate(fieldBackground, new Vector3(x*32+100, y*32-100, 1), Quaternion.identity);
                        fields.Add(aField);
                        fieldsBackground.Add(aFieldBackground);
                        P4Fields.Add(aField);
                        aField.GetComponent<FieldScript>().TableNumberSetter(x+fieldIndicator, y+fieldIndicator);
                        aField.GetComponent<FieldScript>().PositionSetter(x*32+100, y*32-100);
                        x++; 
                    }
                }
            x = 0;
            y++;
            }
        }
    }

    public void initiateBRFaze()
    {
        foreach (GameObject field in fields)
        {
            Destroy(field);
        }
        
        foreach (GameObject fieldBackground in fieldsBackground)
        {
            Destroy(fieldBackground);
        }
        fields.TrimExcess();
        
        int y = 0;
        int x = 0;
        while (y < 22)
        {
            while (x < 22)
            {
                if (x == 0 || x == 21 || y == 0 || y == 21) 
                {
                    GameObject aField = Instantiate(wall);
                    fields.Add(aField);
                    P1Fields.Add(aField);
                    aField.GetComponent<FieldScript>().TableNumberSetter(x, y);
                    aField.GetComponent<FieldScript>().PositionSetter(x*32-224, y*32-224);
                    x++; 
                }
            else
                {
                    GameObject aField = Instantiate(field);
                    GameObject aFieldBackground = Instantiate(fieldBackground, new Vector3(x*32-224, y*32-224, 1), Quaternion.identity);
                    fields.Add(aField);
                    fieldsBackground.Add(aFieldBackground);
                    P1Fields.Add(aField);
                    aField.GetComponent<FieldScript>().TableNumberSetter(x, y);
                    aField.GetComponent<FieldScript>().PositionSetter(x*32-224, y*32-224);
                    x++; 
                }
            }
            x = 0;
            y++;
        }
    }
}

