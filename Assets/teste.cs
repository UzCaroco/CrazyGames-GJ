using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class teste : SimulationBehaviour, IPlayerJoined
{
    public GameObject playerPrefab;

    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            Runner.Spawn(playerPrefab, Vector3.zero, Quaternion.identity);
        }
    }
}

