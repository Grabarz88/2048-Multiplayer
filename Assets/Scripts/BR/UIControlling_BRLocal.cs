using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIControlling_BRLocal : MonoBehaviour
{
[SerializeField] GameObject MainCamera;
[SerializeField] GameObject BlockSpawner;
[SerializeField] TextMeshProUGUI turnText;
[SerializeField] TextMeshProUGUI turnNumber;
[SerializeField] GameObject ColorPanel;
[SerializeField] TextMeshProUGUI nowMoving;

[SerializeField] GameObject SkippedPlayerPanel;
[SerializeField] TextMeshProUGUI SkippedPlayerID;

[SerializeField] GameObject EliminatedPlayerPanel;
[SerializeField] TextMeshProUGUI EliminatedPlayerID;

[SerializeField] GameObject WinnerPanel;
[SerializeField] TextMeshProUGUI WinnerID;




public int moves;
public int turn;
public int activePlayersCounter = 2;
LocalBRSpawnBlock SpawnBlock; 
Camera Camera;

    void Start()
    {
        SpawnBlock = BlockSpawner.GetComponent<LocalBRSpawnBlock>();
        turn = SpawnBlock.turnsToStartBR;
        SkippedPlayerPanel.SetActive(false);
        EliminatedPlayerPanel.SetActive(false);
        WinnerPanel.SetActive(false);


    }

   public void ChangeTurnNumber(int checkedMoves)
   {
        moves = checkedMoves;
        turn = moves/activePlayersCounter;
        if(turn < 0){turn = turn*(-1);}
        turnNumber.text = turn.ToString();

   }
   
   public void MoveForBR()
   {
        turnText.GetComponent<RectTransform>().position = new Vector2 (530, 430);
        turnNumber.GetComponent<RectTransform>().position = new Vector2 (530, 380);
     //    nowMoving.GetComponent<RectTransform>().position = new Vector2 (-270, 430);
        ColorPanel.GetComponent<RectTransform>().position = new Vector2 (-315, 400);
   }

   public void AnnounceELiminatedPlayer(int player)
   {
     EliminatedPlayerPanel.SetActive(true);
     EliminatedPlayerID.text = player.ToString();
     EliminatedPlayerPanel.GetComponent<RectTransform>().position = new Vector2(100, 500);
     StartCoroutine(PullDownEliminationPanel());
   }

   public void AnnounceMoveSkip(int player)
   {
     SkippedPlayerPanel.SetActive(true);
     SkippedPlayerID.text = player.ToString();
     SkippedPlayerPanel.GetComponent<RectTransform>().position = new Vector2(700, 100);
     StartCoroutine(PullUpMoveSkippedPanel());
   }

     
     IEnumerator PullUpMoveSkippedPanel()
     {
          for (int l = 30; l >= 0; l -= 1)
          {
          SkippedPlayerPanel.GetComponent<RectTransform>().position += new Vector3(-5, 0, 0);
          SkippedPlayerPanel.GetComponent<Image>().color -= new Color32(0,0,0,2);
          yield return new WaitForSeconds(.05f);   
          }
          SkippedPlayerPanel.SetActive(false);
     }
     
     IEnumerator PullDownEliminationPanel()
     {
          for (int l = 30; l >= 0; l -= 1)
          {
          EliminatedPlayerPanel.GetComponent<RectTransform>().position += new Vector3(0, -l, 0);
          EliminatedPlayerPanel.GetComponent<Image>().color -= new Color32(0,0,0,2);
          yield return new WaitForSeconds(.05f);   
          }
          EliminatedPlayerPanel.SetActive(false);

          activePlayersCounter = SpawnBlock.activePlayersCounter;
          if(activePlayersCounter <= 1)
          {
               StartCoroutine(PullUpWinnerPanel());
          }
          
     }

     IEnumerator PullUpWinnerPanel()
     {
          int player = 0;
          if(SpawnBlock.isPlayer1Playing == true){player = 1;}
          else if(SpawnBlock.isPlayer2Playing == true){player = 2;}
          else if(SpawnBlock.isPlayer3Playing == true){player = 3;}
          else if(SpawnBlock.isPlayer4Playing == true){player = 4;}
          
          WinnerPanel.SetActive(true);
          WinnerPanel.GetComponent<RectTransform>().position = new Vector2(100, -260);
          Camera = MainCamera.GetComponent<Camera>();
          WinnerID.text = player.ToString();
          Color32 winnerColor = new Color32(0,0,0,0);
          int winnerColorID = 0;
          if(player == 1){winnerColorID = SpawnBlock.Player1Color;}
          else if(player == 2){winnerColorID = SpawnBlock.Player2Color;}
          else if(player == 3){winnerColorID = SpawnBlock.Player3Color;}
          else if(player == 4){winnerColorID = SpawnBlock.Player4Color;}

          if (winnerColorID == 1){winnerColor = new Color32(119,221,250,255);}
          else if (winnerColorID == 2){winnerColor = new Color32(242,118,140,255);}
          else if (winnerColorID == 3){winnerColor = new Color32(137,217,171,255);}
          else if (winnerColorID == 4){winnerColor = new Color32(236,123,222,255);}
          else if (winnerColorID == 5){winnerColor = new Color32(104,105,104,255);}

          for (int l = 0; l <= 30; l += 1)
          {
          WinnerPanel.GetComponent<RectTransform>().position += new Vector3(0, l, 0);
          // WinnerPanel.GetComponent<Image>().color += new Color32(0,0,0,2);
          Camera.backgroundColor = Color.Lerp(new Color32(250, 248, 239, 0), winnerColor, l*.05f);
          yield return new WaitForSeconds(.05f);   
          }
     }

     


}
