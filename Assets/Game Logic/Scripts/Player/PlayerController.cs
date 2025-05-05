using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using CrazyGames;
using System.Linq;
using UnityEngine.SceneManagement;

public class PlayerController : NetworkBehaviour
{
    string currentM;

    bool ifFacingRight = true;

    SpriteRenderer spriteRenderer;
    Vector2 CurrentPos;
    Camera camPlayer;
    SunController SunController;
    //public GameObject cameraRoot;
    Animator animPlayer;
    private Vector2 moveInput;
    public LayerMask collisionLayers; // Define no Inspector os layers que vai colidir

    PlayerChecker playerChecker;
    private PlayerInputs playerInput;
    private Collider2D col;
    private GameManager gameManager;

    public sbyte speed = 5;

    [HideInInspector] public bool timeToCopyTheMovements = false, dontMove = false, move = false, moveu = false;
    [HideInInspector] public byte[] copyThisMovement = new byte[4];

    public List<sbyte> listCopyThisMovement = new List<sbyte>();
    bool wasPressingX = false;
    bool wasPressingY = false;


    [Header("Missions Bool")]

    



    public bool missionProjectile = true, missionCollectCoin = true, missionCopyMoviment = true, missionDontMove = true, missionMove = true, missionPushRival = true, missionBomb = true, missionStaySquare = true;

    

