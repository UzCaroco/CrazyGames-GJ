using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSkinAnimator : NetworkBehaviour
{
    int PlayerSpawn = 0;
    [Networked] int numRandom { get; set; }

     List<int> numeros = new List<int>() ;
    int minPlayer = 0;
    int maxPlayer = 8;

    public override void Spawned()
    {
        DrawSkinController();
    }
    public void DrawSkinController()
    {
        while (numeros.Count <  8)
        {
            int numeroAleatorio = Random.Range(minPlayer, maxPlayer + 1);
            if (!numeros.Contains(numeroAleatorio))
            {
                numeros.Add(numeroAleatorio);
            }
        }
    }
    public void PlayerIsSpawned()
    {
        numRandom = numeros[PlayerSpawn];

        if (PlayerSpawn <= 7) 
            PlayerSpawn++;
    }

    public int SetNumRandom()
    {
        return numRandom;
    }
}
