using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabLeaderboardManagerScript : MonoBehaviour
{
    
    void Start()
    {
        Debug.Log("We will send score to Leaderboard");
        Login();
    }
   

   //This function allows us to connect with Online Services.
    void Login()
    {
        var request = new LoginWithCustomIDRequest {
        CustomId = SystemInfo.deviceUniqueIdentifier, 
        CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSucces, OnError);
    }

    //This function activates on succesful connection to online services. 
    void OnSucces(LoginResult result)
    {
        Debug.Log("Logowanie udane");
    }

    //This function activates on fail to connect to online services. 
    void OnError(PlayFabError error)
    {
        Debug.Log("ERROR Logowanie nieudane");
        Debug.Log(error.GenerateErrorReport());
    }

    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest {
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate {
                    StatisticName  = "ClassicScore",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError); 
    }
    
    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Succesful leaderboard sent");
    }

}
