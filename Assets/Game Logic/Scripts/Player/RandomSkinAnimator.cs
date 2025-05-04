using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSkinAnimator : NetworkBehaviour
{
    [SerializeField] int PlayerSpawn = 0;
    [Networked] int numRandom { get; set; }

    [SerializeField]List<int> numeros = new List<int>();
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
            int numeroAleatorio = Random.Range(minPlayer, maxPlayer);
            if (!numeros.Contains(numeroAleatorio))
            {
                numeros.Add(numeroAleatorio);
            }
        }

        foreach (int numero in numeros) 
            {
                    Debug.Log(numero);

            }
    }
    public void PlayerIsSpawned()
    {
        numRandom = numeros[PlayerSpawn];
        Debug.Log(numeros[PlayerSpawn] + " " + numRandom);
        if (PlayerSpawn < 7) 
            PlayerSpawn++;
    }

    public int SetNumRandom()
    {
        return numRandom;
    }
}
