using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCopyMovementController : NetworkBehaviour, INetworkInput
{
    MissionCopyMovement copyMovement;
    [Networked] public NetworkArray<byte> Movements => default; // tamanho fixo de 4
    [Networked] public int MovementIndex { get; set; }

    public static List<PlayerCopyMovementController> AllPlayers = new();

    public override void Spawned()
    {
        if (Object.HasStateAuthority)
        {
            AllPlayers.Add(this);
            copyMovement = FindObjectOfType<MissionCopyMovement>();
        }
    }

    private void FixedUpdate()
    {
        if (copyMovement.isActiveAndEnabled)
        {
            print(" Is Active");
            if (GetInput(out NetworkInputData input))
            {
                if (input.Movement != 0 && MovementIndex < 4)
                {
                    Movements.Set(MovementIndex, input.Movement);
                    MovementIndex++;
                    print("Movent Index " + MovementIndex);
                    /*Movements[MovementIndex].Set(MovementIndex, input.Movement);
                    MovementIndex++;*/
                }
            }
        }
        else
        {
            MovementIndex = 0;
            print("Not Active");
        }
    }
}
