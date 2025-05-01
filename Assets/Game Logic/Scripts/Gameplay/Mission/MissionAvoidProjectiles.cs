using System.Collections;
using System.Collections.Generic;
using CrazyGames;
using Fusion;
using UnityEngine;

public class MissionAvoidProjectiles : Missions
{
    [Header("Mission 0 - A")]
    [SerializeField] NetworkObject projectilePrefab;
    //private NetworkRunner runner;

    bool isInstantiate = false;

    private Vector2[] directionsProjectitles = new Vector2[4];

    // x = -22 até 22 y = -14 ate 14 
    //private sbyte posXSpawn, posYSpawn;

    sbyte randomURDL;
    [SerializeField] int[] randomQuantProject = new int[4];

    [SerializeField] int[] projectilesLess = new int[4];
    [SerializeField] int totalProjects;

    [SerializeField] sbyte[] indexProj = new sbyte[4];
    [SerializeField] int[] quantityProjectiles = new int[4] { 25, 25, 25, 25, };
    void Update()
    {
    }

    public override void FixedUpdateNetwork()
    {
        StartMission();
    }

    public override void CallStartMission()
    {
        //runner = FindObjectOfType<NetworkRunner>();
        print("Begginng");

        StartMission();
    }

    public override void CallCompleteMission()
    {
        CompleteMission();
    }

    IEnumerator CountDown()
    {
        InstacienteProjectiles();

        yield return new WaitForSeconds(0.5f);

        LocalIntanciete();
    }


    void SetupDirections()
    {
        directionsProjectitles[0] = Vector2.up;
        directionsProjectitles[1] = Vector2.right;
        directionsProjectitles[2] = Vector2.down;
        directionsProjectitles[3] = Vector2.left;
    }

    void RandomDirInstanciete()
    {
        //RANDOM UP, RIGHT, DOWN, LEFT
        randomURDL = (sbyte)Random.Range(0, 4);

    }

    void RandonQuantProjectiles()
    {
        /*randomQuantProject[0] = Random.Range(0, 26);
        randomQuantProject[1] = Random.Range(0, 26);
        randomQuantProject[2] = Random.Range(0, 26);
        randomQuantProject[3] = Random.Range(0, 26);*/

        // or

        randomQuantProject[randomURDL] = Random.Range(0, 26);
    }

    void QuantProjectSpawn()
    {
        //total de bomba = 25 - valor sorteado 
        //projectilesLess[0] = quantityProjectiles[0] - randomQuantProject[0];

        if (projectilesLess[randomURDL] - randomQuantProject[randomURDL] > 0)
        {
            projectilesLess[randomURDL] = quantityProjectiles[randomURDL] - randomQuantProject[randomURDL];
            totalProjects -= randomQuantProject[randomURDL];
        }
        else if (projectilesLess[randomURDL] - randomQuantProject[randomURDL] <= 0)
        {
            // se caso o valor do que falta - o valor sorteado for abaixo de 0 
            // deve pegar o valor sortedo e deccrescente em um ate que o valor que falta - o valor sorteado fique 0 

            int value = randomQuantProject[randomURDL]; // 9

                   //   5                        5           =  0
            while (projectilesLess[randomURDL] - randomQuantProject[randomURDL] <= 0)
            {
                value--; // 8, 7, 6, 5, 4
                randomQuantProject[randomURDL] = value; // 8, 7, 6, 5 , 4
            }

            projectilesLess[randomURDL] = quantityProjectiles[randomURDL] - randomQuantProject[randomURDL];
        }
        else if (projectilesLess[randomURDL] - randomQuantProject[randomURDL] == 0)
        {
            quantityProjectiles[randomURDL] = 0;
            FinishAllProjectiles();

            //Chamar metodo para sortear outro lado
        }
    }
    void QuantProjectSpawnRight() { }
    void QuantProjectSpawnDown()  { }
    void QuantProjectSpawnLeft()  { }

    void FinishAllProjectiles()
    {
        if (totalProjects <= 0)
        {
            //Chamar metodo de finalizacao 
        }
        else if (totalProjects > 0)
        {
            RandomDirInstanciete();
            RandonQuantProjectiles();
            //StartCoroutine(CountDown());
        }
    }

    void InstacienteProjectiles()
    {
        QuantProjectSpawn();
    }

    void LocalIntanciete() 
    {
        float extremes = 0; 

        if (randomURDL % 2 == 0) // 0 or 2
        {
            if (randomURDL == 0)
                extremes = 14;
            else if (randomURDL == 2)
                extremes = -14;

            float randomPositionSpawn = Random.Range(-22, 22);

            if (indexProj[randomURDL] != randomQuantProject[randomURDL])
            {
                NetworkObject obj = Runner.Spawn(projectilePrefab, new Vector3(randomPositionSpawn, extremes, 0), Quaternion.identity);
                if (obj.TryGetComponent<MoveProjectiles>(out var projectileScript))
                {
                    indexProj[randomURDL]++;
                    projectileScript.GetDirAndIndex(directionsProjectitles, randomURDL);
                }
            }
            else 
            {
                if(indexProj[randomURDL] >= randomQuantProject[randomURDL])
                {
                    Debug.Log("Todos projéteis dessa direção foram instanciados.");
                    FinishAllProjectiles();
                    //yield break;
                }

                //StartCoroutine(CountDown());
            }
        }
        else // 1 or 3
        {
            if (randomURDL == 1)
                extremes = 22;
            else if (randomURDL == 3)
                extremes = -22;

            float randomPositionSpawn = Random.Range(-14, 14);

            if (indexProj[randomURDL] != randomQuantProject[randomURDL])
            {
                NetworkObject obj = Runner.Spawn(projectilePrefab, new Vector3(extremes, randomPositionSpawn, 0), Quaternion.identity);
                if (obj.TryGetComponent<MoveProjectiles>(out var projectileScript))
                {
                    projectileScript.GetDirAndIndex(directionsProjectitles, randomURDL);
                }
            }
            else
            {
                //StartCoroutine(CountDown());
            }
        }
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
        Debug.Log("AVOID PROJECTILES, Beginning!");
        //RandomLocalIntanciete();

        SetupDirections();
        FinishAllProjectiles();
    }
    protected override void CompleteMission()
    {
        totalProjects = 100;

        for (int i = 0; i < quantityProjectiles.Length; i++)
        {
            indexProj[i] = 0;
        }

        Debug.Log("Avoid projéteis, Finish!");
    }
}
