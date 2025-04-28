using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class MissionCollectCoin : Missions
{
    [Header("Mission 1 - CC")]
    byte any;
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
