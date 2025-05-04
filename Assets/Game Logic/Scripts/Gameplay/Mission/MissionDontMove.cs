using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CrazyGames;
using Fusion;
using UnityEngine;

public class MissionDontMove : Missions
{
    [Header("Mission 3 - DM")]
    byte any;

    NetworkRunner runner;

    void Start()
    {
        
    }
    void Update()
    {
        StartMission();
    }
    public override void FixedUpdateNetwork()
    {

    }
    public override void CallStartMission()
    {
        runner = FindObjectOfType<NetworkRunner>(); //Pega o NetworkRunner na cena
        Debug.Log("Quantidade de players ativos: " + runner.ActivePlayers.Count());


        foreach (var player in runner.ActivePlayers)
        {
            Debug.Log("Checando player: " + player);

            var networkObject = runner.GetPlayerObject(player); //Percorre os objetos de rede ativos (Players)

            Debug.Log("NETWORKOBJECT VAZIO??: " + networkObject);
            if (networkObject != null) //Verifica se o objeto de rede não é nulo
            {
                Debug.Log("EXISTE O NETWORKOBJECT");

                PlayerController playerController = networkObject.GetComponent<PlayerController>(); //Pega o script PlayerController do objeto de rede

                if (playerController != null)
                {
                    
                    playerController.dontMove = true; // ativa o booleano

                }
            }
        }
    }
    public override void CallCompleteMission()
    {
        CompleteMission();
    }

    protected override void StartMission()
    {
        
    }
    protected override void CompleteMission()
    {
        Debug.Log("DONT MOVE, Finish!");
    }
}
