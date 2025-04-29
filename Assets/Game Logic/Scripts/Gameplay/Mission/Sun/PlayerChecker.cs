using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerChecker : NetworkBehaviour
{
    [SerializeField] NetworkRunner runner;

    List<PlayerController> playerControllers = new List<PlayerController>();

    /*private void Start()
    {
        foreach (var player in runner.ActivePlayers)
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
        }
    }*/

    public void AdicionarPlayerALista(PlayerController playerController)
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

    public void RemovePlayerFromList(PlayerController playerController)
    {
        if (playerControllers.Contains(playerController))
        {
            playerControllers.Remove(playerController);
        }
    }
}
