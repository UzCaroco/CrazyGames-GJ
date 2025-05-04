using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CrazyGames;
using Fusion;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SunController : NetworkBehaviour
{
    /// <summary>
    /// Scipts to Control the Missions
    /// </summary>/
    /// 

    // Text Mission por jogador
    
    [SerializeField] private TextMeshProUGUI textPainel;

    //[Networked(OnChanged = nameof(OnTextChanged))]
    private NetworkString<_256> playerTextSee { get; set; }   
    public static SunController local { get; set; }

    [SerializeField] private SunSaysUi sharedUITextInstance;
    
    //SunSaysUi uiSun;
    //private PlayerRef playerTextSunSays; 

    [SerializeField] private GameObject painelText;

    private string[] taskSunSays = new string[2] { "Sun Says:\n", "Sun Don't Says:\n"};
    private string[] nameTheMission = new string[7] { "Avoid The Projectiles", "Collect a Coin", "Copy the Movement", "Don't Move" , "Move" /*, "Push a Rival" */, "Stay Away From the Bomb" , "Go to the Square"};
    public string message;


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

        sharedUITextInstance = FindObjectOfType<SunSaysUi>();
        runner = FindObjectOfType<NetworkRunner>();

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

    public override void FixedUpdateNetwork()
    {
        //GetSomeStats();
    }
    #region CanvaText
    //[Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    /* public void RPC_SeeTextMission(PlayerRef player, string text)
     {
         if (!HasStateAuthority) return;

         playerTextSee.Set(player, text);
         Debug.Log($"O texto é agora {text}");

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
     }*/
    
    
    /*void GetSomeStats()
    {
        if(Object.HasInputAuthority && sharedUITextInstance != null)
        {
            sharedUITextInstance.SetConnectionType(runner.CurrentConnectionType.ToString());
            sharedUITextInstance.SetRtt($"RTT {Mathf.RoundToInt((float)runner.GetPlayerRtt(Object.InputAuthority) * 100)} ms");
        }
    }*/

    /*static void OnTextChanged(Changed<SunController> changed)
    {
        changed.Behaviour.OnTextChanged();
    }

    void OnTextChanged()
    {
        Debug.Log($"Text Sun Chaged to {playerTextSee}");

        textPainel.text = playerTextSee.ToString();
    }*/

    public void ApplyUI(string text)
    {
        RPC_UpdateUIText(text);
    }


    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    void RPC_UpdateUIText(string text, RpcInfo info = default)
    {
        Debug.Log($"[RPC] RPX_ApllayText {playerTextSee}");
        playerTextSee = text;
        textPainel.text = text;

        if (sharedUITextInstance != null)
        {
            sharedUITextInstance.SetMessage(text);
        }
    }

    #endregion CanvaText

    #region Draw
    void Draw()
    {
        while (random == randomNumber)
        {
            random = Random.Range(5, 7); // Generate a random number between 0 and 9
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
                message = taskSunSays[0] + nameTheMission[index];
                /*foreach (var player in runner.ActivePlayers)
                {
                    if (player != null)
                    {
                        PlayerRef playerRefs = Object.InputAuthority;
                                
                        if (playerRefs != null)
                        {
                            RPC_SeeTextMission(playerRefs, taskSunSays[0] + nameTheMission[index]);
                        }
                    }
                }*/

                ApplyUI(taskSunSays[0] + nameTheMission[index]);

                //OnTextChanged();

                Debug.Log(taskSunSays[0] + nameTheMission[index]);
                /*sharedUITextInstance.SetMessage(message);
                sharedUITextInstance.SetMessage(taskSunSays[0] + nameTheMission[index]);
                textPainel.text = (taskSunSays[0] + nameTheMission[index]).ToString();*/

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
