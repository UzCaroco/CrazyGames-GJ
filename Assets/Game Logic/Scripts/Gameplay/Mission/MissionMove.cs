using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class MissionMove : Missions
{
    [Header("Mission 4 - M")]
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
        Debug.Log("MOVE, Beginning!");
    }
    protected override void CompleteMission()
    {
        Debug.Log("MOVE, Finish!");
    }
}
