using System.Collections;
using System.Collections.Generic;
using CrazyGames;
using Fusion;
using UnityEngine;

public class MissionStayAwayBomb : Missions
{
    [Header("Mission 6 - SAB")]
    [SerializeField] private GameObject prefabBomb;

    [SerializeField] private byte index;

    [SerializeField] private int posXBomb, posYBomb;
    [SerializeField] private int lastXRandom, lastYRandom;

    [Networked] private Vector2 BombPosition { get; set; }
    [Networked] private bool isInitialized { get; set; }

    [SerializeField] private bool isExploding;

    void Start()
    {
        
    }

    void Update()
    {
        StartMission();
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
            lastXRandom = Random.Range(-7,7);
        }

        while(lastYRandom == posYBomb)
        {
            lastYRandom = Random.Range(-4,4);
        }

        posXBomb = lastXRandom;
        posYBomb = lastYRandom;
        BombPosition = new Vector2(posXBomb, posYBomb);
    }

    void initializeBomb()
    {
        if (!isInitialized && index < 5)
        {
            DrawPosition();

            Debug.Log("Spawndando em X:" + posXBomb + "em Y:" + posYBomb + "sendo a:" + index);
            NetworkObject bomb = Runner.Spawn(prefabBomb, BombPosition, Quaternion.identity);
            bomb.transform.SetParent(transform);
            isInitialized = true;
            
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
        if (index + 1 <= 6)
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

        isInitialized = false;
    }

    protected override void StartMission()
    {
        Debug.Log("Stay Away Bomb, Beginning!");

        if (index == 5)
        {

        }
        else 
        {
            initializeBomb();
        }
    }

    protected override void CompleteMission()
    {
        Debug.Log("Stay Away Bomb, Finish!");
        index = 0;
    }

    /*protected override void LostMission()*/
}
