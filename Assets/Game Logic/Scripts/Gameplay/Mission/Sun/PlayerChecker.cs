using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System.Collections;

public class PlayerChecker : NetworkBehaviour
{

    public PlayerController playerController;
    private void OnEnable()
    {
        StartCoroutine(enumerator()); 



    }

    public void PegarPlayerControler()
    {
        
        Debug.Log("PlayerController: " + playerController + "OU SEJA N�O � NULOOO");
    }

    public override void Spawned()
    {
        GameChecker gameChecker = FindAnyObjectByType<GameChecker>();
        if (gameChecker == null) return;

        gameChecker.AdicionarPlayerALista(this);
    }


    //----------------------------------------------------------------------------------------------------------//
    //----------------Verificando se o player fugiu dos proj�teis-----------------//
    //--------------------------------------------//

    public NetworkBool MissionProjectile(bool expectedResult)
    {
        Debug.Log("PlayerController: " + playerController  + "SE N�O TIVER NADA ANTES � NULO");

        if (playerController.missionProjectile == expectedResult)
        {
            Debug.Log("Player COMPLETOOOOOOOOOOOOOOOOOOOOOOOU the mission!");
            return true;
        }

        return false;
    }

    //--------------------------------------------//
    //-----------------------------------------------------------------------//
    //----------------------------------------------------------------------------------------------------------//



    //----------------------------------------------------------------------------------------------------------//
    //----------------Verificando se o player n�o se moveu-----------------//
    //--------------------------------------------//

    public NetworkBool MissionDontMove(bool expectedResult)
    {
        Debug.Log("PlayerController: " + playerController + "SE N�O TIVER NADA ANTES � NULO");
        if (playerController.missionDontMove == expectedResult)
        {
            Debug.Log("Player COMPLETOOOOOOOOOOOOOOOOOOOOOOOU the mission!");
            return true;
        }

        return false;
    }

    //--------------------------------------------//
    //-----------------------------------------------------------------------//
    //----------------------------------------------------------------------------------------------------------//



    //----------------------------------------------------------------------------------------------------------//
    //----------------Verificando se o player se moveu-----------------//
    //--------------------------------------------//

    public NetworkBool MissionMove(bool expectedResult)
    {
        Debug.Log("PlayerController: " + playerController + "SE N�O TIVER NADA ANTES � NULO");

        if (playerController.missionMove == expectedResult)
        {
            Debug.Log("Player COMPLETOOOOOOOOOOOOOOOOOOOOOOOU the mission!");
            return true;
        }

        return false;
    }

    //--------------------------------------------//
    //-----------------------------------------------------------------------//
    //----------------------------------------------------------------------------------------------------------//



    //----------------------------------------------------------------------------------------------------------//
    //----------------Verificando se o player fugiu da bomba-----------------//
    //--------------------------------------------//

    public bool MissionBomb(bool expectedResult)
    {
        Debug.Log("PlayerController: " + playerController + "SE N�O TIVER NADA ANTES � NULO");
        if (playerController.missionBomb == expectedResult)
        {
            Debug.Log("Player COMPLETOOOOOOOOOOOOOOOOOOOOOOOU the mission!");
            return true;
        }

        return false;
    }

    //--------------------------------------------//
    //-----------------------------------------------------------------------//
    //----------------------------------------------------------------------------------------------------------//



    //----------------------------------------------------------------------------------------------------------//
    //----------------Verificando se o player ficou no quadrado-----------------//
    //--------------------------------------------//

    public NetworkBool MissionStaySquare(bool expectedResult)
    {
        Debug.Log("PlayerController: " + playerController + "SE N�O TIVER NADA ANTES � NULO");

        if (playerController.missionStaySquare == expectedResult)
        {
            Debug.Log("Player COMPLETOOOOOOOOOOOOOOOOOOOOOOOU the mission!");
            return true;
        }

            return false;
    }

    //--------------------------------------------//
    //-----------------------------------------------------------------------//
    //----------------------------------------------------------------------------------------------------------//



    //----------------------------------------------------------------------------------------------------------//
    //----------Enviando para o GameManager que o player terminou a miss�o----------//
    //--------------------------------------------//

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void Rpc_NotifyMissionCompletedTheMission()
    {
        // Apenas quem tem autoridade do State (o Host) vai rodar isso
        GameChecker gameChecker = FindObjectOfType<GameChecker>();
        gameChecker.NotifyMissionCompleted(this);
    }
    public void CheckAndNotifyMissionCopyMoviment()
    {
        Debug.Log("PlayerController: " + playerController + "SE N�O TIVER NADA ANTES � NULO");
        if (playerController.missionCollectCoin)
        {
            Rpc_NotifyMissionCompletedTheMission(); // Pede pro Host registrar
        }
    }

    //--------------------------------------------//
    //-----------------------------------------------------------------------//
    //----------------------------------------------------------------------------------------------------------//


    IEnumerator enumerator()
    {

        yield return new WaitForSeconds(5f);
        // Adiciona 1 ponto ao jogador
        AddScoreToSelf(1);
    }

    private void AddScoreToSelf(int amount)
    {
        var gameManager = FindAnyObjectByType<GameManager>();
        if (gameManager == null) return;

        if (HasInputAuthority)
        {

            gameManager.RPC_AddScore(Runner.LocalPlayer, amount);

            // Esse valor � local e pode estar desatualizado
            int pontuacaoLocal = gameManager.GetScore(Object.InputAuthority);
        }
        else if (HasStateAuthority)
        {


            gameManager.RPC_AddScore(Runner.LocalPlayer, amount);

            // Esse valor � local e pode estar desatualizado
            int pontuacaoLocal = gameManager.GetScore(Object.InputAuthority);
        }
    }

}
