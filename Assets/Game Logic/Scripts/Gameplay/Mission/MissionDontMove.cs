using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class MissionDontMove : Missions
{
    [Header("Mission 3 - DM")]
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
        Debug.Log("DONT MOVE, Beginning!");
    }
    protected override void CompleteMission()
    {
        Debug.Log("DONT MOVE, Finish!");
    }
}
