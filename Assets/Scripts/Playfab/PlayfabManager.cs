using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;


//This script is used to connect to Online Services in PlayFab. It is a Component of PlayFabMenuObject of OnlineMenu scene.
public class PlayfabManager : MonoBehaviour
{
    
    void Start()
    {
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
}
