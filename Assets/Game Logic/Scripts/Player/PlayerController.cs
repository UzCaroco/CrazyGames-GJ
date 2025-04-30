using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using CrazyGames;

public class PlayerController : NetworkBehaviour
{
    Vector2 moveInput;
    PlayerInputs playerInput;

    public sbyte speed = 5;
    Collider2D col;
    public LayerMask collisionLayers; // Define no Inspector os layers que vai colidir

    GameManager gameManager;

    private void Awake()
    {
        CrazySDK.Init(() => { Debug.Log("CrazySDK inicializado com sucesso!"); });

        playerInput = new PlayerInputs();
        col = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        playerInput.Player.Enable();
    }

    private void OnDisable()
    {
        playerInput.Player.Disable();
    }
    public override void Spawned()
    {
        base.Spawned();
        gameManager = FindAnyObjectByType<GameManager>();

        if (gameManager != null)
        {
            gameManager.OnPlayerJoined(Object.InputAuthority, this);
        }
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        base.Despawned(runner, hasState);

        if (gameManager != null)
        {
            gameManager.OnPlayerLeft(Object.InputAuthority, this);
        }
    }
    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();
        UnityEngine.InputSystem.InputSystem.Update();

        moveInput = playerInput.Player.Move.ReadValue<Vector2>().normalized;
        Vector2 moveDelta = moveInput * speed * Runner.DeltaTime;


        if (moveDelta != Vector2.zero)
        {
            RaycastHit2D hit = Physics2D.BoxCast(transform.position, col.bounds.size, 0f, moveDelta.normalized, moveDelta.magnitude, collisionLayers);

            if (!hit)
            {
                transform.Translate(moveDelta);
            }
            else
            {
                // Opcional: pode fazer algo quando colidir, tipo logar
                Debug.Log("Bateu em: " + hit.collider.name);
            }
        }

    }
}


