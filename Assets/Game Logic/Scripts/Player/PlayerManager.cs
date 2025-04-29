using Fusion;
using UnityEngine;

public class PlayerManager : NetworkBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private int playerID;
    [SerializeField] private NetworkRunner runner;
    [SerializeField] private bool hasCollision;
    [SerializeField] private Vector2 positionPLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var player in runner.ActivePlayers)
        {
            NetworkObject playerObject = runner.GetPlayerObject(player);
            if (playerObject != null)
            {
                var playerController = playerObject.GetComponent<PlayerController>();
                if (playerController != null)
                {
                    //if (playerController)
                    //playerController.ReadInputs(); // ou outro método para ler inputs
                }
            }
        }
    }

    public void SetCollision(bool value)
    {
        hasCollision = value;
    }
    public bool GetCollision()
    {
        return hasCollision;
    }

}
