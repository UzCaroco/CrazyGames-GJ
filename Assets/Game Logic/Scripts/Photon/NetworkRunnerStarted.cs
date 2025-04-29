using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkRunnerStarted : MonoBehaviour
{
    public NetworkRunner runner;

    private async void Start()
    {
        if (runner == null)
        {
            runner = GetComponent<NetworkRunner>();
        }

        await runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Host, // Ou Client, Server, etc.
            SessionName = "SalaTeste", // Nome da sala
            //Scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }
}
