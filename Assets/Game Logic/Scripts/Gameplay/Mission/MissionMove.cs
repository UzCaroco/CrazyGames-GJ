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
    public override void FixedUpdateNetwork()
    {
    }
    public override void CallStartMission()
    {
        print("Begginng");
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
        Debug.Log("MOVE, Finish!");
    }
}
