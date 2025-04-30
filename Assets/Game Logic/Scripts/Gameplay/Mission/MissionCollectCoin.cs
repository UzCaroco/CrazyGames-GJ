using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class MissionCollectCoin : Missions
{
    [Header("Mission 1 - CC")]
    byte any;
    //[Networked} byte totalPLayers;
    //[Networked] byte takeCoin;
    [Networked] private Vector2 BombPosition { get; set; }
    [Networked] private bool isInitialized { get; set; }

    void Start()
    {
        
    }
    void Update()
    {
        StartMission();
    }

    protected override void StartMission()
    {
        Debug.Log("Collect Coin, Beginning!");
    }
    protected override void CompleteMission()
    {
        Debug.Log("Collect Coin, Finish!");
    }
}
