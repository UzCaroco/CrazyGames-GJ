using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System.Collections;

public class PlayerChecker : NetworkBehaviour
{

    [SerializeField] PlayerController playerController;
    private void OnEnable()
    {
        StartCoroutine(enumerator());

        
    }

    public void PegarPlayerControler(PlayerController playerController)
    {
        this.playerController = playerController;
        Debug.Log("PlayerController: " + playerController);
    }

    public override void Spawned()
    {
        
    }


    //----------------------------------------------------------------------------------------------------------//
    //----------------Verificando se o player fugiu dos projéteis-----------------//
    //--------------------------------------------//

    public NetworkBool MissionProjectile(bool expectedResult)
    {
        Debug.Log("PlayerController: " + playerController);

        PlayerController ppppp = gameObject.GetComponent<PlayerController>();
        Debug.Log("pppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppp: " + ppppp);

        if (playerController.missionProjectile == expectedResult)
        {
            Debug.Log("Player " + playerController + " completed the mission!");
            return true;
        }

        return false;
    }

    //--------------------------------------------//
    //-----------------------------------------------------------------------//
    //----------------------------------------------------------------------------------------------------------//



    //----------------------------------------------------------------------------------------------------------//
    //----------Verificando de maneira segura se o player coletou a moeda----------//
    //--------------------------------------------//


    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void Rpc_NotifyMissionCompletedCollectCoin()
    {
        // Apenas quem tem autoridade do State (o Host) vai rodar isso
        GameChecker gameChecker = FindObjectOfType<GameChecker>();
        gameChecker.NotifyMissionCompleted(this);
    }
    public void CheckAndNotifyMissionCollectCoin()
    {
        Debug.Log("PlayerController: " + playerController);
        PlayerController ppppp = gameObject.GetComponent<PlayerController>();
        Debug.Log("pppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppp: " + ppppp);
        if (playerController.missionCollectCoin)
        {
            Rpc_NotifyMissionCompletedCollectCoin(); // Pede pro Host registrar
        }
    }


    //--------------------------------------------//
    //-----------------------------------------------------------------------//
    //----------------------------------------------------------------------------------------------------------//



    //----------------------------------------------------------------------------------------------------------//
    //----------Verificando de maneira segura se o player copiou os movimentos----------//
    //--------------------------------------------//

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void Rpc_NotifyMissionCompletedCopyMoviment()
    {
        // Apenas quem tem autoridade do State (o Host) vai rodar isso
        GameChecker gameChecker = FindObjectOfType<GameChecker>();
        gameChecker.NotifyMissionCompleted(this);
    }
    public void CheckAndNotifyMissionCopyMoviment()
    {
        Debug.Log("PlayerController: " + playerController);
        PlayerController ppppp = gameObject.GetComponent<PlayerController>();
        Debug.Log("pppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppp: " + ppppp);
        if (playerController.missionCollectCoin)
        {
            Rpc_NotifyMissionCompletedCollectCoin(); // Pede pro Host registrar
        }
    }

    //--------------------------------------------//
    //-----------------------------------------------------------------------//
    //----------------------------------------------------------------------------------------------------------//



    //----------------------------------------------------------------------------------------------------------//
    //----------------Verificando se o player não se moveu-----------------//
    //--------------------------------------------//

    public NetworkBool MissionDontMove(bool expectedResult)
    {
        Debug.Log("PlayerController: " + playerController);
        PlayerController ppppp = gameObject.GetComponent<PlayerController>();
        Debug.Log("pppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppp: " + ppppp);
        if (playerController.missionDontMove == expectedResult)
        {
            Debug.Log("Player " + playerController + " completed the mission!");
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
        Debug.Log("PlayerController: " + playerController);

        if (playerController.missionMove == expectedResult)
        {
            PlayerController ppppp = gameObject.GetComponent<PlayerController>();
            Debug.Log("pppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppp: " + ppppp);
            Debug.Log("Player " + playerController + " completed the mission!");
            return true;
        }

        return false;
    }

    //--------------------------------------------//
    //-----------------------------------------------------------------------//
    //----------------------------------------------------------------------------------------------------------//



    //----------------------------------------------------------------------------------------------------------//
    //----------Verificando de maneira segura se o player empurrou o rival----------//
    //--------------------------------------------//

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void Rpc_NotifyMissionCompletedPushRival()
    {
        // Apenas quem tem autoridade do State (o Host) vai rodar isso
        GameChecker gameChecker = FindObjectOfType<GameChecker>();
        gameChecker.NotifyMissionCompleted(this);
    }
    public void CheckAndNotifyMissionPushRival()
    {
        Debug.Log("PlayerController: " + playerController);
        PlayerController ppppp = gameObject.GetComponent<PlayerController>();
        Debug.Log("pppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppp: " + ppppp);
        if (playerController.missionCollectCoin)
        {
            Rpc_NotifyMissionCompletedCollectCoin(); // Pede pro Host registrar
        }
    }

    //--------------------------------------------//
    //-----------------------------------------------------------------------//
    //----------------------------------------------------------------------------------------------------------//



    //----------------------------------------------------------------------------------------------------------//
    //----------------Verificando se o player fugiu da bomba-----------------//
    //--------------------------------------------//

    public bool MissionBomb(bool expectedResult)
    {
        Debug.Log("PlayerController: " + playerController);
        PlayerController ppppp = gameObject.GetComponent<PlayerController>();
        Debug.Log("pppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppp: " + ppppp);
        if (playerController.missionBomb == expectedResult)
        {
            Debug.Log("Player " + playerController + " completed the mission!");
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
        Debug.Log("PlayerController: " + playerController);
        PlayerController ppppp = gameObject.GetComponent<PlayerController>();
        Debug.Log("pppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppp: " + ppppp);
        if (playerController.missionStaySquare == expectedResult)
        {
            Debug.Log("Player " + playerController + " completed the mission!");
            return true;
        }

        return false;
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
        Debug.Log(gameManager + "ta falso");
        if (gameManager == null) return;
        Debug.Log(gameManager + "ta ok");

        Debug.Log("HasInputAuthority: " + HasInputAuthority);
        if (HasInputAuthority)
        {
            Debug.Log(gameManager + "ta de brincadeira");

            gameManager.RPC_AddScore(Runner.LocalPlayer, amount);
            Debug.Log("pontuação adicionada");

            // Esse valor é local e pode estar desatualizado
            int pontuacaoLocal = gameManager.GetScore(Object.InputAuthority);
            Debug.Log($"[PlayerChecker] Pontuação local do player: {pontuacaoLocal}");
        }
        else if (HasStateAuthority)
        {
            Debug.Log("HasStateAuthority: " + HasStateAuthority);


            Debug.Log(gameManager + "ta de brincadeira");

            gameManager.RPC_AddScore(Runner.LocalPlayer, amount);
            Debug.Log("pontuação adicionada");

            // Esse valor é local e pode estar desatualizado
            int pontuacaoLocal = gameManager.GetScore(Object.InputAuthority);
            Debug.Log($"[PlayerChecker] Pontuação local do player: {pontuacaoLocal}");
        }
    }

}
