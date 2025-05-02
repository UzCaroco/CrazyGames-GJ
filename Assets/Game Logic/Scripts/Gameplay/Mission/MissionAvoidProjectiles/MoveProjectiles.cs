using System.Collections;
using System.Collections.Generic;
using CrazyGames;
using Fusion;
using UnityEngine;

public class MoveProjectiles : NetworkBehaviour
{
    private List<PlayerManager> playersCollided = new List<PlayerManager>();
    private NetworkRunner runner;

    [SerializeField] Vector2 direction;
    [SerializeField] private Rigidbody2D rbProjectiles;
    [SerializeField] private int speedObjects;

    private bool touchPlayer;

    private void Start()
    {
        runner = FindObjectOfType<NetworkRunner>();
    }
    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        Debug.Log($"Tem autoridade? {Object.HasStateAuthority}");
        if (Object.HasStateAuthority)
        {
            transform.Translate(direction * speedObjects * Runner.DeltaTime);
            if (transform.position.x > 30 || transform.position.x < -30 || transform.position.y > 30 || transform.position.y < -30)
            {
                Runner.Despawn(Object);
            }
        }
    }
    public void GetDirAndIndex(Vector2[] directions, sbyte index)
    {
        GetDirections(directions, index);
    }

    private void GetDirections(Vector2[] directions, sbyte index)
    {
        for (int i = 0; i < directions.Length; i ++)
        {
            if (i == index)
            {
                direction = directions[i];
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto colidido tem um NetworkObject
        NetworkObject netObj = collision.GetComponent<NetworkObject>();
        if (netObj == null) return;

        // Verifica se é um player da lista de jogadores ativos
        if (!runner.IsPlayer.Equals(netObj.InputAuthority)) return;

        // Tenta pegar o PlayerManager
        PlayerManager playerManager = netObj.GetComponent<PlayerManager>();
        if (playerManager != null && !playersCollided.Contains(playerManager))
        {
            playersCollided.Add(playerManager);
            playerManager.SetCollision(true); //mudar para script de controle ////////////////////////
        }
    }
}
