using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class GameChecker : NetworkBehaviour
{
    NetworkRunner runner;

    List<PlayerChecker> playerControllers = new List<PlayerChecker>();

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

    public void AdicionarPlayerALista(PlayerChecker playerController)
    {
        if (!playerControllers.Contains(playerController))
        {
            playerControllers.Add(playerController);
        }
    }

    public void CheckPlayersInTheEndOfMission(Missions missions)
    {
        foreach (var player in playerControllers)
        {

        }
    }

    public void RemovePlayerFromList(PlayerChecker playerController)
    {
        if (playerControllers.Contains(playerController))
        {
            playerControllers.Remove(playerController);
        }
    }
}
