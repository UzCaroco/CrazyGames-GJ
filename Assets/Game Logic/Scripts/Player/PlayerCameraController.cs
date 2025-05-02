using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : NetworkBehaviour
{
    [SerializeField] private GameObject cameraRoot;
    

    public override void Spawned()
    {
            cameraRoot.SetActive(true);
       
    }
}
