using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class BombController : NetworkBehaviour
{
    private List<PlayerManager> playersCollided = new List<PlayerManager>();
    [SerializeField] private NetworkRunner runner;
    Animator animBomb;

    bool isCountDown = true, isExplode =  false, hasCollision = false;
    [SerializeField] private float timeForExplosion;

    // Start is called before the first frame update
    void Start()
    {
        animBomb = GetComponent<Animator>();
        runner = FindObjectOfType<NetworkRunner>();
    }

    // Update is called once per frame
    void Update()
    {
        TimeExplode();
        
    }

    public bool GetIsExplode()
    {
        return isExplode;
    }
    
    void TimeExplode()
    {
        if (isCountDown && timeForExplosion < 5)
        {
            timeForExplosion += Time.unscaledDeltaTime;
            Debug.Log("eMcOUNTdOWN");
            if (timeForExplosion >= 5)
            {
                isCountDown = false;
                ForExplodeTheBomb();
            }
        }
    }
    void ForExplodeTheBomb()
    {
        //animBomb.SetTrigger("Explode");

        foreach (var playerManager in playersCollided)
        {
            if (playerManager != null)
            {
                if (playerManager.GetCollision()) // Usando o método GetCollision()
                {
                    Debug.Log($"Player {playerManager.name} colidiu com a bomba e Explodio!");
                    
                    
                    // Aqui você pode causar dano, explodir, etc
                }
                else
                {
                    Debug.Log("");
                }
            }
        }

        Invoke("Explode", 1f);

        isCountDown = false;
    }

    void Explode()
    {
        if (Object.HasStateAuthority)
        {
            runner.Despawn(Object);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
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
            playerManager.SetCollision(true);
        }

        /*foreach (var player in runner.ActivePlayers)
        {
            NetworkObject playerObject = runner.GetPlayerObject(player);
            if (playerObject != null && collision.gameObject == playerObject.gameObject)
            {
                PlayerManager playerManager = playerObject.GetComponent<PlayerManager>();
                if (playerManager != null && !playersCollided.Contains(playerManager))
                {
                    playersCollided.Add(playerManager);
                    playerObject.GetComponent<PlayerManager>().SetCollision(true);
                }
            }
        }*/
    }
    void OnTriggerExit2D(Collider2D collision)
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
            playerManager.SetCollision(false);
        }

        /*
        foreach (var player in runner.ActivePlayers)
        {
            NetworkObject playerObject = runner.GetPlayerObject(player);
            if (playerObject != null && collision.gameObject == playerObject.gameObject)
            {
                PlayerManager playerManager = playerObject.GetComponent<PlayerManager>();
                if (playerManager != null && playersCollided.Contains(playerManager))
                {
                    playersCollided.Remove(playerManager);
                    playerObject.GetComponent<PlayerManager>().SetCollision(false);
                }
            }
        }
        */
    }
}
