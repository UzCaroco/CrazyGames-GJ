using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Fusion;
using Unity.VisualScripting;
using UnityEngine;

public class TimerMission : NetworkBehaviour
{
    private SunController sunController;
    [Networked][SerializeField] private float timeToWaitTheMission { get; set; } = 0; // Time to wait in seconds
    [Networked][SerializeField] private float timerToCompleteThMission { get; set; } = 0;

    [Networked][SerializeField] private bool isTimerActiveToStart { get; set; } = false;
    [Networked][SerializeField] private bool isTimerActiveToComplete { get; set; } = false; // Flag to check if the timer is active

    // Start is called before the first frame update
    void Start()
    {
       // sunController = FindAnyObjectByType<SunController>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void Spawned()
    {
        if (HasStateAuthority)
        sunController = FindAnyObjectByType<SunController>();
    }
    public override void FixedUpdateNetwork()
    {
        if (HasStateAuthority)
        { 
            StartTimerToWait();
            StartTimerToComplete();
        }
    }

    void StartTimerToWait()
    {
            if (timeToWaitTheMission <= 0 && isTimerActiveToStart)
            {
                isTimerActiveToComplete = true;
                isTimerActiveToStart = false; // Disable the script when the timer is complete

                sunController.SetupBegin(true);
            }
            else if (isTimerActiveToStart && timeToWaitTheMission > 0)
            {
                timeToWaitTheMission -= Runner.DeltaTime;
            }
            else if (!isTimerActiveToStart && timeToWaitTheMission > 0)
            {
                isTimerActiveToStart = true;
            }
    }


    void StartTimerToComplete()
    {

        if (timerToCompleteThMission <= 0 && isTimerActiveToComplete)
        {
            isTimerActiveToComplete = false; // Disable the script when the timer is complete
            
            sunController.SetupConclusion(true);
        }
        else if (isTimerActiveToComplete && timerToCompleteThMission > 0)
        {
            timerToCompleteThMission -= Runner.DeltaTime;
        } 
    }

#region GetTimesValue
    private void GetTimeValue(float timeToWait, float TimerTheMission)
    {
        timeToWaitTheMission = timeToWait; // Set the time to wait for the mission
        timerToCompleteThMission = TimerTheMission; // Set the timer for the mission
    }

    public void InitializeTimeToGet(float timeToWait, float TimerTheMission, SunController sunControl)
    {
        GetTimeValue(timeToWait, TimerTheMission);
        sunController = sunControl; 
    }

#endregion GetTimesValue 
}
