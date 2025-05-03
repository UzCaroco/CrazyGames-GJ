using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI first, second, third;
    [SerializeField] private PlayerRef trackedPlayer;

    private void OnEnable()
    {
        GameManager.OnScoreChanged += HandleScoreChanged;
    }

    private void OnDisable()
    {
        GameManager.OnScoreChanged -= HandleScoreChanged;
    }

    private void HandleScoreChanged(PlayerRef _, int __)
    {
        UpdateRankingUI();
    }

    public void UpdateRankingUI()
    {
        var rankedList = FindObjectOfType<GameManager>().GetRankedList(); // ou guarda uma referência ao GameManager

        if (rankedList.Count > 0)
            first.text = $"1º: {rankedList[0].Item1.PlayerId} - {rankedList[0].Item2}";

        if (rankedList.Count > 1)
            second.text = $"2º: {rankedList[1].Item1.PlayerId} - {rankedList[1].Item2}";

        if (rankedList.Count > 2)
            third.text = $"3º: {rankedList[2].Item1.PlayerId} - {rankedList[2].Item2}";
    }
}