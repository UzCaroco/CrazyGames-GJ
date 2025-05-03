using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerCopyMovementController;

public class NetworkInputHandler : MonoBehaviour, INetworkInput
{

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        sbyte move = 0;

        if (Input.GetKeyDown(KeyCode.W)) move = 1;
        else if (Input.GetKeyDown(KeyCode.S)) move = 2;
        else if (Input.GetKeyDown(KeyCode.A)) move = 3;
        else if (Input.GetKeyDown(KeyCode.D)) move = 4;

        input.Set(new NetworkInputData { Movement = move });
    }
}
