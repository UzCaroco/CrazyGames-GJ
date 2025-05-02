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

            if (networkObject != null) //Verifica se o objeto de rede n�o � nulo
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
        if (playerCheckers.Contains(playerChecker)) return; // Se o player j� estiver na lista, n�o adiciona novamente

        Debug.Log("Adicionando player " + playerChecker + " � lista de jogadores.");
        if (!playerCheckers.Contains(playerChecker))
        {
            Debug.Log("Adicionando player " + playerChecker + " � lista de jogadores.");
            playerCheckers.Add(playerChecker);
        }
    }

    public void CheckPlayersInTheEndOfMission(sbyte mission)
    {
        Debug.Log("Verificando os jogadores no final da miss�o: " + mission);
        Debug.Log("Total de jogadores na lista: " + playerCheckers.Count);
        switch (mission)
        {
            case 0:
                Debug.Log($"Verificando os jogadores {playerCheckers[0]} e {playerCheckers[1]} na miss�o 0");
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
                Debug.Log($"Verificando os jogadores {playerCheckers[0]} e {playerCheckers[1]} na miss�o 3");
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
                Debug.Log($"Verificando os jogadores {playerCheckers[0]} e {playerCheckers[1]} na miss�o 4");
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
                Debug.Log($"Verificando os jogadores {playerCheckers[0]} e {playerCheckers[1]} na miss�o 6");
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
                Debug.Log($"Verificando os jogadores {playerCheckers[0]} e {playerCheckers[1]} na miss�o 7");
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


    // Miss�es onde os players recebem pontua��es diferentes dependendo da ordem que completaram

    public void NotifyMissionCompleted(PlayerChecker player)
    {
        if (!Runner.IsServer) return; // S� o Host pode registrar!

        if (!playersSequence.Contains(player))
        {
            playersSequence.Add(player);
            Debug.Log("Player " + player + " foi adicionado � lista na posi��o " + playersSequence.Count);




            //gameManager.AtualizarPontuacao(player, completedPlayers.Count);
        }
    }
}
