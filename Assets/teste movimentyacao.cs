using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class testemovimentyacao : NetworkBehaviour
{
    Vector2 moveInput;
    //PlayerInput playerInput;

    public sbyte speed = 5;
    Collider2D col;
    public LayerMask collisionLayers; // Define no Inspector os layers que vai colidir

    //PlayerStateMachine stateMachine;

    //public IdleState idleState;
    //public WalkState walkState;
    //public PushState pushState;

    private void Awake()
    {
        //playerInput = new PlayerInput();
        col = GetComponent<Collider2D>();

        //stateMachine = new PlayerStateMachine();
        //idleState = new IdleState(stateMachine, this);
        //walkState = new WalkState(stateMachine, this);
        //pushState = new PushState(stateMachine, this);
    }

    private void Start()
    {
        //stateMachine.Inicialize(idleState);
    }

    private void OnEnable()
    {
        //playerInput.Player.Enable();
    }

    private void OnDisable()
    {
        //playerInput.Player.Disable();
    }

    public override void FixedUpdateNetwork()
    {
        //UnityEngine.InputSystem.InputSystem.Update();

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 input = new Vector2(x, y).normalized;
        //moveInput = playerInput.Player.Move.ReadValue<Vector2>().normalized;
        Vector2 moveDelta = input * speed * Runner.DeltaTime;

        //stateMachine.Update();


        if (moveDelta != Vector2.zero)
        {
            RaycastHit2D hit = Physics2D.BoxCast(transform.position, col.bounds.size, 0f, moveDelta.normalized, moveDelta.magnitude, collisionLayers);

            if (!hit)
            {
                transform.Translate(moveDelta * speed * Runner.DeltaTime);
            }
            else
            {
                // Opcional: pode fazer algo quando colidir, tipo logar
                Debug.Log("Bateu em: " + hit.collider.name);
            }
        }

        /*if (playerInput.Player.Jump.triggered)
        {
            //rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        }*/

    }
}
