using CrazyGames;
using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunSpawnner : SimulationBehaviour, ISceneLoadDone
{
    public GameObject sunControllerPrefab;
    [SerializeField] private NetworkRunner runner;

    void ISceneLoadDone.SceneLoadDone(in SceneLoadDoneArgs sceneInfo)
    {
        if (runner != null && runner.IsServer)
        {
            StartGame();
        }

    }
    void StartGame()
    {
        if (runner.IsServer)
        {
        }
    }

   
}
