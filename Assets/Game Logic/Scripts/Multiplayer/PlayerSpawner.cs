using CrazyGames;
using Fusion;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    [SerializeField] private GameObject sunControllerPrefab;
    [SerializeField] AnimatorOverrideController[] animatorOverrideController;
    public GameObject playerPrefab;
    [SerializeField] private NetworkRunner runner;

    List<int> numeros = new List<int>();

    void Start()
    {
        DrawSkinController(); // Gera a lista com semente fixa
    }

    public void PlayerJoined(PlayerRef player)
    {
        // Apenas o LocalPlayer spawna seu próprio jogador
        if (player == Runner.LocalPlayer)
        {
            NetworkObject playerObj = Runner.Spawn(playerPrefab, Vector3.zero, Quaternion.identity, inputAuthority: player);
            Runner.SetPlayerObject(player, playerObj);

            Animator anim = playerObj.GetComponent<Animator>();
            int playerCount = Runner.ActivePlayers.Count() - 1; // Índice baseado na ordem de chegada

            if (playerCount < numeros.Count)
            {
                int skinIndex = numeros[playerCount];
                anim.runtimeAnimatorController = animatorOverrideController[skinIndex];
            }
            else
            {
                Debug.LogError("Índice de skin fora dos limites!");
            }
        }
    }

    // Gera uma lista de números aleatórios únicos com semente fixa
    void DrawSkinController()
    {
        Random.InitState(12345); // Semente fixa para todos os clientes
        List<int> temp = new List<int>();

        while (temp.Count < 8)
        {
            int numeroAleatorio = Random.Range(0, 8);
            if (!temp.Contains(numeroAleatorio))
            {
                temp.Add(numeroAleatorio);
            }
        }

        numeros.Clear();
        numeros.AddRange(temp);
    }
}