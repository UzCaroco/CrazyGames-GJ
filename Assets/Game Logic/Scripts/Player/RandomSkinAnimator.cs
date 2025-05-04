using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSkinAnimator : NetworkBehaviour
{
    [SerializeField][Networked] int PlayerSpawn {get; set;}
    [Networked] int numRandom { get; set; }
    [Networked, Capacity(8)] public NetworkArray<int> numeros => default;
    int minPlayer = 0;
    int maxPlayer = 8;

    public override void Spawned()
    {
        if (Object.HasStateAuthority)
        {
            DrawSkinController();
        }
    }
    public void DrawSkinController()
    {
        List<int> temp = new List<int>();
        while (temp.Count <  8)
        {
            int numeroAleatorio = Random.Range(minPlayer, maxPlayer);
            if (!temp.Contains(numeroAleatorio))
            {
                temp.Add(numeroAleatorio);
            }
        }

        for (int i = 0; i < temp.Count; i++)
        {
            numeros.Set(i, temp[i]);
        }

        foreach (int numero in numeros) 
        {
            Debug.Log(numero);
        }
    }
    public void PlayerIsSpawned()
    {
        if (PlayerSpawn < numeros.Length)
        {
            numRandom = numeros[PlayerSpawn];
            Debug.Log(numeros[PlayerSpawn] + " " + numRandom);
            PlayerSpawn++;
        }
        else
        {
            Debug.LogError("PlayerSpawn fora dos limites da lista de números!");
        }
    }

    public int SetNumRandom()
    {
        return numRandom;
    }
}
