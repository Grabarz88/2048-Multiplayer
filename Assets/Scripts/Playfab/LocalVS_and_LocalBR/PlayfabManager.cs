using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;
using TMPro;


//This script is used to connect to Online Services in PlayFab. It is a Component of PlayFabMenuObject of OnlineMenu scene.
public class PlayfabManager : MonoBehaviour
{
    [SerializeField] GameObject LoginInProgressPanel;
    [SerializeField] GameObject LoginFailedPanel;
    [SerializeField] GameObject LoginSuccessPanel;
    [SerializeField] GameObject LoginSuccessWelcomePanel;
    [SerializeField] TMP_InputField CustomName;
    [SerializeField] TMP_Text DeviceIdentifier;
    [SerializeField] GameObject NameUpdateSuccessPanel;
    [SerializeField] GameObject NameUpdateFailedPanel;
    public string name;

    void Start()
    {
        LoginInProgressPanel.SetActive(true);
        Login();
    }

    //This function allows us to connect with Online Services.
    void Login()
    {
        if(PlayerPrefs.HasKey("OnlineName") == false)
        {
            PlayerPrefs.SetString("OnlineName", "anonymous");
        }

        var request = new LoginWithCustomIDRequest {
        CustomId = SystemInfo.deviceUniqueIdentifier, 
        CreateAccount = true,
        InfoRequestParameters = new GetPlayerCombinedInfoRequestParams {
            GetPlayerProfile = true
        }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSucces, OnLoginError);
    }

    //This function activates on fail to connect to online services. 
    void OnLoginError(PlayFabError error)
    {
        LoginInProgressPanel.SetActive(false);
        LoginFailedPanel.SetActive(true);
        Debug.Log("ERROR Logowanie nieudane");
        Debug.Log(error.GenerateErrorReport());
    }
    
    //This function activates on succesful connection to online services. 
    void OnLoginSucces(LoginResult result)
    {
        LoginInProgressPanel.SetActive(false);
        LoginSuccessPanel.SetActive(true);
        Debug.Log("Logowanie udane");
        if(result.InfoResultPayload.PlayerProfile != null)
        {
            
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
            DeviceIdentifier.text = SystemInfo.deviceUniqueIdentifier;
        }
        else
        {
            name = PlayerPrefs.GetString("OnlineName");
           
        }
     
        
        CustomName.text = name;


        
    }


    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }


    public void SubmitCustomName()
    {
        var request = new UpdateUserTitleDisplayNameRequest{
            DisplayName = CustomName.text,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnSubmitCustomNameSuccess, OnDisplayNameUpdateError);
    }



    void OnSubmitCustomNameSuccess(UpdateUserTitleDisplayNameResult result)
    {
        PlayerPrefs.SetString("OnlineName", CustomName.text);
        NameUpdateSuccessPanel.SetActive(true);
        }

    void OnDisplayNameUpdateError(PlayFabError error)
    {
        NameUpdateFailedPanel.SetActive(true);
    }
    
    
}
