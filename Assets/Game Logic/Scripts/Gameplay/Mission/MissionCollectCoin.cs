using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class MissionCollectCoin : Missions
{
    [Header("Mission 1 - CC")]
    private List<PlayerController> playersActive = new List<PlayerController>();

    sbyte takeCoin;
    [SerializeField] private GameObject CoinPrefab;
    // x = -10.5 até 10.5 y = - 7 ate 7 
    [Networked] private Vector2 coinPosition { get; set; }
    [Networked] private bool isInicialized { get; set; }

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
        if (!isInicialized) 
        { 
            while(takeCoin > 0)
            {
                takeCoin--;
                DrawPos();

                Instanciate();
            }
            isInicialized = true;
        }
    }

    void Instanciate()
    {
        //Debug.Log("Spawndando em X:" + posXBomb + "em Y:" + posYBomb + "sendo a:" + index);
        NetworkObject coin = Runner.Spawn(CoinPrefab, coinPosition, Quaternion.identity);
        coin.transform.SetParent(transform);
        isInicialized = true;
    }

    protected override void StartMission()
    {
        ControlSpwnCoins(); 

        Debug.Log("Collect Coin, Beginning!");
    }
    protected override void CompleteMission()
    {
        takeCoin = 0;
        isInicialized = false;
        
        Debug.Log("Collect Coin, Finish!");
    }
}
