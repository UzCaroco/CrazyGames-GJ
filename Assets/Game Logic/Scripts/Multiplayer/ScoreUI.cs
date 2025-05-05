using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI first, second, third;
    [SerializeField] private PlayerRef trackedPlayer;
    [SerializeField] TextMeshProUGUI missionText; // Novo campo para a miss�o

    private void OnEnable()
    {
        GameManager.OnScoreChanged += HandleScoreChanged;
        GameManager.OnMissionChanged += HandleMissionChanged; // Novo handler
    }

    private void OnDisable()
    {
        GameManager.OnScoreChanged -= HandleScoreChanged;
        GameManager.OnMissionChanged -= HandleMissionChanged; // Novo handler
    }

    private void HandleScoreChanged(PlayerRef _, int __)
    {
        UpdateRankingUI();
    }

    public void UpdateRankingUI()
    {
        var gameManager = FindObjectOfType<GameManager>();
        var rankedList = FindObjectOfType<GameManager>().GetRankedList(); // ou guarda uma refer�ncia ao GameManager

        if (rankedList.Count > 0)
            first.text = $"1�: {rankedList[0].Item1.PlayerId} - {rankedList[0].Item2}";

        if (rankedList.Count > 1)
            second.text = $"2�: {rankedList[1].Item1.PlayerId} - {rankedList[1].Item2}";

        if (rankedList.Count > 2)
            third.text = $"3�: {rankedList[2].Item1.PlayerId} - {rankedList[2].Item2}";


        // Atualiza miss�o
        UpdateMissionUI(gameManager.GetCurrentMission());
    }

    


    // Novo m�todo para atualizar a miss�o
    private void HandleMissionChanged(string newMission)
    {
        UpdateMissionUI(newMission);
    }

    public void UpdateMissionUI(string mission)
    {
        missionText.text = mission;
        //missionText.text = $"Miss�o: {mission}";
    }

}