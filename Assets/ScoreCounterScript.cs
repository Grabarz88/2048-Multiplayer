using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounterScript : MonoBehaviour
{
    
    [SerializeField] int score;
    [SerializeField] Text textScore;
    [SerializeField] GameObject ScorePanel;
    public RectTransform ScorePanelRectTransform;
    bool grownth1 = false;
    bool grownth2 = false;
    bool grownth3 = false;
    bool grownth4 = false;
    bool grownth5 = false;
    bool grownth6 = false;
    bool grownth7 = false;
    bool grownth8 = false;

     void Start()
    {
      score = 0;
      ScorePanelRectTransform = ScorePanel.GetComponent<RectTransform>();
    }

    


    public void AddPoints(int value)
    {
        score = score + value;
  
        textScore.text = score.ToString();
        
        if(score > 999 && grownth1 == false)
        {
          ScorePanelRectTransform.sizeDelta = new Vector2(ScorePanelRectTransform.sizeDelta.x + 20, ScorePanelRectTransform.sizeDelta.y);
          ScorePanelRectTransform.position =  new Vector2(ScorePanelRectTransform.position.x + 20, ScorePanelRectTransform.position.y);
          grownth1 = true;
        }

        if(score > 9999 && grownth2 == false)
        {
          ScorePanelRectTransform.sizeDelta = new Vector2(ScorePanelRectTransform.sizeDelta.x + 20, ScorePanelRectTransform.sizeDelta.y);
          ScorePanelRectTransform.position =  new Vector2(ScorePanelRectTransform.position.x + 20, ScorePanelRectTransform.position.y);
          grownth2 = true;
        }

        if(score > 99999 && grownth3 == false)
        {
          ScorePanelRectTransform.sizeDelta = new Vector2(ScorePanelRectTransform.sizeDelta.x + 20, ScorePanelRectTransform.sizeDelta.y);
          ScorePanelRectTransform.position =  new Vector2(ScorePanelRectTransform.position.x + 20, ScorePanelRectTransform.position.y);
          grownth3 = true;
        }

        if(score > 99999 && grownth4 == false)
        {
          ScorePanelRectTransform.sizeDelta = new Vector2(ScorePanelRectTransform.sizeDelta.x + 20, ScorePanelRectTransform.sizeDelta.y);
          ScorePanelRectTransform.position =  new Vector2(ScorePanelRectTransform.position.x + 20, ScorePanelRectTransform.position.y);
          grownth4 = true;
        }

        if(score > 999999 && grownth5 == false)
        {
          ScorePanelRectTransform.sizeDelta = new Vector2(ScorePanelRectTransform.sizeDelta.x + 20, ScorePanelRectTransform.sizeDelta.y);
          ScorePanelRectTransform.position =  new Vector2(ScorePanelRectTransform.position.x + 20, ScorePanelRectTransform.position.y);
          grownth5 = true;
        }

        if(score > 9999999 && grownth6 == false)
        {
          ScorePanelRectTransform.sizeDelta = new Vector2(ScorePanelRectTransform.sizeDelta.x + 20, ScorePanelRectTransform.sizeDelta.y);
          ScorePanelRectTransform.position =  new Vector2(ScorePanelRectTransform.position.x + 20, ScorePanelRectTransform.position.y);
          grownth6 = true;
        }

        if(score > 99999999 && grownth7 == false)
        {
          ScorePanelRectTransform.sizeDelta = new Vector2(ScorePanelRectTransform.sizeDelta.x + 20, ScorePanelRectTransform.sizeDelta.y);
          ScorePanelRectTransform.position =  new Vector2(ScorePanelRectTransform.position.x + 20, ScorePanelRectTransform.position.y);
          grownth7 = true;
        }

        if(score > 999999999 && grownth8 == false)
        {
          ScorePanelRectTransform.sizeDelta = new Vector2(ScorePanelRectTransform.sizeDelta.x + 20, ScorePanelRectTransform.sizeDelta.y);
          ScorePanelRectTransform.position =  new Vector2(ScorePanelRectTransform.position.x + 20, ScorePanelRectTransform.position.y);
          grownth8 = true;
        } 
    }    

}
