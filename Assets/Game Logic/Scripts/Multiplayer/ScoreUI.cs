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

    private void HandleScoreChanged(PlayerRef player, int score)
    {
        if (player == trackedPlayer)
        {
            first.text = $"Score: {score}";
        }
    }
}