using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class MissionCollectCoin : Missions
{
    [Header("Mission 1 - CC")]
    private List<PlayerController> playersActive = new List<PlayerController>();

    sbyte takeCoin;

    // x = -10.5 até 10.5 y = - 7 ate 7 
    [Networked] private Vector2 coinPosition { get; set; }
    [Networked] private bool isInitialized { get; set; }

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
        foreach (var playerController in playersActive)
        {
            if (playerController != null)
            {
                playersActive.Add(playerController);
                takeCoin++;
            }
        }
    }

    void DrawPos()
    {
        coinPosition = new Vector2((float)Random.Range(-10.5f, 10.5f), (float)Random.Range(-7f, 7f));
    }

    void ControlSpwnCoins()
    {
        if (!isInitialized) 
        { 
            while(takeCoin > 0)
            {
                takeCoin--;
                DrawPos();


            }
        }
    }

    void Instanciate()
    {

    }

    protected override void StartMission()
    {
        Debug.Log("Collect Coin, Beginning!");
    }
    protected override void CompleteMission()
    {

        takeCoin = 0;
        isInitialized = false;
        
        Debug.Log("Collect Coin, Finish!");
    }
}
