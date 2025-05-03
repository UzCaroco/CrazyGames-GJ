using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCopyMovementController : NetworkBehaviour
{
    [Networked] public NetworkArray<sbyte> Movements => default; // tamanho fixo de 4
    [Networked] public int MovementIndex { get; set; }

    public static List<PlayerCopyMovementController> AllPlayers = new();

    public override void Spawned()
    {
        if (Object.HasStateAuthority)
            AllPlayers.Add(this);
    }

    private void FixedUpdate()
    {
        if (GetInput(out NetworkInputData input))
        {
            if (input.Movement != 0 && MovementIndex < 4)
            {
                Movements.Set(MovementIndex, input.Movement);
                MovementIndex++;
            }
        }
    }

    public struct NetworkInputData : INetworkInput
    {
        public sbyte Movement; // 1 = cima, 2 = baixo, 3 = esquerda, 4 = direita
    }
}
