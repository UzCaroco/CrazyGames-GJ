using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CrazyGames;
using Fusion;
using UnityEngine;
using UnityEngine.UI;

public class MissionCopyMovement : Missions
{
    [Header("Mission 2 - CM")]
    [SerializeField] private NetworkRunner runner;

    [SerializeField] private GameObject painelmovement;

    //[SerializeField] private NetworkInputHandler networkInputHandler;
    [SerializeField] private Image imageMovement;
    [SerializeField] private Sprite[] spritesMovement = new Sprite[4];

    byte[] copyThisMovement = new byte[4];
    byte totalImages = 0;
    //sbyte[] playerMovementCopy = new sbyte[4];

    //Dictionary<PlayerRef, byte[]> playerInputs = new();
    //Dictionary<PlayerRef, int> playerCorrectCount = new();

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
        /*print("NETWORK");
        if (isMission)
        {
            ResultMovement();
        }*/
    }

    public override void CallStartMission()
    {
        StartMission();
    }
    public override void CallCompleteMission()
    {
        CompleteMission();
    }

    void DrawSequence()
    {
        for (int i = 0; i < 4; i++)
        {
            copyThisMovement[i] = (byte)Random.Range(0, 4);
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


            Debug.Log("FINALIZOU A CORROTINA");
            //--------------------------------------------------------------------------------------------------------------------------------//
            //-----------Envia os comandos que o player deve seguir para o player controller de cada um-----------//
            //---------------------------------------------------------------//

            runner = FindObjectOfType<NetworkRunner>(); //Pega o NetworkRunner na cena
            Debug.Log(runner + "EXISTE");
            Debug.Log("Quantidade de players ativos: " + runner.ActivePlayers.Count());


            foreach (var player in runner.ActivePlayers)
            {
                Debug.Log("Checando player: " + player);

                var networkObject = runner.GetPlayerObject(player); //Percorre os objetos de rede ativos (Players)

                Debug.Log("NETWORKOBJECT VAZIO??: " + networkObject);
                if (networkObject != null) //Verifica se o objeto de rede não é nulo
                {
                    Debug.Log("EXISTE O NETWORKOBJECT");

                    PlayerController playerController = networkObject.GetComponent<PlayerController>(); //Pega o script PlayerController do objeto de rede

                    if (playerController != null)
                    {
                        Debug.Log("EXISTE O PLAYERCONTROLLER");
                        playerController.timeToCopyTheMovements = true; //Ativa o tempo para copiar os movimentos
                        Debug.Log("ESTA TRUE?: " + playerController.timeToCopyTheMovements);
                        playerController.copyThisMovement = copyThisMovement; //Adiciona a lista
                    }
                }
            }
            //----------------------------------------------//
            //-----------------------------------------------------------------------------------//
            //--------------------------------------------------------------------------------------------------------------------------------//



        }

        else
        {
            StartCoroutine(TimeForSeeMovement());
        }

        
    }

    /*void ResultMovement()
    {
        foreach (var player in PlayerCopyMovementController.AllPlayers)
        {
            if (player.MovementIndex >= 4) // jogador terminou
            {
                int correct = 0;

                for (int i = 0; i < 4; i++)
                {
                    print(player.Movements.Get(i));
                    if (player.Movements.Get(i) == copyThisMovement[i])
                    {
                        print(player.Movements.Get(i) + " = " + copyThisMovement[i]);
                        correct++;
                    }
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
    }*/

    protected override void StartMission()
    {
        //runner = FindObjectOfType<NetworkRunner>();
        painelmovement.SetActive(true);
        //networkInputHandler = GetComponent<NetworkInputHandler>();
        //networkInputHandler.enabled = true;

        DrawSequence();
        Debug.Log("COPY THE MOVEMENT, Beginning!");
    }
    protected override void CompleteMission()
    {
        copyThisMovement = new byte[4]; //Limpa o array
        totalImages = 0;

        isMission = false;
        painelmovement.SetActive(false);
        //networkInputHandler.enabled = false;
        Debug.Log("COPY THE MOVEMENT, Finish!");
    }
}
