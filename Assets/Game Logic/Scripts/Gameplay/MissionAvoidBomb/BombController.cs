using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class BombController : NetworkBehaviour
{
    private List<PlayerManager> playersCollided = new List<PlayerManager>();
    [SerializeField] private NetworkRunner runner;
    Animator animBomb;

    bool isCountDown, isExplode =  false, hasCollision = false;
    private float timeForExplosion;

    // Start is called before the first frame update
    void Start()
    {
        animBomb = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public bool GetIsExplode()
    {
        return isExplode;
    }
    
    void TimeExplode()
    {
        if (isCountDown && timeForExplosion < 5)
        {
            timeForExplosion += Time.deltaTime;
            Debug.Log("eMcOUNTdOWN");
            if (timeForExplosion >= 5)
            {
                isCountDown = false;
                ExplodeTheBomb();
            }
        }
    }
    void ExplodeTheBomb()
    {
        //animBomb.SetTrigger("Explode");

        foreach (var playerManager in playersCollided)
        {
            if (playerManager != null)
            {
                if (playerManager.GetCollision()) // Usando o método GetCollision()
                {
                    Debug.Log($"Player {playerManager.name} colidiu com a bomba!");
                    
                    
                    // Aqui você pode causar dano, explodir, etc
                }
            }
        }

        isExplode = true;
        isCountDown = false;
    }

    

    void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var player in runner.ActivePlayers)
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
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        foreach (var player in runner.ActivePlayers)
        {
            NetworkObject playerObject = runner.GetPlayerObject(player);
            if (playerObject != null && collision.gameObject == playerObject.gameObject)
            {
                PlayerManager playerManager = playerObject.GetComponent<PlayerManager>();
                if (playerManager != null && playersCollided.Contains(playerManager))
                {
                    playersCollided.Remove(playerManager);
                }
            }
        }
    }
}
