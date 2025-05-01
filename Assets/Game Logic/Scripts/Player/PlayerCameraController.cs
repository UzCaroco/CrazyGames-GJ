using Cinemachine;
using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : NetworkBehaviour
{
    [SerializeField] private GameObject cameraRoot;
    [SerializeField] private CinemachineVirtualCamera virtualCam;

    public override void Spawned()
    {
            cameraRoot.SetActive(true);
            virtualCam.Follow = this.transform;
            virtualCam.LookAt = this.transform;
       
    }
}
