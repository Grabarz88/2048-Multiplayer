using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIControlling_BRLocal : MonoBehaviour
{
   
[SerializeField] GameObject BlockSpawner;
[SerializeField] TextMeshProUGUI turnText;
[SerializeField] TextMeshProUGUI turnNumber;
[SerializeField] GameObject ColorPanel;
[SerializeField] TextMeshProUGUI nowMoving;
public int moves;
public int turn;
public int activePlayersCounter = 2;
LocalBRSpawnBlock SpawnBlock; 

    void Start()
    {
        SpawnBlock = BlockSpawner.GetComponent<LocalBRSpawnBlock>();
        turn = SpawnBlock.turnsToStartBR;


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
        nowMoving.GetComponent<RectTransform>().position = new Vector2 (-270, 430);
        ColorPanel.GetComponent<RectTransform>().position = new Vector2 (-315, 400);
   }
}
