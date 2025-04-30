using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using TMPro;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    // Score por jogador
    private Dictionary<PlayerRef, int> playerScores = new Dictionary<PlayerRef, int>();

    [SerializeField] TextMeshProUGUI first, second, third;

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
            playerScores[player] = 0;
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

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_AddScore(PlayerRef player, int amount)
    {
        if (!HasStateAuthority) return;

        if (!playerScores.ContainsKey(player))
            playerScores[player] = 0;

        playerScores[player] += amount;
        Debug.Log($"[GameManager] Score do player {player.PlayerId} é agora {playerScores[player]}");

        OnScoreChanged?.Invoke(player, playerScores[player]);
    }

    // Para acessar scores localmente (apenas para exibição)
    public int GetScore(PlayerRef player)
    {
        return playerScores.TryGetValue(player, out int score) ? score : 0;
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
}