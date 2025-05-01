using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class MoveProjectiles : NetworkBehaviour
{
    [SerializeField] Vector2 direction;
    [SerializeField] private Rigidbody2D rbProjectiles;
    private int speedObjects;

    private bool touchPlayer;

    // Start is called before the first frame update
    void Start()
    {
        //rbProjectiles.velocity = direction * speedObjects;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        Debug.Log($"Tem autoridade? {Object.HasStateAuthority}");
        if (Object.HasStateAuthority)
        {
            transform.Translate(direction * speedObjects * Runner.DeltaTime);
            if (transform.position.x > 10 || transform.position.x < -10 || transform.position.y > 10 || transform.position.y < -10)
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
}
