using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class MissionStaySquare : Missions
{
    [Header("Mission 7 - SS")]
    [SerializeField] private int posXSquare, posYSquere;
    private Vector2 squarePos;
    bool isInicialized;

    void Start()
    {
        
    }
    private void FixedUpdate()
    {
        
    }

   /* protected override void FixedUpdateNetwork()
    {
        //base.FixedUpdateNetwork();

        StartMission();
    }*/

    void SquarePos()
    {
        posXSquare = Random.Range(-7, 7);
    } 

    protected override void StartMission()
    {
        Debug.Log("Stay Square, Beginning!");
    }
    protected override void CompleteMission()
    {
        Debug.Log("Stay Square, Finish!");
    }
}
