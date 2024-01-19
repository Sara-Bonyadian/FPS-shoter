using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay.Models;
using Unity.Services.Relay;
using Unity.Networking.Transport.Relay;
using Unity.Netcode.Transports.UTP;
using QFSW.QC;


public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button serverBtn;
    [SerializeField] private Button hostBtn;
    [SerializeField] private Button clientBtn;
    
    [SerializeField] private GameObject quantom;

    bool quantumToggle = false;
 


    private void Awake()
    {
        
        hostBtn.onClick.AddListener(() =>
        {
            Debug.Log("Host button clicked");
            CreateRelay();
          
            
        });


        clientBtn.onClick.AddListener(() =>
        {
            print("started joining with code: ");
            //startTime = Time.time;

            quantom.SetActive(false);
            //quantumToggle = !quantumToggle;
            //quantom.SetActive(quantumToggle);

        });



        //serverBtn.onClick.AddListener(() =>
        //{
        //    NetworkManager.Singleton.StartServer();
        //    print("server");
        //});

        //hostBtn.onClick.AddListener(() =>
        //{
        //    NetworkManager.Singleton.StartHost();
        //    print("Host");
        //});

        //clientBtn.onClick.AddListener(() =>
        //{
        //    NetworkManager.Singleton.StartClient();
        //    print("Client");
        //});


    }


    private async void Start()
    {
        
        await UnityServices.InitializeAsync();
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("sign in " + AuthenticationService.Instance.PlayerId);
        };
        await AuthenticationService.Instance.SignInAnonymouslyAsync();

           


    }
    void Update()
    {
        //float elapsedSeconds = Time.time - startTime;
        //time.text = "Time: " + Mathf.FloorToInt(elapsedSeconds) + "s";
        //if (Mathf.FloorToInt(elapsedSeconds) == 60)
        //{
        //    GameOver();
        //}



    }

    [Command]
    public async void CreateRelay()
    {
        
        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(2);
            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            RelayServerData relayServerData = new RelayServerData(allocation, "dtls");

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            NetworkManager.Singleton.StartHost();
            Debug.Log("Host created with this join code: ");
            Debug.Log(joinCode);
        }
        catch (RelayServiceException e)
        {
            print("error");
            Debug.Log(e);           
        }
        
    }

    [Command]
    private async void JoinRelay(string joinCode)
    {
        try
        {
            Debug.Log("joining Relay with" + joinCode);
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

            RelayServerData relayServerData = new RelayServerData(joinAllocation, "dtls");

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            NetworkManager.Singleton.StartClient();
            Debug.Log("Client joined");
           


        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
        }
    }

    //void GameOver()
    //{
    //    Time.timeScale = 0;

    //    finishTime.enabled = true;

    //}

}
