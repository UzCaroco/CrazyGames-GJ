using CrazyGames;
using Fusion;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    [SerializeField] private GameObject sunControllerPrefab;
    
    Animator anim;
    [SerializeField] AnimatorOverrideController[] animatorOverrideController;
    int idex;

    public GameObject playerPrefab;
    [SerializeField] private NetworkRunner runner;




    [SerializeField] int PlayerSpawn { get; set; }

    int numRandom;
    int minPlayer = 0;
    int maxPlayer = 8;


    List<int> numeros = new List<int>();


    void Start()
    {
        DrawSkinController();
    }

    public void PlayerJoined(PlayerRef player)
    {
        // Só o peer com StateAuthority vai instanciar

        if (player == Runner.LocalPlayer) // ou Runner.IsServer se tiver usando Server/Client
        {
            /*print("TA O QUE" + gameManager);


            if (gameManager == null)
            {
                Debug.Log("entreiiiia aqui no sei la");
                gameManager = runner.Spawn(sunControllerPrefab, Vector3.zero, Quaternion.identity, inputAuthority: null);
            }

            print("TA O QUE"+gameManager);*/

            NetworkObject playerObj = Runner.Spawn(playerPrefab, Vector3.zero, Quaternion.identity, inputAuthority: player);
            Runner.SetPlayerObject(player, playerObj);
            
            anim = playerObj.GetComponent<Animator>();
            PlayerIsSpawned();
            anim.runtimeAnimatorController = animatorOverrideController[SetNumRandom()];
        }
    }


    

    public void DrawSkinController()
    {
        List<int> temp = new List<int>();
        while (temp.Count < 8)
        {
            int numeroAleatorio = Random.Range(minPlayer, maxPlayer);
            if (!temp.Contains(numeroAleatorio))
            {
                temp.Add(numeroAleatorio);
            }
        }

        for (int i = 0; i < temp.Count; i++)
        {
            numeros.Add(temp[i]);
        }

        foreach (int numero in numeros)
        {
            Debug.Log(numero);
        }
    }
    public void PlayerIsSpawned()
    {
        if (PlayerSpawn < numeros.Count)
        {
            numRandom = numeros[PlayerSpawn];
            Debug.Log(numeros[PlayerSpawn] + " " + numRandom);
            PlayerSpawn++;
        }
        else
        {
            Debug.LogError("PlayerSpawn fora dos limites da lista de números!");
        }
    }

    public int SetNumRandom()
    {
        return numRandom;
    }
}
