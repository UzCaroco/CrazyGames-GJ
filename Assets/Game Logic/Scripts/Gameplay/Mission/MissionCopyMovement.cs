using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.UI;
using static PlayerCopyMovementController;

public class MissionCopyMovement : Missions
{
    [Header("Mission 2 - CM")]
    [SerializeField] NetworkInputHandler NetworkInputHandler;
    Image imageMovement;
    SpriteRenderer[] spritesMovement = new SpriteRenderer[4];

    sbyte[] copyThisMovement = new sbyte[4];
    sbyte[] playerMovementCopy = new sbyte[4];

    Dictionary<PlayerRef, sbyte[]> playerInputs = new();
    Dictionary<PlayerRef, int> playerCorrectCount = new();


    void Start()
    {
        
    }
    void Update()
    {
        //StartMission();
    }
    public override void FixedUpdateNetwork()
    {
        StartMission();
    }

    void DrawSequence()
    {
        for (int i = 0; i < copyThisMovement.Length; i++)
        {
            copyThisMovement[i] = (sbyte)Random.Range(0, 5);

        }
    }

    void WaitInput()
    {
        foreach (var player in MissionCopyMovement.AllPlayers)
        {
            if (player.MovementIndex >= 4) // jogador terminou
            {
                int correct = 0;

                for (int i = 0; i < 4; i++)
                {
                    if (player.Movements.Get(i) == copyThisMovement[i])
                        correct++;
                }

                if (correct == 4)
                {
                    Debug.Log($"Jogador {player.Object.InputAuthority} acertou tudo!");
                }
                else
                {
                    Debug.Log($"Jogador {player.Object.InputAuthority} errou {4 - correct}.");
                }
            }
        }
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
        Debug.Log("COPY THE MOVEMENT, Beginning!");
    }
    protected override void CompleteMission()
    {
        Debug.Log("COPY THE MOVEMENT, Finish!");
    }
}
