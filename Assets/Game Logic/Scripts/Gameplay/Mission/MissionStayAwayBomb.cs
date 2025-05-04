
using System.Collections;
using System.Collections.Generic;
using CrazyGames;
using Fusion;
using UnityEngine;

public class MissionStayAwayBomb : Missions
{
    [Header("Mission 6 - SAB")]

    [SerializeField] private GameObject prefabBomb;

    [SerializeField] private byte totalBomb;
    [SerializeField] private byte index;
    private byte indexExplode = 0;

    // x = -10.5 até 10.5 y = - 7 ate 7 
    [SerializeField] private float posXBomb, posYBomb;
    [SerializeField] private float lastXRandom, lastYRandom;

    // [Networked] private Vector2 BombPosition { get; set; }
    private Vector2 BombPosition;
    //[Networked] private bool isInitialized { get; set; }
    private bool isInicialized;

    [SerializeField] private bool isExploding;

    void Start()
    {

    }

    void Update()
    {
        /*print("Rodando Upfdate");

        StartMission();*/

    }

    public override void CallCompleteMission()
    {
        CompleteMission();
    }

    public override void CallStartMission()
    {
        index = 0;
        indexExplode = 0;
        isInicialized = false;
        print("Begginng");
    }

    public override void FixedUpdateNetwork()
    {
        
        StartMission();
    }

    void DrawPosition()
    {
        /*if (!HasStateAuthority)
            return;*/

        while (lastXRandom == posXBomb)
        {
            lastXRandom = (float)Random.Range(-10.5f, 10.5f);
        }

        while (lastYRandom == posYBomb)
        {
            lastYRandom = (float)Random.Range(-7f, 7f);
        }

        posXBomb = lastXRandom;
        posYBomb = lastYRandom;
        BombPosition = new Vector2(posXBomb, posYBomb);
    }

    void initializeBomb()
    {
        if (!isInicialized && index < totalBomb)
        {
            DrawPosition();

            Debug.Log("Spawndando em X:" + posXBomb + "em Y:" + posYBomb + "sendo a:" + index);
            NetworkObject bomb = Runner.Spawn(prefabBomb, BombPosition, Quaternion.identity);
            bomb.transform.SetParent(transform);
            isInicialized = true;

            StartCoroutine(CountDown());
        }
        else
        {
            print("nao");
        }
    }

    void ExplodeTheBomb()
    {

    }

    IEnumerator CountDown()
    {
        if (index + 1 <= (totalBomb + 1))
        {
            index++;

            Debug.Log("NextBomb: " + index);
        }
        else
        {
            Debug.Log("is Initialized all!");
            //index = 0;
        }

        yield return new WaitForSeconds(0.5f);

        isInicialized = false;
    }

    protected override void StartMission()
    {

        if (index == totalBomb)
        {

        }
        else
        {
            initializeBomb();
        }
    }

    public void Conclusion(byte totalBombExplode)
    {
        indexExplode += totalBombExplode;
    }

    protected override void CompleteMission()
    {
        Debug.Log("Stay Away Bomb, Finish!");
        index = 0;
        indexExplode = 0;
    }

    /*protected override void LostMission()*/
}
