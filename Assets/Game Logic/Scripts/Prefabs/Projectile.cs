using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class Projectile : NetworkBehaviour
{
    public Vector2 direction;

    [SerializeField] sbyte speed = 5;
    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        Debug.Log($"Tem autoridade? {Object.HasStateAuthority}");
        if (Object.HasStateAuthority)
        {
            transform.Translate(direction * speed * Runner.DeltaTime);
            if (transform.position.x > 10 || transform.position.x < -10 || transform.position.y > 10 || transform.position.y < -10)
            {
                Runner.Despawn(Object);
            }
        }
    }

    public void Init(Vector2 vector)
    {
        direction = vector;
    }
}
