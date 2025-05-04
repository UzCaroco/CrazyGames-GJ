using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CrazyGames;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class SunController : NetworkBehaviour
{
    /// <summary>
    /// Scipts to Control the Missions
    /// </summary>/
    /// 

    // Score por jogador
    [Networked, Capacity(10)]
    private NetworkDictionary<PlayerRef, string> playerTextSee { get; } = default;
    

    private PlayerRef playerTextSunSays;

    [SerializeField] private GameObject painelText;
    [SerializeField] private TextMeshProUGUI textPainel;

    private string[] taskSunSays = new string[2] { "Sun Says: \r\n", "Sun Don't Says: \r\n"};
    private string[] nameTheMission = new string[7] { "Avoid The Projectiles", "Collect a Coin", "Copy the Movement", "Don't Move" , "Move" /*, "Push a Rival" */, "Stay Away From the Bomb" , "Go to the Square"};
    

    private TimerMission timerMission; // Reference to the TimerMission script
    private Missions[] mission = new Missions[7];

    /*private MissionAvoidProjectiles missionAvoidProjectiles; // SCRIPT AVOID PROJECTILES 0
    private MissionCollectCoin missionCollectCoin; // SCRIPT COLLECT THE COIN 1
    private MissionCopyMovement missionCopyMovement; // SCRIPT COPY THE MOVEMENT 2
    private MissionDontMove missionDontMove; // SCRIPT DONT MOVE 3
    private MissionMove missionMove; // SCRIPT MOVE 4
    private MissionPushRival missionPushRival; // SCRIPT PUSH THE RIVAL 5*/
    private MissionStayAwayBomb missionStayAwayBomb; // SCRIPT STAY AWAY FROM THE BOMB 6 
   // private MissionStaySquare missionStaySquare; // SCRIPT STAY IN THE GREEN SQUARE 7
    
    /// <summary>
    /// Controller the Missions
    /// </summary>

    int randomNumber = -1; // Variable to store the random number
    int random = -1;
    [SerializeField] private float[] timerForStartTheMission = new float[7]; // Array to store time for each mission
    [SerializeField] private float[] timeCompleteMission = new float[7]; // Array to store time for each mission

    [SerializeField] private bool isFinishWait, isFinishMission;

    /// <summary>
    /// Beginning
    /// </summary>
    
    private float beginning =  0;


    private NetworkRunner runner;

    #region GetComponents
    void Awake()
    {
        timerMission = GetComponent<TimerMission>(); // Get the TimerMission component attached to the same GameObject
        
        mission[0] = GetComponentInChildren<MissionAvoidProjectiles>();
        mission[1] = GetComponentInChildren<MissionCollectCoin>();
        mission[2] = GetComponentInChildren<MissionCopyMovement>();
        mission[3] = GetComponentInChildren<MissionDontMove>();
        mission[4] = GetComponentInChildren<MissionMove>();
        //mission[5] = GetComponentInChildren<MissionPushRival>();
        mission[5] = GetComponentInChildren<MissionStayAwayBomb>();
        mission[6] = GetComponentInChildren<MissionStaySquare>();

        

        //missionStayAwayBomb = GetComponentInChildren<MissionStayAwayBomb>();
    }



    #endregion GetComponent


    


    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < mission.Length; i ++)
        {
            mission[i].enabled = false;
        }

        print("Beginning the draw");
        Invoke("Draw", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        ActiveTheMission();
        DesactiveMission();
    }

    #region CanvaText
    //[Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_SeeTextMission(PlayerRef player, string text)
    {
        if (!HasStateAuthority) return;

        if (!playerTextSee.TryGet(player, out string currentText))
            currentText = taskSunSays[0] + nameTheMission[randomNumber];

        playerTextSee.Set(player, currentText);
        Debug.Log($"O texto é agora {currentText}");

        // Atualiza todos os clientes
        RPC_UpdateTextMission();
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    private void RPC_UpdateTextMission()
    {
        // Dispara evento para atualizar UIs
        UpdateAllUIs();
    }
    private void UpdateAllUIs()
    {
        var textSunUIs = FindObjectsOfType<SunSaysUi>();
        foreach (var ui in textSunUIs)
        {
            ui.UpdateRankingUI(taskSunSays[0] + nameTheMission[random]);
        }
    }

    #endregion CanvaText

    #region Draw
    void Draw()
    {
        while (random == randomNumber)
        {
            random = Random.Range(0, 7); // Generate a random number between 0 and 9
        }
        
        randomNumber = random;
        SetupTM();
    }
#endregion Draw

#region Time
    void SetupTM()
    {
        for(byte index = 0; index < timerForStartTheMission.Length; index++)
        {
            if (index == randomNumber){

                timerMission.InitializeTimeToGet(timerForStartTheMission[index], timeCompleteMission[index], this);

                painelText.SetActive(true);

                /*foreach(var player in PlayerController)
                {
                    if (player != null)
                    {
                        RPC_SeeTextMission(player, taskSunSays[0] + nameTheMission[index]);
                    }
                }*/
                //textPainel.text = (taskSunSays[0] + nameTheMission[index]).ToString();
                Debug.Log(taskSunSays[0] + nameTheMission[index]);
            }
        }
    }
    void ActiveTheMission()
    {
        if (isFinishWait)
        {
            painelText.SetActive(false);

            mission[randomNumber].enabled = true;
            mission[randomNumber].CallStartMission();
            isFinishWait = false;
        }
    }

    void DesactiveMission(){
        if (isFinishMission)
        {
            mission[randomNumber].CallCompleteMission();

            GameChecker gameChecker = FindAnyObjectByType<GameChecker>();
            Debug.Log("GameChecker: " + gameChecker);
            gameChecker.CheckPlayersInTheEndOfMission((sbyte)randomNumber);

            isFinishMission = false;
            mission[randomNumber].enabled = false;

            //--------------------------------------------------------------------------------------------------------------------------------//
            //-----------Envia os comandos para todos os player resetarem os valores dos booleanos-----------//
            //---------------------------------------------------------------//

            
            runner = FindObjectOfType<NetworkRunner>(); //Pega o NetworkRunner na cena
            Debug.Log(runner + "EXISTE");
            Debug.Log("Quantidade de players ativos: " + runner.ActivePlayers.Count());


            foreach (var player in runner.ActivePlayers)
            {
                Debug.Log("Checando player: " + player);

                var networkObject = runner.GetPlayerObject(player); //Percorre os objetos de rede ativos (Players)

                Debug.Log("NETWORKOBJECT VAZIO??: " + networkObject);
                if (networkObject != null) //Verifica se o objeto de rede não é nulo
                {
                    Debug.Log("EXISTE O NETWORKOBJECT");

                    PlayerController playerController = networkObject.GetComponent<PlayerController>(); //Pega o script PlayerController do objeto de rede

                    if (playerController != null)
                    {
                        playerController.missionProjectile = false; // Reseta a missão do player
                        playerController.missionCollectCoin = false; // Reseta a missão do player
                        playerController.missionCopyMoviment = false; // Reseta a missão do player
                        playerController.missionDontMove = false; // Reseta a missão do player
                        playerController.missionMove = false; // Reseta a missão do player
                        playerController.missionPushRival = false; // Reseta a missão do player
                        playerController.missionBomb = false; // Reseta a missão do player
                        playerController.missionStaySquare = false; // Reseta a missão do player

                        playerController.timeToCopyTheMovements = false; // Reseta o tempo para copiar os movimentos
                        playerController.copyThisMovement = new byte[4]; // Limpa a lista de movimentos copiados
                        playerController.listCopyThisMovement.Clear(); // Limpa a lista de movimentos copiados

                        playerController.dontMove = false; // Reseta o booleano
                        playerController.move = false; // Reseta o booleano
                        playerController.moveu = false; // Reseta o booleano

                        Debug.Log("APAGOU TUDO DE TODOS");
                    }
                }
            }

            Draw();
        }
    }
    public void SetupBegin(bool fStart){
        isFinishWait = fStart;
    }

    public void SetupConclusion(bool fWait){
        isFinishMission = fWait;
    }
#endregion Time
}
