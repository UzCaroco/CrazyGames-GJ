using Fusion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;
using static UnityEngine.ParticleSystem;

public class SunSaysUi : MonoBehaviour
{
    private PlayerRef playerTextSunSays;
    [SerializeField] private TextMeshProUGUI textPainel;
    public void UpdateRankingUI(string textToSay)
    {
        textPainel.text = textToSay.ToString();

    }
}
