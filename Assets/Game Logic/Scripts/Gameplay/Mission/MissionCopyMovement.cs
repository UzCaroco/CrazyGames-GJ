using System.Collections;
using System.Collections.Generic;
using CrazyGames;
using Fusion;
using UnityEngine;
using UnityEngine.UI;
using static PlayerCopyMovementController;

public class MissionCopyMovement : Missions
{
    [Header("Mission 2 - CM")]
    [SerializeField] private NetworkRunner runner;

    [SerializeField] private GameObject painelmovement;

    [SerializeField] private NetworkInputHandler networkInputHandler;
    [SerializeField] private Image imageMovement;
    [SerializeField] private Sprite[] spritesMovement = new Sprite[4];

    sbyte[] copyThisMovement = new sbyte[4];
    sbyte totalImages = 0;
    //sbyte[] playerMovementCopy = new sbyte[4];

    Dictionary<PlayerRef, sbyte[]> playerInputs = new();
    Dictionary<PlayerRef, int> playerCorrectCount = new();

    [SerializeField] bool isMission = false;

    void Start()
    {
        StartMission();
    }
    void Update()
    {
        /*print("UPDATE"); 

        if (isMission)
        {
            StartMission();
            isMission = false;
        }*/
    }
    public override void FixedUpdateNetwork()
    {
        print("NETWORK");
        if (isMission)
        {
            ResultMovement();
        }
    }

    public override void CallStartMission()
    {
        StartMission();
        print("Begginng");
    }
    public override void CallCompleteMission()
    {
        CompleteMission();
    }

    void DrawSequence()
    {
        for (int i = 0; i < copyThisMovement.Length; i++)
        {
            copyThisMovement[i] = (sbyte)Random.Range(0, 4);
        }

        StartCoroutine(TimeForSeeMovement());
    }

    IEnumerator TimeForSeeMovement()
    {
        imageMovement.sprite = spritesMovement[copyThisMovement[totalImages]];
        
        yield return new WaitForSeconds(0.5f);

        totalImages++;
        
        if (totalImages > 3)
        {
            painelmovement.SetActive(false);
            StopCoroutine(TimeForSeeMovement());
            isMission = true;
        }

        else
        {
            StartCoroutine(TimeForSeeMovement());
        }

        
    }

    void ResultMovement()
    {
        foreach (var player in PlayerCopyMovementController.AllPlayers)
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

    protected override void StartMission()
    {
        runner = FindObjectOfType<NetworkRunner>();
        painelmovement.SetActive(true);
        networkInputHandler = GetComponent<NetworkInputHandler>();
        networkInputHandler.enabled = true;

        DrawSequence();
        Debug.Log("COPY THE MOVEMENT, Beginning!");
    }
    protected override void CompleteMission()
    {
        for (int i = 0; i < 4; i++)
        {
            copyThisMovement[i] = 0;
        }
        isMission = false;
        painelmovement.SetActive(false);
        networkInputHandler.enabled = false;
        Debug.Log("COPY THE MOVEMENT, Finish!");
    }
}
