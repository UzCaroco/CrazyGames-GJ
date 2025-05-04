using CrazyGames;
using Fusion;
using System.Reflection;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{

    [SerializeField] private GameObject sunControllerPrefab;
    NetworkObject gameManager;
    
    Animator anim;
    AnimatorOverrideController[] animatorOverrideController;

    public GameObject playerPrefab;
    [SerializeField] private NetworkRunner runner;



    public void PlayerJoined(PlayerRef player)
    {
        // Só o peer com StateAuthority vai instanciar

        if (player == Runner.LocalPlayer) // ou Runner.IsServer se tiver usando Server/Client
        {
            if (gameManager == null)
            {
                Debug.Log("entreiiiia aqui no sei la");
                gameManager = runner.Spawn(sunControllerPrefab, Vector3.zero, Quaternion.identity, inputAuthority: null);
            }

            NetworkObject playerObj = Runner.Spawn(playerPrefab, Vector3.zero, Quaternion.identity, inputAuthority: player);
            Runner.SetPlayerObject(player, playerObj);
            
            anim = playerObj.GetComponent<Animator>();
        }
    }
}
