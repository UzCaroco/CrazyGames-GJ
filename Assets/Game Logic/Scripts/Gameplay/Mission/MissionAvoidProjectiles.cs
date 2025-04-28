using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class MissionAvoidProjectiles : Missions
{
    [Header("Mission 0 - A")]
    [SerializeField] NetworkObject projectilePrefab;

    bool isInstantiate = false;
    private Vector2[] directionsProjectitles;

    private int lastRandom;
    int[] quantityProjectiles = new int[4];

    void Start()
    {
        RandomLocalIntanciete();
    }
    void Update()
    {
        StartMission();
    }

    void SetupDirections()
    {

    }

    void RandomLocalIntanciete()
    {
        sbyte randomHorizontalOrVertical = (sbyte)Random.Range(0, 2);

        if (randomHorizontalOrVertical == 0) //Projétil Vertical
        {
            float randomPositionSpawn = Random.Range(-6, 7);

            NetworkObject obj = Runner.Spawn(projectilePrefab, new Vector3(randomPositionSpawn, 6, 0), Quaternion.identity);
            if (obj.TryGetComponent<Projectile>(out var projectileScript))
            {
                projectileScript.Init(new Vector2(0, -1)); // cria um método Init na classe Projectile
            }
        }

        else if (randomHorizontalOrVertical == 1) //Projétil Horizontal
        {
            float randomPositionSpawn = Random.Range(-3.5f, 3.5f);

            NetworkObject obj = Runner.Spawn(projectilePrefab, new Vector3(10, randomPositionSpawn, 0), Quaternion.Euler(0, 0, -90));
            if (obj.TryGetComponent<Projectile>(out var projectileScript))
            {
                projectileScript.Init(new Vector2(0, -1)); // cria um método Init na classe Projectile
            }
        }

    }

    protected override void StartMission()
    {
        //Debug.Log("Avoid projéteis, Beginning!");
    }
    protected override void CompleteMission()
    {
        //Debug.Log("Avoid projéteis, Finish!");
    }
}
