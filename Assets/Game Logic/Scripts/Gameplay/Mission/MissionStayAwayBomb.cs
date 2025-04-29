using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class MissionStayAwayBomb : Missions
{
    [Header("Mission 6 - SAB")]
    [SerializeField] private GameObject prefabBomb;

    [SerializeField] private bool isInitialized;

    [SerializeField] private byte index;

    [SerializeField] private int posXBomb, posYBomb;
    [SerializeField] private int lastXRandom, lastYRandom;
    [SerializeField] private Vector2 spawnBomb;
    [SerializeField] private bool isExploding;

    void Start()
    {
        
    }

    void Update()
    {
        StartMission();
    }

    void DrawPosition()
    {
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
        spawnBomb = new Vector2(posXBomb, posYBomb);
    }

    void initializeBomb()
    {
        if (!isInitialized && index < 5)
        {
            DrawPosition();

            Debug.Log("Spawndando em X:" + posXBomb + "em Y:" + posYBomb + "sendo a:" + index);
            GameObject bomb = Instantiate(prefabBomb, spawnBomb, transform.rotation);
            bomb.transform.SetParent(transform);
            isInitialized = true;
            
            StartCoroutine(CountDown());
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
