using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class MissionStaySquare : Missions
{
    [Header("Mission 7 - SS")]
    SquareController squareController;
    [SerializeField] private NetworkRunner runner;

    [SerializeField] float TimeToArriveOnTheSquare = 15f;

    [SerializeField] private GameObject squarePrefab; 
    [SerializeField] private int posXSquare, posYSquere;
    private Vector2 squarePos;

    bool isInicialized;

    private void Awake()
    {
        runner = FindObjectOfType<NetworkRunner>();
        squareController = GetComponentInChildren<SquareController>();

        isInicialized = false;
        squareController.SetFinishTask(false);
    }
    void Start()
    {
        StartMission();
    }
    private void FixedUpdate()
    {
        
    }
    public override void FixedUpdateNetwork()
    {
        //base.FixedUpdateNetwork();

        //StartMission();
    }

    IEnumerator Countdown()
    {
        InicializedSquare();

        yield return new WaitForSeconds(TimeToArriveOnTheSquare);

        GetResults();
        ControlScorePlayers();
    }


    void SquarePosDraw()
    {
        posXSquare = Random.Range(-7, 7);
        posYSquere = Random.Range(-7, 7);

        squarePos = new Vector2 (posXSquare, posYSquere);
    }
    
    void InicializedSquare()
    {
        if (!isInicialized)
        {
            SquarePosDraw();

            Debug.Log("Spawndando em X:" + posXSquare + "em Y:" + posYSquere);
            NetworkObject square = Runner.Spawn(squarePrefab, squarePos, Quaternion.identity);
            square.transform.SetParent(transform);
            isInicialized = true;

        }
        else
        {
            print("nao");
        }
    }

    void GetResults()
    {
        squareController.SetFinishTask(true);
    }

    void ControlScorePlayers()
    {
        foreach (var player in runner.ActivePlayers)
        {
            NetworkObject playerObject = runner.GetPlayerObject(player);
            if (playerObject != null)
            {
                var playerController = playerObject.GetComponent<PlayerController>(); ///////////Script que tera as informações
                if (playerController != null)
                {
                    //se o player teve colissao com o quadrado 
                    // ++score

                    //se nao sem score a mais
                }
            }
        }
    }

    protected override void StartMission()
    {
        Debug.Log("Stay Square, Beginning!");

        StartCoroutine(Countdown());
    }
    protected override void CompleteMission()
    {
        Debug.Log("Stay Square, Finish!");
    }
}
