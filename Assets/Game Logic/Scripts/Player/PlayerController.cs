using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using CrazyGames;
using Cinemachine;

public class PlayerController : NetworkBehaviour
{
    //public GameObject cameraRoot;
    [SerializeField] public CinemachineVirtualCamera virtualCam;

    private Vector2 moveInput;
    public LayerMask collisionLayers; // Define no Inspector os layers que vai colidir

    private PlayerInputs playerInput;
    private Collider2D col;
    private GameManager gameManager;

    public sbyte speed = 5;

    [Header("Missions Bool")]

    
    public NetworkBool missionProjectile, missionCollectCoin, missionCopyMoviment, missionDontMove, missionMove, missionPushRival, missionBomb, missionStaySquare;

    

    private void Awake()
    {
        CrazySDK.Init(() => { Debug.Log("CrazySDK inicializado com sucesso!"); });

        playerInput = new PlayerInputs();
        col = GetComponent<Collider2D>();

    }

    private void OnEnable()
    {
        
        playerInput.Player.Enable();

        /*Camera cam = FindAnyObjectByType<Camera>();
        if (cam != null)
        {
            cam.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Camera não encontrada!");
        }
        */
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
        if (HasInputAuthority || HasStateAuthority) // Só ativa a câmera do jogador local
        {
            print("entrei aqui");

            Camera cam = GetComponentInChildren<Camera>(true); // true = busca em objetos inativos
            Debug.Log("Camera: " + cam);
            cam.gameObject.SetActive(true);
            //cameraRoot.tag = "MainCamera"; // se quiser usar Camera.main

            virtualCam.Follow = this.transform;
            virtualCam.LookAt = this.transform;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Adicionou 1 ponto");

        if (collision.CompareTag("Coin"))
        {
            // Adiciona 1 ponto ao jogador
            gameManager.RPC_AddScore(Object.InputAuthority, 1);
            Debug.Log("Adicionou 1 ponto");
            Destroy(collision.gameObject);
        }
    }


}


