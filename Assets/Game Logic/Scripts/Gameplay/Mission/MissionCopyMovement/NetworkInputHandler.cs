using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerCopyMovementController;

public class NetworkInputHandler : MonoBehaviour, INetworkInput
{
    void Start()
    {
        Debug.Log("Input handler está ativo e no GameObject: " + gameObject.name);
    }
    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        Debug.Log("OnInput está sendo chamado!");

        byte move = 0;

        if (Input.GetKeyDown(KeyCode.W)) move = 1;
        else if (Input.GetKeyDown(KeyCode.D)) move = 2;
        else if (Input.GetKeyDown(KeyCode.S)) move = 3;
        else if (Input.GetKeyDown(KeyCode.A)) move = 4;
        
        print("I am Getting those INputs: " + move);

        input.Set(new NetworkInputData { Movement = move });
    }
}
public struct NetworkInputData : INetworkInput
{
    public byte Movement;

}
