using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Fusion;
using UnityEngine;

public class SunController : NetworkBehaviour
{
    /// <summary>
    /// Scipts to Control the Missions
    /// </summary>/
    /// 
    private TimerMission timerMission; // Reference to the TimerMission script
    private Missions[] mission = new Missions[8];

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
    [SerializeField] private float[] timerForStartTheMission = new float[8]; // Array to store time for each mission
    [SerializeField] private float[] timeCompleteMission = new float[6]; // Array to store time for each mission

    [SerializeField] private bool isFinishWait, isFinishMission;

    /// <summary>
    /// Beginning
    /// </summary>
    
    private float beginning =  0;

#region GetComponents
    void Awake()
    {
        timerMission = GetComponent<TimerMission>(); // Get the TimerMission component attached to the same GameObject
        
        mission[0] = GetComponentInChildren<MissionAvoidProjectiles>();
        mission[1] = GetComponentInChildren<MissionCollectCoin>();
        mission[2] = GetComponentInChildren<MissionCopyMovement>();
        mission[3] = GetComponentInChildren<MissionDontMove>();
        mission[4] = GetComponentInChildren<MissionMove>();
        mission[5] = GetComponentInChildren<MissionPushRival>();
        mission[6] = GetComponentInChildren<MissionStayAwayBomb>();
        mission[7] = GetComponentInChildren<MissionStaySquare>();

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
    
#region Draw
    void Draw()
    {
        while (random == randomNumber)
        {
            random = Random.Range(0, 2); // Generate a random number between 0 and 9
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
            }
        }
    }
    void ActiveTheMission()
    {
        if (isFinishWait)
        {
            mission[randomNumber].enabled = true;
            isFinishWait = false;
        }
    }

    void DesactiveMission(){
        if (isFinishMission)
        {
            mission[randomNumber].enabled = false;
            isFinishMission = false;

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