    private void Awake()
    {
        CrazySDK.Init(() => { Debug.Log("CrazySDK inicializado com sucesso!"); });

        playerInput = new PlayerInputs();
        col = GetComponent<Collider2D>();
        animPlayer = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void OnEnable()
    {
        
        playerInput.Player.Enable();

        PlayerChecker playerChecker = GetComponent<PlayerChecker>();
        Debug.Log("PlayerChecker: " + playerChecker);
        playerChecker.PegarPlayerControler();
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
        if (HasInputAuthority) // Só ativa a câmera do jogador local
        {
            Camera cam = GetComponentInChildren<Camera>(true); // true = busca em objetos inativos
            //Debug.Log("Camera: " + cam);
            camPlayer = GetComponentInChildren<Camera>(true);
            cam.enabled = true;
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
            animPlayer.SetBool("isWalking", true);

            if (moveDelta.x > 0)
            {
                if (!ifFacingRight)
                {
                    spriteRenderer.flipX = false;
                    ifFacingRight = true;
                }
            }
            else if (moveDelta.x < 0)
            {
                if (ifFacingRight)
                {
                    spriteRenderer.flipX = true;
                    ifFacingRight = false;
                }
            }

                if (!hit)
            {
                transform.Translate(moveDelta);
            }
            else
            {
                // Opcional: pode fazer algo quando colidir, tipo logar
            }
        }
        else
        {
            animPlayer.SetBool("isWalking", false);
        }

        if (SunController != null && !camPlayer.enabled)
        {
            GameManager gM = FindObjectOfType<GameManager>();
            if (HasInputAuthority && currentM != gM.GetCurrentMission())
            {
                transform.position = new Vector2(0, 0);
                spriteRenderer.color = new Color(255, 255, 255, 1);
                camPlayer.enabled = true;
                SunController = null;

                Debug.Log("FInalizou respawnando: ");
            }
        }

        if (timeToCopyTheMovements)
        {
            CopyMoviment();
        }



        if (dontMove)
        {
            if (moveInput != Vector2.zero && !moveu)
            {
                moveu = true; // Se o jogador se mover, a missão de não se mover falha
                missionDontMove = false; // Se o jogador se mover, a missão de não se mover falha
            }
            
            if (moveInput == Vector2.zero && !moveu)
            {
                missionDontMove = true; // Se o jogador não se mover, a missão de não se mover é completada
            }
        }

        if (move)
        {
            if (moveInput != Vector2.zero && !moveu)
            {
                moveu = true; // Se o jogador se mover, a missão de não se mover falha
                missionMove = true; // Se o jogador se mover, a missão de não se mover falha
                Debug.Log("Moveu: " + missionMove);
            }
        }

        if (playerInput.Player.Dash.triggered)
        {
            if (moveInput != Vector2.zero)
            {
                Vector2 dashDirection = moveInput.normalized;
                StartCoroutine(DashRoutine(dashDirection));
            }
        }

    }

    IEnumerator DashRoutine(Vector2 direction)
    {
        float dashDuration = 0.2f;
        float dashSpeed = 15f;
        float elapsed = 0f;

        while (elapsed < dashDuration)
        {
            transform.Translate(direction * dashSpeed * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null; // Executa por frame, sem atraso de FixedUpdate
        }
    }

    private void CopyMoviment()
    {
        
        float x = playerInput.Player.CopyMovimentX.ReadValue<float>();
        float y = playerInput.Player.CopyMovimentY.ReadValue<float>();

        if (listCopyThisMovement.Count < 4)
        {
            // Eixo X
            if (x != 0 && !wasPressingX)
            {
                wasPressingX = true;

                if (x > 0)
                    listCopyThisMovement.Add(2); // Direita
                else
                    listCopyThisMovement.Add(4); // Esquerda
            }
            else if (x == 0)
            {
                wasPressingX = false;
            }

            // Eixo Y
            if (y != 0 && !wasPressingY)
            {
                wasPressingY = true;

                if (y > 0)
                    listCopyThisMovement.Add(1); // Cima
                else
                    listCopyThisMovement.Add(3); // Baixo
            }
            else if (y == 0)
            {
                wasPressingY = false;
            }

            // Debug só se tiver movimento copiado
            if (listCopyThisMovement.Count > 0)
            {
                Debug.Log("Copiou o movimento: " + string.Join(", ", listCopyThisMovement));
            }
        }

        else
        {
            if (listCopyThisMovement[0] == copyThisMovement[0] && listCopyThisMovement[1] == copyThisMovement[1] && listCopyThisMovement[2] == copyThisMovement[2] && listCopyThisMovement[3] == copyThisMovement[3])
            {
                Debug.Log("Copiou o movimento certo");

                playerChecker = GetComponent<PlayerChecker>();
                Debug.Log("PlayerChecker ESTÁ VAZIO???: " + playerChecker);
                GameChecker gameChecker = FindObjectOfType<GameChecker>();

                gameChecker.NotifyMissionCompleted(playerChecker); // Envia notificação de missão completa para o GameChecker

                timeToCopyTheMovements = false; // Para de copiar o movimento
                listCopyThisMovement.Clear(); // Limpa a lista de movimentos copiados
                copyThisMovement = new byte[4]; // Limpa a lista de movimentos a serem copiados
            }
            else
            {
                playerChecker = GetComponent<PlayerChecker>();
                Debug.Log("PlayerChecker ESTÁ VAZIO???: " + playerChecker);
                GameChecker gameChecker = FindObjectOfType<GameChecker>();

                gameChecker.NotifyMissionCompleted(playerChecker); // Envia notificação de missão completa para o GameChecker

                timeToCopyTheMovements = false; // Para de copiar o movimento
                listCopyThisMovement.Clear(); // Limpa a lista de movimentos copiados
                copyThisMovement = new byte[4]; // Limpa a lista de movimentos a serem copiados
            }
        }
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {        
        //CurrentPos = new Vector2(transform.position.x, transform.position.y);
        
        if (CompareTag("Bomb") || collision.CompareTag("Projectile"))
        {
            

        }

        if (collision.CompareTag("Coin"))
        {
            Debug.Log("MOEDAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            playerChecker = GetComponent<PlayerChecker>();
            Debug.Log("PlayerChecker ESTÁ VAZIO???: " + playerChecker);

            GameChecker gameChecker = FindObjectOfType<GameChecker>();
            gameChecker.NotifyMissionCompleted(playerChecker); // Envia notificação de missão completa para o GameChecker

            missionCollectCoin = true;

            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Projectile"))
        {
            spriteRenderer.color = new Color(255, 255, 255, 0);

            missionProjectile = true;
            camPlayer.enabled = false;

            Debug.Log("PROJETIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIL");

        }
        else if (collision.CompareTag("Bomb"))
        {
            spriteRenderer.color = new Color(255, 255, 255, 0);

            camPlayer.enabled = false;
            missionBomb = true;

            Debug.Log("BOMBAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        }
        else if (collision.CompareTag("Square"))
        {
            missionStaySquare = true;
            Debug.Log("QUADRADOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO");
        }
        else if (collision.CompareTag("Player") && playerInput.Player.PushRival.triggered)
        {
            missionPushRival = true;
        }

        GameManager gM = FindObjectOfType<GameManager>();
        currentM = gM.GetCurrentMission();
        SunController = FindObjectOfType<SunController>();
    }




    public void Despawn()
    {
        // Só executa se tivermos autoridade sobre o objeto
        if (Object.HasInputAuthority)
        {
            // Avisa o GameManager (se necessário)
            if (gameManager != null)
            {
                gameManager.OnPlayerLeft(Object.InputAuthority, this);
            }

            // Despawna o objeto na rede
            Runner.Despawn(Object);

            SceneManager.LoadScene("Menu");
        }
    }

}


