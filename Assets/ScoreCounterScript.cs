using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.CoreModule;

public class ScoreCounterScript : MonoBehaviour
{
    
    [SerializeField] int score;
    [SerializeField] int highScore = 0;
    [SerializeField] Text textScore;
    [SerializeField] Text textHighScore;
    [SerializeField] GameObject ScorePanel;
    [SerializeField] GameObject highScorePanel;
    public RectTransform ScorePanelRectTransform;
    public RectTransform highScorePanelRectTransform;
    bool scoreGrowth1 = false;
    bool scoreGrowth2 = false;
    bool scoreGrowth3 = false;
    bool scoreGrowth4 = false;
    bool scoreGrowth5 = false;
    bool scoreGrowth6 = false;
    bool scoreGrowth7 = false;
    bool scoreGrowth8 = false;

    bool highScoreGrowth1 = false;
    bool highScoreGrowth2 = false;
    bool highScoreGrowth3 = false;
    bool highScoreGrowth4 = false;


     void Start()
    {
      // PlayerPrefs.DeleteAll();
      score = 0;
      ScorePanelRectTransform = ScorePanel.GetComponent<RectTransform>();
      highScorePanelRectTransform = highScorePanel.GetComponent<RectTransform>();
      highScore = PlayerPrefs.GetInt("HighScore");
      textHighScore.text = highScore.ToString();
      adjustHighScoreFieldSize();

    }

    


    public void AddPoints(int value)
    {
        score = score + value;

        if(score > highScore)
        {
          PlayerPrefs.SetInt("HighScore", score);
        }
  
        textScore.text = score.ToString();
        
        adjustScoreFieldSize();
    }   




  public void adjustScoreFieldSize()
  {
    if(score > 999 && scoreGrowth1 == false)
    {
      ScorePanelRectTransform.sizeDelta = new Vector2(ScorePanelRectTransform.sizeDelta.x + 20, ScorePanelRectTransform.sizeDelta.y);
      ScorePanelRectTransform.position =  new Vector2(ScorePanelRectTransform.position.x + 20, ScorePanelRectTransform.position.y);
      scoreGrowth1 = true;
    }

    if(score > 9999 && scoreGrowth2 == false)
    {
      ScorePanelRectTransform.sizeDelta = new Vector2(ScorePanelRectTransform.sizeDelta.x + 20, ScorePanelRectTransform.sizeDelta.y);
      ScorePanelRectTransform.position =  new Vector2(ScorePanelRectTransform.position.x + 20, ScorePanelRectTransform.position.y);
      scoreGrowth2 = true;
    }

    if(score > 99999 && scoreGrowth3 == false)
    {
      ScorePanelRectTransform.sizeDelta = new Vector2(ScorePanelRectTransform.sizeDelta.x + 20, ScorePanelRectTransform.sizeDelta.y);
      ScorePanelRectTransform.position =  new Vector2(ScorePanelRectTransform.position.x + 20, ScorePanelRectTransform.position.y);
      scoreGrowth3 = true;
    }

    if(score > 99999 && scoreGrowth4 == false)
    {
      ScorePanelRectTransform.sizeDelta = new Vector2(ScorePanelRectTransform.sizeDelta.x + 20, ScorePanelRectTransform.sizeDelta.y);
      ScorePanelRectTransform.position =  new Vector2(ScorePanelRectTransform.position.x + 20, ScorePanelRectTransform.position.y);
      scoreGrowth4 = true;
    }

    if(score > 999999 && scoreGrowth5 == false)
    {
      ScorePanelRectTransform.sizeDelta = new Vector2(ScorePanelRectTransform.sizeDelta.x + 20, ScorePanelRectTransform.sizeDelta.y);
      ScorePanelRectTransform.position =  new Vector2(ScorePanelRectTransform.position.x + 20, ScorePanelRectTransform.position.y);
      scoreGrowth5 = true;
    }

    if(score > 9999999 && scoreGrowth6 == false)
    {
      ScorePanelRectTransform.sizeDelta = new Vector2(ScorePanelRectTransform.sizeDelta.x + 20, ScorePanelRectTransform.sizeDelta.y);
      ScorePanelRectTransform.position =  new Vector2(ScorePanelRectTransform.position.x + 20, ScorePanelRectTransform.position.y);
      scoreGrowth6 = true;
    }

    if(score > 99999999 && scoreGrowth7 == false)
    {
      ScorePanelRectTransform.sizeDelta = new Vector2(ScorePanelRectTransform.sizeDelta.x + 20, ScorePanelRectTransform.sizeDelta.y);
      ScorePanelRectTransform.position =  new Vector2(ScorePanelRectTransform.position.x + 20, ScorePanelRectTransform.position.y);
      scoreGrowth7 = true;
    }

    if(score > 999999999 && scoreGrowth8 == false)
    {
      ScorePanelRectTransform.sizeDelta = new Vector2(ScorePanelRectTransform.sizeDelta.x + 20, ScorePanelRectTransform.sizeDelta.y);
      ScorePanelRectTransform.position =  new Vector2(ScorePanelRectTransform.position.x + 20, ScorePanelRectTransform.position.y);
      scoreGrowth8 = true;
    }     
  }


  public void adjustHighScoreFieldSize()
  {

    

    if(highScore > 999999 && highScoreGrowth1 == false)
    {
      highScorePanelRectTransform.sizeDelta = new Vector2(highScorePanelRectTransform.sizeDelta.x + 5, highScorePanelRectTransform.sizeDelta.y);
      highScorePanelRectTransform.position =  new Vector2(highScorePanelRectTransform.position.x - 20, highScorePanelRectTransform.position.y);
      highScoreGrowth1 = true;
    }

    if(highScore > 9999999 && highScoreGrowth2 == false)
    {
      highScorePanelRectTransform.sizeDelta = new Vector2(highScorePanelRectTransform.sizeDelta.x + 5, highScorePanelRectTransform.sizeDelta.y);
      highScorePanelRectTransform.position =  new Vector2(highScorePanelRectTransform.position.x - 20, highScorePanelRectTransform.position.y);
      highScoreGrowth2 = true;
    }

    if(highScore > 99999999 && highScoreGrowth3 == false)
    {
      highScorePanelRectTransform.sizeDelta = new Vector2(highScorePanelRectTransform.sizeDelta.x + 5, highScorePanelRectTransform.sizeDelta.y);
      highScorePanelRectTransform.position =  new Vector2(highScorePanelRectTransform.position.x - 20, highScorePanelRectTransform.position.y);
      highScoreGrowth3 = true;
    }

    if(highScore > 999999999 && highScoreGrowth4 == false)
    {
      highScorePanelRectTransform.sizeDelta = new Vector2(highScorePanelRectTransform.sizeDelta.x + 5, highScorePanelRectTransform.sizeDelta.y);
      highScorePanelRectTransform.position =  new Vector2(highScorePanelRectTransform.position.x - 20, highScorePanelRectTransform.position.y);
      highScoreGrowth4 = true;
    }

  }

}
