using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using TMPro;
using UnityEngine;

public class GameManager : NetworkBehaviour, INetworkRunnerCallbacks
{
    // Score por jogador
    [Networked, Capacity(10)]
    private NetworkDictionary<PlayerRef, int> playerScores { get; } = default;



    // Eventos locais para UI
    public static event Action<PlayerRef, int> OnScoreChanged;

    public override void Spawned()
    {
        Debug.Log("[GameManager] Spawned no Network. StateAuthority: " + HasStateAuthority);
    }

    public override void FixedUpdateNetwork()
    {
        if (!HasStateAuthority) return;

        // Coloque aqui a lógica de partida (timer, fases, etc.)
    }

    public virtual void OnPlayerJoined(PlayerRef player, PlayerController playerController)
    {
        if (!HasStateAuthority) return;

        if (!playerScores.ContainsKey(player))
        {
            playerScores.Set(player, 0); // Usar Set() em vez de indexador
            Debug.Log($"[GameManager] Player {player.PlayerId} entrou e foi adicionado ao ranking.");
        }
    }

    public virtual void OnPlayerLeft(PlayerRef player, PlayerController playerController)
    {
        if (!HasStateAuthority) return;

        if (playerScores.ContainsKey(player))
        {
            playerScores.Remove(player);
            Debug.Log($"[GameManager] Player {player.PlayerId} saiu e foi removido do ranking.");
        }
    }

    //[Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_AddScore(PlayerRef player, int amount)
    {
        if (!HasStateAuthority) return;

        if (!playerScores.TryGet(player, out int currentScore))
            currentScore = 0;

        playerScores.Set(player, currentScore + amount);
        Debug.Log($"[GameManager] Score do player {player.PlayerId} é agora {currentScore + amount}");

        // Atualiza todos os clientes
        RPC_UpdateScores();
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    private void RPC_UpdateScores()
    {
        // Dispara evento para atualizar UIs
        UpdateAllUIs();
    }

    private void UpdateAllUIs()
    {
        var scoreUIs = FindObjectsOfType<ScoreUI>();
        foreach (var ui in scoreUIs)
        {
            ui.UpdateRankingUI();
        }
    }

    // Para acessar scores localmente (apenas para exibição)
    public int GetScore(PlayerRef player)
    {
        return playerScores.TryGet(player, out int score) ? score : 0;
    }

    // Ranking ordenado
    public List<(PlayerRef, int)> GetRankedList()
    {
        var list = new List<(PlayerRef, int)>();
        foreach (var kvp in playerScores)
        {
            list.Add((kvp.Key, kvp.Value));
        }
        list.Sort((a, b) => b.Item2.CompareTo(a.Item2)); // Descendente
        return list;
    }












    // Adicione essas novas variáveis
    [Networked] private string CurrentMission { get; set; }
    public static event Action<string> OnMissionChanged; // Novo evento para missões

    // Método para sortear nova missão (chamado pelo Host)
    public void GenerateNewMission(string textS, string textM)
    {
        if (!HasStateAuthority) return;

        /*
        // Exemplo simples de sorteio
        string[] missions = {
            "Colete 10 cristais!",
            "Derrote 5 inimigos!",
            "Sobreviva por 2 minutos!"
        };*/

        CurrentMission = textM;
        RPC_UpdateMission(CurrentMission);
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    private void RPC_UpdateMission(string newMission)
    {
        CurrentMission = newMission;
        OnMissionChanged?.Invoke(newMission);
        UpdateAllUIs(); // Reaproveita o método existente
    }

    // Método para pegar a missão atual
    public string GetCurrentMission()
    {
        return CurrentMission;
    }














    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        throw new NotImplementedException();
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        throw new NotImplementedException();
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        throw new NotImplementedException();
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        throw new NotImplementedException();
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        throw new NotImplementedException();
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
        throw new NotImplementedException();
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        throw new NotImplementedException();
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        throw new NotImplementedException();
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        throw new NotImplementedException();
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
        throw new NotImplementedException();
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
        throw new NotImplementedException();
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        throw new NotImplementedException();
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        throw new NotImplementedException();
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        throw new NotImplementedException();
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        throw new NotImplementedException();
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        throw new NotImplementedException();
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        throw new NotImplementedException();
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        throw new NotImplementedException();
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        throw new NotImplementedException();
    }
}