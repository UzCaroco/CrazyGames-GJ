using System.Collections;
using System.Collections.Generic;
using CrazyGames;
using Fusion;
using Unity.VisualScripting;
using UnityEngine;

public class MissionAvoidProjectiles : Missions
{
    [Header("Mission 0 - A")]
    [SerializeField] NetworkObject projectilePrefab;
    //private NetworkRunner runner;

    [SerializeField]bool isInstantiate = false;

    private Vector2[] directionsProjectitles = new Vector2[4];

    // x = -22 até 22 y = -14 ate 14 
    //private sbyte posXSpawn, posYSpawn;

    sbyte randomURDL;
    [SerializeField] int[] randomQuantProject = new int[4];

    [SerializeField] int[] projectilesLess = new int[4] { 12, 12, 12, 12, };
    [SerializeField] int totalProjects;

    [SerializeField] sbyte[] indexProj = new sbyte[4];
    [SerializeField] int[] quantityProjectiles = new int[4] { 12, 12, 12, 12, };
    void Update()
    {
        /*if (isInstantiate)
        {
            StartMission();
            isInstantiate = false;
        }*/
    }

    public override void FixedUpdateNetwork()
    {
        if (isInstantiate)
        {
            StartMission();
            isInstantiate = false;
        }
    }

    public override void CallStartMission()
    {
        StartMission();
    }

    public override void CallCompleteMission()
    {
        CompleteMission();
    }

    void SetupDirections()
    {
        directionsProjectitles[0] = Vector2.up;
        directionsProjectitles[1] = Vector2.right;
        directionsProjectitles[2] = Vector2.down;
        directionsProjectitles[3] = Vector2.left;
    }

    void FinishAllProjectiles()
    {
        if (totalProjects <= 0)
        {
            totalProjects = 0;

            StopCoroutine(CountDown());
            //Debug.Log("Finalizou");
            //Chamar metodo de finalizacao 
        }
        else if (totalProjects > 0)
        {
            InstaciateConfig();
        }
    }

    void RandomDirInstanciete()
    {
        //RANDOM UP, RIGHT, DOWN, LEFT
        sbyte randomLast = randomURDL;

        while (randomLast == randomURDL)
        {
            randomURDL = (sbyte)Random.Range(0, 4);
        }

        print("random" + randomURDL);
    }

    void RandonQuantProjectiles()
    {
        /*randomQuantProject[0] = Random.Range(0, 26);
        randomQuantProject[1] = Random.Range(0, 26);
        randomQuantProject[2] = Random.Range(0, 26);
        randomQuantProject[3] = Random.Range(0, 26);*/

        // or

        indexProj[randomURDL] = 0;
        randomQuantProject[randomURDL] = Random.Range(0, quantityProjectiles[0] + 1);

        //print("randomQuant" + randomQuantProject[randomURDL]);
    }

    void QuantProjectSpawn()
    {
        //total de bomba = 25 - valor sorteado 
        //projectilesLess[0] = quantityProjectiles[0] - randomQuantProject[0];

        //print("projetilless" +projectilesLess[randomURDL]);

        if (projectilesLess[randomURDL] - randomQuantProject[randomURDL] > 0)
        {
            //print("maior");
            projectilesLess[randomURDL] -= randomQuantProject[randomURDL];
            totalProjects -= randomQuantProject[randomURDL];
        }
        else if (projectilesLess[randomURDL] - randomQuantProject[randomURDL] <= 0)
        {
            //print("menor");
            // se caso o valor do que falta - o valor sorteado for abaixo de 0 
            // deve pegar o valor sortedo e deccrescente em um ate que o valor que falta - o valor sorteado fique 0 

            int value = randomQuantProject[randomURDL]; // 9

                   //   5                        5           =  0
            while (projectilesLess[randomURDL] - randomQuantProject[randomURDL] < 0)
            {
                value--; // 8, 7, 6, 5, 4
                randomQuantProject[randomURDL] = value; // 8, 7, 6, 5 , 4
            }

            projectilesLess[randomURDL] -= randomQuantProject[randomURDL];
            totalProjects -= randomQuantProject[randomURDL];
        }
        else if (projectilesLess[randomURDL] - randomQuantProject[randomURDL] == 0)
        {
            //print("igual");
            projectilesLess[randomURDL] = 0;

            //Chamar metodo para sortear outro lado
        }

        StartCoroutine(SpawnTime()); //////////////////////////////////////////////

       // print("projetilless" + projectilesLess[randomURDL]);
    }

    void InstaciateConfig()
    {
        RandomDirInstanciete();
        RandonQuantProjectiles();
        QuantProjectSpawn();
    }
    IEnumerator SpawnTime()
    {
        while (indexProj[randomURDL] != randomQuantProject[randomURDL])
        {
            yield return new WaitForSeconds(0.5f);

            LocalIntanciete();
        }

        if (indexProj[randomURDL] == randomQuantProject[randomURDL])
        {
            StartCoroutine(CountDown());
        }
    }

    IEnumerator CountDown()
    {
        StopCoroutine(SpawnTime());

        yield return new WaitForSeconds(1f);

        FinishAllProjectiles();
    }

    void LocalIntanciete() 
    {
        float extremes = 0; 

        if (randomURDL % 2 == 0) // 0 or 2
        {
            if (randomURDL == 0)
                extremes = -14;
            else if (randomURDL == 2)
                extremes = 14;

            //print("extremes" + extremes);
            float randomPositionSpawn = Random.Range(-10.5f, 10.5f);

            if (indexProj[randomURDL] != randomQuantProject[randomURDL])
            {
                NetworkObject obj = Runner.Spawn(projectilePrefab, new Vector3(randomPositionSpawn, extremes, 0), Quaternion.identity);
                obj.transform.SetParent(transform);
                indexProj[randomURDL]++;
                if (obj.TryGetComponent<MoveProjectiles>(out var projectileScript))
                {
                    projectileScript.GetDirAndIndex(directionsProjectitles, randomURDL);
                }
            }
            else 
            {
                if(indexProj[randomURDL] > randomQuantProject[randomURDL])
                {
                    indexProj[randomURDL] = (sbyte)randomQuantProject[randomURDL];
                    
                    Debug.Log("Todos projéteis dessa direção foram instanciados.");
                }
            }
        }
        else // 1 or 3
        {
            if (randomURDL == 1)
                extremes = -22;
            else if (randomURDL == 3)
                extremes = 22;

            //print("extremes" + extremes);

            float randomPositionSpawn = Random.Range(-7f, 7f);

            if (indexProj[randomURDL] != randomQuantProject[randomURDL])
            {
                NetworkObject obj = Runner.Spawn(projectilePrefab, new Vector3(extremes, randomPositionSpawn, 0), Quaternion.identity);
                obj.transform.SetParent(transform);
                indexProj[randomURDL]++;
                if (obj.TryGetComponent<MoveProjectiles>(out var projectileScript))
                {
                    projectileScript.GetDirAndIndex(directionsProjectitles, randomURDL);
                }
            }
            else
            {
                if (indexProj[randomURDL] >= quantityProjectiles[randomURDL])
                {
                    indexProj[randomURDL] = (sbyte)randomQuantProject[randomURDL];

                    Debug.Log("Todos projéteis dessa direção foram instanciados.");
                    
                }
            }
        }
    }

    /*void RandomLocalIntanciete()
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
    }*/

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
            projectilesLess[i] = 25;
        }
        Debug.Log("Avoid projéteis, Finish!");
    }
}
