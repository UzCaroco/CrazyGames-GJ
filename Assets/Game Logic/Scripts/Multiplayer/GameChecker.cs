using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using UnityEngine;

public class GameChecker : NetworkBehaviour
{
    NetworkRunner runner;
    [SerializeField] GameManager gameManager;

     List<PlayerChecker> playerCheckers = new List<PlayerChecker>();

    List<Tuple<PlayerRef, int>> playersSequence = new List<Tuple<PlayerRef, int>>();
    Dictionary<PlayerRef, int> playerScores = new Dictionary<PlayerRef, int>();

    private void OnEnable()
    {
        runner = FindObjectOfType<NetworkRunner>(); //Pega o NetworkRunner na cena

        /*foreach (var player in runner.ActivePlayers)
        {
            var networkObject = runner.GetPlayerObject(player); //Percorre os objetos de rede ativos (Players)

            if (networkObject != null) //Verifica se o objeto de rede não é nulo
            {
                PlayerController playerController = networkObject.GetComponent<PlayerController>(); //Pega o script PlayerController do objeto de rede

                if (playerController != null)
                {
                    playerControllers.Add(playerController); //Adiciona a lista
                }
            }
        }*/
    }



    

    public void AdicionarPlayerALista(PlayerChecker playerChecker)
    {
        if (playerCheckers.Contains(playerChecker)) return; // Se o player já estiver na lista, não adiciona novamente

        if (!playerCheckers.Contains(playerChecker))
        {
            if (playerChecker.playerController != null)
            {
                Debug.Log("Adicionando player " + playerChecker + " à lista de jogadores.");
                playerCheckers.Add(playerChecker);
            }
        }
    }

    //SOMENTE NO FINAL quando acabar a missão, verifica os jogadores completaram
    public void CheckPlayersInTheEndOfMission(sbyte mission)
    {
        Debug.Log("Verificando os jogadores no final da missão: " + mission);

        if (mission == 0 || mission == 3 || mission == 4 || mission == 5 || mission == 6)
        {
            AddEqualScores(mission); // Adiciona pontuação igual para todos os jogadores que completaram a missão
        }
        else
        {
            AdicionarPontucaoEmSequencia(); // Adiciona pontuação em sequência para os jogadores que completaram a missão
        }

        //--------------------------------------------------------------------------------------------------------------------------------//
        //-----------Envia os comandos para todos os player resetarem os valores dos booleanos-----------//
        //---------------------------------------------------------------//


        runner = FindObjectOfType<NetworkRunner>(); //Pega o NetworkRunner na cena
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
                    playerController.missionProjectile = false; // Reseta a missão do player
                    playerController.missionCollectCoin = false; // Reseta a missão do player
                    playerController.missionCopyMoviment = false; // Reseta a missão do player
                    playerController.missionDontMove = false; // Reseta a missão do player
                    playerController.missionMove = false; // Reseta a missão do player
                    playerController.missionPushRival = false; // Reseta a missão do player
                    playerController.missionBomb = false; // Reseta a missão do player
                    playerController.missionStaySquare = false; // Reseta a missão do player

                    playerController.timeToCopyTheMovements = false; // Reseta o tempo para copiar os movimentos
                    playerController.copyThisMovement = new byte[4]; // Limpa a lista de movimentos copiados
                    playerController.listCopyThisMovement.Clear(); // Limpa a lista de movimentos copiados

                    playerController.dontMove = false; // Reseta o booleano
                    playerController.move = false; // Reseta o booleano
                    playerController.moveu = false; // Reseta o booleano

                    Debug.Log("APAGOU TUDO DE TODOS");
                }
            }
        }
    }

    private void AddEqualScores(sbyte mission)
    {
        foreach (var player in runner.ActivePlayers)
        {
            Debug.Log("Checando player: " + player);

            var networkObject = runner.GetPlayerObject(player); //Percorre os objetos de rede ativos (Players)

            Debug.Log("NETWORKOBJECT VAZIO??: " + networkObject);
            if (networkObject != null) //Verifica se o objeto de rede não é nulo
            {
                PlayerChecker playerChecker = networkObject.GetComponent<PlayerChecker>(); //Pega o script PlayerController do objeto de rede

                if (playerChecker != null)
                {

                    Debug.Log(playerChecker + " Verificando se TODOS os player");

                    PlayerRef playerRef = playerChecker.Object.InputAuthority;

                    switch (mission)
                    {
                        case 0:
                            if (playerChecker.MissionProjectile(false)) //falso é igual a não colidir
                            {
                                Debug.Log("Player " + playerChecker + " completed the mission!");
                                playerScores.Add(playerRef, 600);
                            }
                            break;
                        case 3:
                            if (playerChecker.MissionDontMove(true))
                            {
                                Debug.Log("Player " + playerChecker + " completed the mission!");
                                playerScores.Add(playerRef, 600);
                            }
                            break;
                        case 4:
                            if (playerChecker.MissionMove(true))
                            {
                                Debug.Log("Player " + playerChecker + " completed the mission!");
                                playerScores.Add(playerRef, 600);
                            }
                            break;
                        case 5:
                            if (playerChecker.MissionBomb(false))
                            {
                                Debug.Log("Player " + playerChecker + " completed the mission!");
                                playerScores.Add(playerRef, 600);
                            }
                            break;
                        case 6:
                            if (playerChecker.MissionStaySquare(true))
                            {
                                Debug.Log("Player " + playerChecker + " completed the mission!");
                                playerScores.Add(playerRef, 600);
                            }
                            break;
                    }

                }
            }
        }

        foreach (var pair in playerScores) // Envia a pontuação para o GameManager
        {
            gameManager.RPC_AddScore(pair.Key, pair.Value);
        }
        playerScores.Clear(); // Limpa a lista após enviar as pontuações
    }


    public void RemovePlayerFromList(PlayerChecker playerChecker)
    {
        if (playerCheckers.Contains(playerChecker))
        {
            playerCheckers.Remove(playerChecker);
            Debug.Log("Removendo player " + playerChecker + " da lista de jogadores.");
        }
    }


    // Missões onde os players recebem pontuações diferentes dependendo da ordem que completaram
    public void NotifyMissionCompleted(PlayerChecker player)
    {
        //if (!Runner.IsServer) return; // Só o Host pode registrar!

        if (!playerScores.ContainsKey(player.Object.InputAuthority))
        {
            Debug.Log("Player " + player + " foi adicionado à lista na posição " + playersSequence.Count);


            playersSequence.Add(new Tuple<PlayerRef, int>(player.Object.InputAuthority, 1));
        }
    }


    public void AdicionarPontucaoEmSequencia()
    {
        Debug.Log("Adicionando pontuação em sequência para os jogadores.");

        for (int i = 0; i < playersSequence.Count; i++)
        {
            var player = playersSequence[i].Item1; // PlayerRef
            Debug.Log($"Adicionando pontuação para o player {player}");

            int pontuacao = 0;
            switch (i)
            {
                case 0: pontuacao = 1200; break;
                case 1: pontuacao = 1000; break;
                case 2: pontuacao = 800; break;
                case 3: pontuacao = 600; break;
                default: pontuacao = 600; break;
            }

            if (!playerScores.ContainsKey(player))
                playerScores.Add(player, pontuacao);
        }
        playersSequence.Clear(); // Limpa a lista após adicionar as pontuações


        foreach (var pair in playerScores) // Envia a pontuação para o GameManager
        {
            gameManager.RPC_AddScore(pair.Key, pair.Value);
        } 

        playerScores.Clear(); // Limpa a lista após enviar as pontuações
    }
}
