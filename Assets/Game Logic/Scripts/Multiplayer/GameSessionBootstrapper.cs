using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class GameSessionBootstrapper : MonoBehaviour
{
    [SerializeField] private NetworkPrefabRef gameManagerPrefab;

    private NetworkRunner runner;

    private void Start()
    {
        runner = FindObjectOfType<NetworkRunner>();
        StartCoroutine(WaitAndSpawnGameManager());
    }

    private IEnumerator WaitAndSpawnGameManager()
    {
        while (runner == null || !runner.IsRunning || runner.LocalPlayer == null)
        {
            yield return null;
        }

        // Espera até o runner nos atribuir autoridade
        while (!runner.IsSharedModeMasterClient)
        {
            yield return null;
        }

        Debug.Log("[Bootstrapper] Este cliente tem autoridade no modo Shared. Spawnando GameManager.");

        if (FindObjectOfType<GameManager>() == null)
        {
            runner.Spawn(gameManagerPrefab, Vector3.zero, Quaternion.identity, null);
            Debug.Log("[Bootstrapper] GameManager instanciado com sucesso.");
        }
    }
}
