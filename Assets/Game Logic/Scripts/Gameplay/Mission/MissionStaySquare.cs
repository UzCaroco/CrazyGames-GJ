using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class MissionStaySquare : Missions
{
    [Header("Mission 7 - SS")]
    [SerializeField] SquareController squareController;
    [SerializeField] private NetworkRunner runner;

    [SerializeField] float TimeToArriveOnTheSquare = 15f;

    [SerializeField] private GameObject squarePrefab; 
    [SerializeField] private float posXSquare, posYSquere;
    private Vector2 squarePos;

    [SerializeField] bool isInicialized;

    private void Awake()
    {
        
    }
    void Start()
    {
        //StartMission();
    }
    private void FixedUpdate()
    {
        print("Update");
    }
    public override void CallStartMission()
    {
        runner = FindObjectOfType<NetworkRunner>();

        StartMission();
        print("Begginng");
    }
    public override void CallCompleteMission()
    {
        CompleteMission();
    }
    public override void FixedUpdateNetwork()
    {
        print("Update net");
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
        posXSquare = Random.Range(-10.5f, 10.5f);
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
            
            squareController = square.GetComponent<SquareController>();
            squareController.SetFinishTask(false);

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
        isInicialized = false;

        Debug.Log("Stay Square, Beginning!");

        StartCoroutine(Countdown());
    }
    protected override void CompleteMission()
    {
        Debug.Log("Stay Square, Finish!");
        isInicialized = false;
        squareController.SetFinishTask(false);
    }
}
