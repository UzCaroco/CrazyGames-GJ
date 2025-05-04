using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CrazyGames;
using Fusion;
using UnityEngine;

public class MissionCollectCoin : Missions
{
    [Header("Mission 1 - CC")]
    NetworkRunner runner;

    sbyte totalPlayers;
    sbyte spawnedCoins = 0;
    [SerializeField] private GameObject CoinPrefab;
    // x = -10.5 até 10.5 y = - 7 ate 7 
    [Networked] private Vector2 coinPosition { get; set; }

    public override void FixedUpdateNetwork()
    {
       // base.FixedUpdateNetwork();
    }

    public override void CallStartMission()
    {
        StartMission();
    }
    public override void CallCompleteMission()
    {
        CompleteMission();
    }

    void GetTotalPlayers()
    {
        runner = FindObjectOfType<NetworkRunner>(); //Pega o NetworkRunner na cena

        totalPlayers = sbyte.Parse(runner.ActivePlayers.Count().ToString());
        Debug.Log("Quantidade de players ativos: " + runner.ActivePlayers.Count());
    }

    void DrawPos()
    {
        coinPosition = new Vector2((float)Random.Range(-10.5f, 10.5f), (float)Random.Range(-7f, 7f));
        Instanciate();
    }

    void ControlSpawnCoins()
    {

        while (spawnedCoins < totalPlayers) // enquanto a quantidade de moedas for menor que a quantidade de players
        {
            spawnedCoins++;
            DrawPos();
        }
    }

    void Instanciate()
    {
        NetworkObject coin = Runner.Spawn(CoinPrefab, coinPosition, Quaternion.identity, null);
        coin.transform.SetParent(transform);
    }

    protected override void StartMission()
    {
        spawnedCoins = 0;

        GetTotalPlayers();
        ControlSpawnCoins(); 

        Debug.Log("Collect Coin, Beginning!");
    }
    protected override void CompleteMission()
    {
        
        Debug.Log("Collect Coin, Finish!");
    }
}
