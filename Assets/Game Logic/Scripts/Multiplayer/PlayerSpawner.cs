using CrazyGames;
using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject playerPrefab;
    [SerializeField] private NetworkRunner runner;

    public void PlayerJoined(PlayerRef player)
    {
        // Só o peer com StateAuthority vai instanciar

        if (player == Runner.LocalPlayer) // ou Runner.IsServer se tiver usando Server/Client
        {
            NetworkObject playerObj = Runner.Spawn(playerPrefab, Vector3.zero, Quaternion.identity, inputAuthority: player);
            Runner.SetPlayerObject(player, playerObj);
        }
    }
}
