using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CrazyGames;
using ExitGames.Client.Photon.StructWrapping;
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

    [SerializeField] private TextMeshProUGUI textSays;
    [SerializeField] private TextMeshProUGUI textMission;

    //[Networked(OnChanged = nameof(OnTextChanged))]
    private NetworkString<_256> playerTextSee { get; set; }
    public static SunController local { get; set; }

    //[SerializeField] private SunSaysUi sharedUITextInstance;

    //SunSaysUi uiSun;
    //private PlayerRef playerTextSunSays; 

    //[SerializeField] private NetworkObject painelText;

    private string[] taskSunSays = new string[2] { "Sun Says:\n", "Sun Don't Says:\n" };
    private string[] nameTheMission = new string[7] { "Avoid The Projectiles", "Collect a Coin", "Copy the Movement", "Don't Move", "Move" /*, "Push a Rival" */, "Stay Away From the Bomb", "Go to the Square" };
    public string messageS;
    public string messageM;


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

    [Networked] int randomNum { get; set;}
    [Networked] int rand { get; set; }

    int randomNumber = -1; // Variable to store the random number
    int random = -1;

    [Networked] private float startMission { get; set; }
    [Networked] private float completeMission { get; set; }

    [SerializeField] private float[] timerForStartTheMission  = new float[7]; // Array to store time for each mission
    [SerializeField] private float[] timeCompleteMission = new float[7]; // Array to store time for each mission

    [Networked]public bool isFinishMission{ get; set; }
    [Networked][SerializeField] private bool isFinishWait { get; set; }

    /// <summary>
    /// Beginning
    /// </summary>
    
    private float beginning =  0;


    private NetworkRunner runner;

    #region GetComponents
    void Awake()
    {
        if (HasStateAuthority)
        {
           

            //missionStayAwayBomb = GetComponentInChildren<MissionStayAwayBomb>();
        }
    }

    #endregion GetComponent

    // Start is called before the first frame update
    void Start()
    {
       

    }

    public override void Spawned()
    {
        if (HasStateAuthority)
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

            //sharedUITextInstance = FindObjectOfType<SunSaysUi>();
            runner = FindObjectOfType<NetworkRunner>();

            base.Spawned();

            for (int i = 0; i < mission.Length; i++)
            {
                mission[i].enabled = false;
            }

            print("Beginning the draw");

            Invoke("Draw", 5f); // Só chame aqui, após o Spawned
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (HasStateAuthority)
        {
            ActiveTheMission();
            DesactiveMission();
        }
    }
    #region CanvaText
    
    public void ApplyUI(string textS, string textM)
    {
        //Luiz
        FindObjectOfType<GameManager>().GenerateNewMission(textS, textM);


        RPC_UpdateUIText(textS, textM);
    }


    //[Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    void RPC_UpdateUIText(string textS, string textM,  RpcInfo info = default)
    {
        Debug.Log($"[RPC] RANDOM É:{randomNum}");

        Debug.Log($"[RPC] RPX_ApllayText {playerTextSee}");
        playerTextSee = textS + textM;
        textSays.text = textS.ToString();
        textMission.text = textM.ToString();
        
    }

    #endregion CanvaText

    #region Draw
    void Draw()
    {
        if (HasStateAuthority)
        {
            while (random == randomNumber)
            {
                random = Random.Range(0, 7); // Generate a random number between 0 and 9
                rand = Random.Range(0, 7);
            }

            randomNum = rand;
            //Debug.Log("OIEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE AQUI O RANDOM É:" + randomNum);

            randomNumber = random;
            SetupTM();
        }
        
    }
#endregion Draw

#region Time
    void SetupTM()
    {
        for(byte index = 0; index < timerForStartTheMission.Length; index++)
        {
            if (index == randomNumber && HasStateAuthority){
                startMission = timerForStartTheMission[index];
                completeMission = timeCompleteMission[index];

                timerMission.InitializeTimeToGet(startMission, completeMission, this);

                //painelText.enabled = true; 
                messageS = taskSunSays[0];
                messageM = nameTheMission[index];
                ApplyUI(messageS, messageM);












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
            //painelText.enabled = false;

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
