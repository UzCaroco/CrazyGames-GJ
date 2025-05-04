using TMPro;
using UnityEngine;

public class SunSaysUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textSays;
    [SerializeField] private TextMeshProUGUI textMission;
    [SerializeField] private GameObject painelText;

    private void OnEnable()
    {
        GameManager.OnNewMission += UpdateMissionUI;
    }

    private void Start()
    {
        GameManager.OnNewMission += UpdateMissionUI;

        var gm = FindObjectOfType<GameManager>();
        if (gm != null && gm.HasCurrentMission)
            UpdateMissionUI(gm.CurrentSays, gm.CurrentMission);
    }



    private void OnDisable()
    {
        GameManager.OnNewMission -= UpdateMissionUI;
    }

    private void UpdateMissionUI(string says, string mission)
    {
        textSays.text = says;
        textMission.text = mission;
        painelText.SetActive(true);
    }
}
