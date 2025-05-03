using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class GameChecker : NetworkBehaviour
{
    NetworkRunner runner;
    [SerializeField] GameManager gameManager;

     List<PlayerChecker> playerCheckers = new List<PlayerChecker>();

    Dictionary<PlayerRef, int> playersSequence = new Dictionary<PlayerRef, int>();
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

        Debug.Log("Adicionando player " + playerChecker + " à lista de jogadores.");
        if (!playerCheckers.Contains(playerChecker))
        {
            Debug.Log("Adicionando player " + playerChecker + " à lista de jogadores.");
            playerCheckers.Add(playerChecker);
        }
    }

    //SOMENTE NO FINAL quando acabar a missão, verifica os jogadores completaram
    public void CheckPlayersInTheEndOfMission(sbyte mission)
    {
        Debug.Log("Verificando os jogadores no final da missão: " + mission);
        Debug.Log("Total de jogadores na lista: " + playerCheckers.Count);

        if (mission == 0 || mission == 3 || mission == 4 || mission == 6 || mission == 7)
        {
            AddEqualScores(mission); // Adiciona pontuação igual para todos os jogadores que completaram a missão
        }
        else
        {
            AdicionarPontucaoEmSequencia(); // Adiciona pontuação em sequência para os jogadores que completaram a missão
        }
    }

    private void AddEqualScores(sbyte mission)
    {
        Debug.Log($"Verificando os jogadores {playerCheckers[0]} e {playerCheckers[1]} na missão 7");

        foreach (PlayerChecker playerChecker in playerCheckers)
        {
            if (playerChecker.playerController != null)
            {
                Debug.Log(playerChecker + " Verificando se TODOS os player");

                PlayerRef playerRef = playerChecker.Object.InputAuthority;

                switch (mission)
                {
                    case 0:
                        if (playerChecker.MissionProjectile(true))
                        {
                            playerScores.Add(playerRef, 600);
                        }
                        break;
                    case 3:
                        if (playerChecker.MissionDontMove(true))
                        {
                            playerScores.Add(playerRef, 600);
                        }
                        break;
                    case 4:
                        if (playerChecker.MissionMove(true))
                        {
                            playerScores.Add(playerRef, 600);
                        }
                        break;
                    case 6:
                        if (playerChecker.MissionBomb(true))
                        {
                            playerScores.Add(playerRef, 600);
                        }
                        break;
                    case 7:
                        if (playerChecker.MissionStaySquare(true))
                        {
                            playerScores.Add(playerRef, 600);
                        }
                        break;
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


            playersSequence.Add(player.Object.InputAuthority, 1);
        }
    }


    public void AdicionarPontucaoEmSequencia()
    {
        Debug.Log("Adicionando pontuação em sequência para os jogadores.");

        foreach (KeyValuePair<PlayerRef, int> player in playersSequence)
        {
            if (player.Value == 1)
            {
                playerScores.Add(player.Key, 1200);
            }
            else if (player.Value == 2)
            {
                playerScores.Add(player.Key, 1000);
            }
            else if (player.Value == 3)
            {
                playerScores.Add(player.Key, 800);
            }
            else if (player.Value > 3)
            {
                playerScores.Add(player.Key, 600);
            }
        }
        playersSequence.Clear(); // Limpa a lista após adicionar as pontuações


        foreach (var pair in playerScores) // Envia a pontuação para o GameManager
        {
            gameManager.RPC_AddScore(pair.Key, pair.Value);
        } 

        playerScores.Clear(); // Limpa a lista após enviar as pontuações
    }
}
