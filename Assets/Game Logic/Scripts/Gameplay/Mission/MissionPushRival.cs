using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class MissionPushRival : Missions
{
    [Header("Mission 5 - PR")]
    byte any;

    void Start()
    {
        
    }
    void Update()
    {
        
    }
    protected override void StartMission()
    {
        Debug.Log("Push Rival, Beginning!");
    }
    protected override void CompleteMission()
    {
        Debug.Log("Push Rival, Finish!");
    }
}
