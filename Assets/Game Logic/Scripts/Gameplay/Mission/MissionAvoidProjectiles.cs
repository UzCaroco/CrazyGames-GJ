using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class MissionAvoidProjectiles : Missions
{
    [Header("Mission 0 - A")]
    bool isInstantiate = false;
    private Vector2[] directionsProjectitles;

    private int randomsDirections;
    private int lastRandom;
    int[] quantityProjectiles = new int[4];

    void Start()
    {
        
    }
    void Update()
    {
        StartMission();
    }

    void SetupDirections()
    {

    }

    void RandomLocalIntanciete()
    {
        randomsDirections = Random.Range(0,4); //cima 

    }

    protected override void StartMission()
    {
        Debug.Log("Avoid projéteis, Beginning!");
    }
    protected override void CompleteMission()
    {
        Debug.Log("Avoid projéteis, Finish!");
    }
}
