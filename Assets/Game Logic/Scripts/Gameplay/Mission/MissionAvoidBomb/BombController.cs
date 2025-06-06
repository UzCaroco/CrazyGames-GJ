using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class BombController : NetworkBehaviour
{
    MissionStayAwayBomb awayBomb;

    private List<PlayerManager> playersCollided = new List<PlayerManager>();
    [SerializeField] private NetworkRunner runner;
    Animator animBomb;
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite explosionSprite;

    bool isCountDown = true, isExplode =  false, hasCollision = false;
    [SerializeField] private float timeForExplosion;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animBomb = GetComponent<Animator>();
        runner = FindObjectOfType<NetworkRunner>();
        awayBomb = GetComponentInParent<MissionStayAwayBomb>();
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
            if (timeForExplosion >= 5)
            {
                isCountDown = false;
                StartCoroutine(Expansion());
                ForExplodeTheBomb();
            }
        }
    }

    IEnumerator Expansion()
    {
        spriteRenderer.sprite = explosionSprite;
        CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();

        for (int i = 0; i < 20; i++)
        {
            transform.localScale += new Vector3(0.1f, 0.1f, 0);
            circleCollider.radius += 0.0008f;
            yield return new WaitForSeconds(0.05f);
        }

        
        
        yield return null;
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
            awayBomb.Conclusion(1);
            runner.Despawn(Object);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        /*
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
        }*/

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
        /*
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
        }*/

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
