using Fusion;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private Text scoreText;
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
            scoreText.text = $"Score: {score}";
        }
    }
}