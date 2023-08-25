using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class PlayFabLeaderboardManagerScript : MonoBehaviour
{
    [SerializeField] GameObject ScoreCounter;
    
    [SerializeField] GameObject ParentOfOnlinePanels;
    [SerializeField] GameObject ConnectingPanel;
    [SerializeField] GameObject ConnectionFailedPanel;
    [SerializeField] GameObject ConnectionSuccessPanel;
    [SerializeField] GameObject LeaderboardGetUpdateFailesPanel;
    [SerializeField] GameObject LeaderboardPanel;
    [SerializeField] GameObject LeaderboardPosition1;
    [SerializeField] GameObject LeaderboardPosition2;
    [SerializeField] GameObject LeaderboardPosition3;
    [SerializeField] GameObject LeaderboardPosition4;
    [SerializeField] GameObject LeaderboardPosition5;
    [SerializeField] GameObject LeaderboardPosition6;
    [SerializeField] GameObject LeaderboardPosition7;
    [SerializeField] GameObject LeaderboardPosition8;
    [SerializeField] GameObject LeaderboardPosition9;
    [SerializeField] GameObject LeaderboardPosition10;
    [SerializeField] GameObject LeaderboardPlayerScore;
    [SerializeField] public TMP_InputField NameInputField;
    public int score;
    public string name;
    // [SerializeField] GameObject ScoreSendingPanel;
    // [SerializeField] GameObject ScoreSendingFailesPanel;
    // [SerializeField] GameObject ScoreSendingSuccessPanel;
    
    PlayFab.ClientModels.PlayerLeaderboardEntry[] LeaderboardEntries = new PlayFab.ClientModels.PlayerLeaderboardEntry[10];
    GameObject[] LeaderboardPositions = new GameObject[10];
    void Start()
    {
        Debug.Log("We will send score to Leaderboard");
        ConnectingPanel.SetActive(false);
        ConnectionFailedPanel.SetActive(false);
        ConnectionSuccessPanel.SetActive(false);
        LeaderboardGetUpdateFailesPanel.SetActive(false);
        LeaderboardPanel.SetActive(false);
        // ScoreSendingPanel.SetActive(false);
        // ScoreSendingFailesPanel.SetActive(false);
        // ScoreSendingSuccessPanel.SetActive(false);
        LeaderboardPositions[0] = LeaderboardPosition1;
        LeaderboardPositions[1] = LeaderboardPosition2;
        LeaderboardPositions[2] = LeaderboardPosition3;
        LeaderboardPositions[3] = LeaderboardPosition4;
        LeaderboardPositions[4] = LeaderboardPosition5;
        LeaderboardPositions[5] = LeaderboardPosition6;
        LeaderboardPositions[6] = LeaderboardPosition7;
        LeaderboardPositions[7] = LeaderboardPosition8;
        LeaderboardPositions[8] = LeaderboardPosition9;
        LeaderboardPositions[9] = LeaderboardPosition10;

        
    }
   

   //This function allows us to connect with Online Services. It will first create a panel indicating trying to connect.
    public void Login()
    {
        //We activate panel informing about our attempt to connect to online servers
        ParentOfOnlinePanels.SetActive(true);
        ConnectingPanel.SetActive(true);
        var request = new LoginWithCustomIDRequest {
        CustomId = SystemInfo.deviceUniqueIdentifier, 
        CreateAccount = true,
        InfoRequestParameters = new GetPlayerCombinedInfoRequestParams {
            GetPlayerProfile = true
        }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSucces, OnLoginError);
    }

    //This function activates on succesful connection to online services. 
    void OnLoginSucces(LoginResult result)
    {
        
        Debug.Log("Logowanie udane");
        string name = null;
        if(result.InfoResultPayload.PlayerProfile != null)
        {
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
        }
        if(name == null)
        {
            Debug.Log("brak podanego imienia");
        }
        else 
        {
            Debug.Log("Imię jest już podane");
        }
        ConnectingPanel.SetActive(false);
        ConnectionSuccessPanel.SetActive(true);
        GetLeaderboard();
    }

    //This function activates on fail to connect to online services. 
    void OnLoginError(PlayFabError error)
    {
        Debug.Log("ERROR Logowanie nieudane");
        Debug.Log(error.GenerateErrorReport());
        ConnectingPanel.SetActive(false);
        ConnectionFailedPanel.SetActive(true);
    }


    void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest {
            StatisticName = "ClassicScore",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnLeaderboardGetError);
    }
    
    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        LeaderboardPanel.SetActive(true);
        foreach (var item in result.Leaderboard) {
            Debug.Log(item.Position + " " + item.DisplayName + " " + item.StatValue);
            if(item != null)
            {
                LeaderboardEntries[item.Position] = item;
                LeaderboardPositions[item.Position].transform.GetChild(1).GetComponent<TMP_Text>().text = item.DisplayName;
                LeaderboardPositions[item.Position].transform.GetChild(2).GetComponent<TMP_Text>().text = item.StatValue.ToString();
            }
        }
        LeaderboardPlayerScore.transform.GetChild(1).GetComponent<TMP_Text>().text = ScoreCounter.GetComponent<ScoreCounterScript>().GetPoints().ToString();
    }

    
    void OnLeaderboardGetError(PlayFabError error)
    {
        Debug.Log("ERROR Nie mozna pobrać Leaderboardu");
        LeaderboardGetUpdateFailesPanel.SetActive(true);
        Debug.Log(error.GenerateErrorReport());
        
    }

    
    
    public void SendLeaderboard()
    {
        SubmitNameButton();

        var request = new UpdatePlayerStatisticsRequest {
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate {
                    StatisticName  = "ClassicScore",
                    Value = ScoreCounter.GetComponent<ScoreCounterScript>().GetPoints(),
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdateSuccess, OnLeaderboardUpdateError); 
    }

    void OnLeaderboardUpdateSuccess(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Succesful leaderboard sent");
    }

    void OnLeaderboardUpdateError(PlayFabError error)
    {
        Debug.Log("ERROR Update nieudany");
        Debug.Log(error.GenerateErrorReport());
        ConnectingPanel.SetActive(false);
        ConnectionFailedPanel.SetActive(true);
    }

    public void SubmitNameButton()
    {
        var request = new UpdateUserTitleDisplayNameRequest{
            DisplayName = NameInputField.text,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnDisplayNameUpdateError);
    }
    
    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Zmieniono imię gracza");
        GetLeaderboard();
        
    }

    void OnDisplayNameUpdateError(PlayFabError error)
    {
        Debug.Log("ERROR Update nieudany");
        Debug.Log(error.GenerateErrorReport());
        ConnectingPanel.SetActive(false);
        ConnectionFailedPanel.SetActive(true);
    }
    

    

}
