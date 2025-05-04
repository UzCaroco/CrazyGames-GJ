using CrazyGames;
using Fusion;
using System.Reflection;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    RandomSkinAnimator randomSkin;


    [SerializeField] private GameObject sunControllerPrefab;
    NetworkObject gameManager;
    
    Animator anim;
    AnimatorOverrideController[] animatorOverrideController;
    int idex;

    public GameObject playerPrefab;
    [SerializeField] private NetworkRunner runner;
    void Start()
    {
        randomSkin = FindObjectOfType<RandomSkinAnimator>();
    }

    public void PlayerJoined(PlayerRef player)
    {
        // Só o peer com StateAuthority vai instanciar

        if (player == Runner.LocalPlayer) // ou Runner.IsServer se tiver usando Server/Client
        {
            //randomSkin.PlayerIsSpawned();
            //anim = animatorOverrideController[randomSkin.SetNumRandom()];
            /*print("TA O QUE" + gameManager);


            if (gameManager == null)
            {
                Debug.Log("entreiiiia aqui no sei la");
                gameManager = runner.Spawn(sunControllerPrefab, Vector3.zero, Quaternion.identity, inputAuthority: null);
            }

            print("TA O QUE"+gameManager);*/

            NetworkObject playerObj = Runner.Spawn(playerPrefab, Vector3.zero, Quaternion.identity, inputAuthority: player);
            Runner.SetPlayerObject(player, playerObj);
            
            anim = playerObj.GetComponent<Animator>();
        }
    }
}
