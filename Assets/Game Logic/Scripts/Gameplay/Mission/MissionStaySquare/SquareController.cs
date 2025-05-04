using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareController : NetworkBehaviour
{
    private List<PlayerController> playersCollided = new List<PlayerController>();
    private NetworkRunner runner;

    [SerializeField] bool isFinishMission;
    [SerializeField] bool missionComplete;

    // Start is called before the first frame update
    void Start()
    {
        runner = FindObjectOfType<NetworkRunner>();
        missionComplete = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFinishMission)
        {
            if (Object.HasStateAuthority)
            {
                runner.Despawn(Object);
            }
        }
    }

    public void SetFinishTask(bool valueFinish)
    {
        isFinishMission = valueFinish;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto colidido tem um NetworkObject
        NetworkObject netObj = collision.GetComponent<NetworkObject>();
        if (netObj == null) return;

        // Verifica se é um player da lista de jogadores ativos
        if (!runner.IsPlayer.Equals(netObj.InputAuthority)) return;

        // Tenta pegar o PlayerManager
        PlayerController playerController = netObj.GetComponent<PlayerController>();
        /*if (playerController != null && !playersCollided.Contains(playerController))
        {
            playersCollided.Add(playerController);
            playerController.missionStaySquare = true; ; //mudar para script de controle ////////////////////////
        }*/
    }
}
