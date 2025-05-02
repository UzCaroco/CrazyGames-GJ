using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class GameChecker : NetworkBehaviour
{
    NetworkRunner runner;
    [SerializeField] GameManager gameManager;

     List<PlayerChecker> playerCheckers = new List<PlayerChecker>();

     List<PlayerChecker> playersSequence = new List<PlayerChecker>();

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

        Debug.Log("Adicionando player " + playerChecker + " à lista de jogadores.");
        if (!playerCheckers.Contains(playerChecker))
        {
            Debug.Log("Adicionando player " + playerChecker + " à lista de jogadores.");
            playerCheckers.Add(playerChecker);
        }
    }

    public void CheckPlayersInTheEndOfMission(sbyte mission)
    {
        Debug.Log("Verificando os jogadores no final da missão: " + mission);
        Debug.Log("Total de jogadores na lista: " + playerCheckers.Count);
        switch (mission)
        {
            case 0:
                Debug.Log($"Verificando os jogadores {playerCheckers[0]} e {playerCheckers[1]} na missão 0");
                foreach (PlayerChecker playerChecker in playerCheckers)
                {
                    if (playerChecker.playerController != null)
                    {
                        Debug.Log(playerChecker + " Verificando se TODOS os player");
                        if (playerChecker.MissionProjectile(true))
                        {
                            Debug.Log("Player " + playerChecker + " completed the mission!");
                        }
                        else
                        {
                            Debug.Log("Player " + playerChecker + " failed the mission!");
                        }
                    }
                }
                break;
            case 3:
                Debug.Log($"Verificando os jogadores {playerCheckers[0]} e {playerCheckers[1]} na missão 3");
                foreach (var playerChecker in playerCheckers)
                {
                    if (playerChecker.playerController != null)
                    {
                        Debug.Log(playerChecker + " Verificando se TODOS os player");
                        if (playerChecker.MissionDontMove(true))
                        {
                            Debug.Log("Player " + playerChecker + " completed the mission!");
                        }
                        else
                        {
                            Debug.Log("Player " + playerChecker + " failed the mission!");
                        }
                    }
                }
                break;
            case 4:
                Debug.Log($"Verificando os jogadores {playerCheckers[0]} e {playerCheckers[1]} na missão 4");
                foreach (var playerChecker in playerCheckers)
                {
                    if (playerChecker.playerController != null)
                    {
                        Debug.Log(playerChecker + " Verificando se TODOS os player");
                        if (playerChecker.MissionMove(true))
                        {
                            Debug.Log("Player " + playerChecker + " completed the mission!");
                        }
                        else
                        {
                            Debug.Log("Player " + playerChecker + " failed the mission!");
                        }
                    }
                }
                break;
            case 6:
                Debug.Log($"Verificando os jogadores {playerCheckers[0]} e {playerCheckers[1]} na missão 6");
                foreach (var playerChecker in playerCheckers)
                {
                    if (playerChecker.playerController != null)
                    {
                        Debug.Log(playerChecker + " Verificando se TODOS os player");
                        if (playerChecker.MissionBomb(true))
                        {
                            Debug.Log("Player " + playerChecker + " completed the mission!");
                        }
                        else
                        {
                            Debug.Log("Player " + playerChecker + " failed the mission!");
                        }
                    }
                }
                break;
            case 7:
                Debug.Log($"Verificando os jogadores {playerCheckers[0]} e {playerCheckers[1]} na missão 7");
                foreach (var playerChecker in playerCheckers)
                {
                    if (playerChecker.playerController != null)
                    {
                        Debug.Log(playerChecker + " Verificando se TODOS os player");
                        if (playerChecker.MissionStaySquare(true))
                        {
                            Debug.Log("Player " + playerChecker + " completed the mission!");
                        }
                        else
                        {
                            Debug.Log("Player " + playerChecker + " failed the mission!");
                        }
                    }
                }
                break;
        }
    }


    public void RemovePlayerFromList(PlayerChecker playerChecker)
    {
        if (playerCheckers.Contains(playerChecker))
        {
            playerCheckers.Remove(playerChecker);
        }
    }


    // Missões onde os players recebem pontuações diferentes dependendo da ordem que completaram

    public void NotifyMissionCompleted(PlayerChecker player)
    {
        if (!Runner.IsServer) return; // Só o Host pode registrar!

        if (!playersSequence.Contains(player))
        {
            playersSequence.Add(player);
            Debug.Log("Player " + player + " foi adicionado à lista na posição " + playersSequence.Count);




            //gameManager.AtualizarPontuacao(player, completedPlayers.Count);
        }
    }
}
