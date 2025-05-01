using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class MoveProjectiles : NetworkBehaviour
{
    Vector2 direction;
    [SerializeField] private Rigidbody2D rbProjectiles;
    private int speedObjects;

    private bool touchPlayer;

    // Start is called before the first frame update
    void Start()
    {
        rbProjectiles.velocity = direction * speedObjects;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetDirAndIndex(Vector2[] directions, int index)
    {
        GetDirections(directions, index);
    }

    private void GetDirections(Vector2[] directions, int index)
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
