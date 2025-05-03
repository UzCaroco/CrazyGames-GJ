using CrazyGames;
using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject playerPrefab;
    [SerializeField] private NetworkRunner runner;

    public void PlayerJoined(PlayerRef player)
    {
        // Todos os players (inclusive o host) serão instanciados pelo host
        NetworkObject playerObj = Runner.Spawn(playerPrefab, Vector3.zero, Quaternion.identity, inputAuthority: player);

        runner.SetPlayerObject(player, playerObj); // Agora sim vai funcionar o GetPlayerObject
    }
}
