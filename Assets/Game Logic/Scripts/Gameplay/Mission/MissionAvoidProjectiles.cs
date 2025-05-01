using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class MissionAvoidProjectiles : Missions
{
    [Header("Mission 0 - A")]
    [SerializeField] NetworkObject projectilePrefab;

    bool isInstantiate = false;

    private Vector2[] directionsProjectitles = new Vector2[4];

    // x = -22 até 22 y = -14 ate 14 
    //private sbyte posXSpawn, posYSpawn;

    sbyte randomURDL;
    int[] randomQuantProject = new int[4];

    int[] projectilesLess = new int[4];
    [SerializeField] int totalProjects;


    int[] quantityProjectiles = new int[4] { 25, 25, 25, 25, };
    void Update()
    {
        //StartMission();
    }

    private void FixedUpdate()
    {
        StartMission();
    }

    public override void FixedUpdateNetwork()
    {

    }

    public override void CallStartMission()
    {
        StartMission();
        print("Begginng");
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

    void RandomDirInstanciete()
    {
        //RANDOM UP, RIGHT, DOWN, LEFT
        randomURDL = (sbyte)Random.Range(0, 5);

    }

    void RandonQuantProjectiles()
    {
        randomQuantProject[0] = Random.Range(0, 26);
        randomQuantProject[1] = Random.Range(0, 26);
        randomQuantProject[2] = Random.Range(0, 26);
        randomQuantProject[3] = Random.Range(0, 26);
    }

    void QuantProjectSpawnUp()
    {
        //total de bomba = 25 - valor sorteado 
        //projectilesLess[0] = quantityProjectiles[0] - randomQuantProject[0];

        if (projectilesLess[0] - randomQuantProject[0] > 0)
        {
            projectilesLess[0] = quantityProjectiles[0] - randomQuantProject[0];
            totalProjects -= randomQuantProject[0];
        }
        else if (projectilesLess[0] - randomQuantProject[0] <= 0)
        {
            // se caso o valor do que falta - o valor sorteado for abaixo de 0 
            // deve pegar o valor sortedo e deccrescente em um ate que o valor que falta - o valor sorteado fique 0 

            int value = randomQuantProject[0]; // 9


                   //   5                        5           =  0
            while (projectilesLess[0] - randomQuantProject[0] <= 0)
            {
                value--; // 8, 7, 6, 5, 4
                randomQuantProject[0] = value; // 8, 7, 6, 5 , 4
            }

            projectilesLess[0] = quantityProjectiles[0] - randomQuantProject[0];
        }
        else if (projectilesLess[0] - randomQuantProject[0] == 0)
        {
            quantityProjectiles[0] = 0;


            //Chamar metodo para sortear outro lado
        }
    }
    void QuantProjectSpawnRight()
    { }
    void QuantProjectSpawnDown()
    { }
    void QuantProjectSpawnLeft()
    { }

    void FinishAllProjectiles()
    {
        if (totalProjects <= 0)
        {
            //Chamar metodo de finalizacao 
        }
        else if (totalProjects > 0)
        {
            foreach(var projectile in projectilesLess)
            {
                do
                {
                    RandonQuantProjectiles();
                    InstacienteProjectiles();
                }
                while (projectile == 0);
            }
        }

    }

    void InstacienteProjectiles()
    {
        switch (randomURDL)
        {
            case 0:
                QuantProjectSpawnUp();
                    float randomPositionSpawn = Random.Range(-6, 7);

                    NetworkObject obj = Runner.Spawn(projectilePrefab, new Vector3(randomPositionSpawn, 6, 0), Quaternion.identity);
                    if (obj.TryGetComponent<MoveProjectiles>(out var projectileScript))
                    {
                       
                    }
                break;

            case 1:
                QuantProjectSpawnRight();
                break;
            
            case 2:
                QuantProjectSpawnDown();
                break;
            
            case 3:
                QuantProjectSpawnLeft();
                break;
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
        RandomLocalIntanciete();

        //SetupDirections();
        //RandomDirInstanciete();
    }
    protected override void CompleteMission()
    {
        //Debug.Log("Avoid projéteis, Finish!");
    }
}
