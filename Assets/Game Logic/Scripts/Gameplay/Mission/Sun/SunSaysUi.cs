using CrazyGames;
using Fusion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SunSaysUi : NetworkBehaviour
{
    [Networked] public string MessageS { get; set; }
    [Networked] public string MessageM { get; set; }
    [SerializeField] private TextMeshProUGUI textPainel;

    
    /*public override void Render()
    {
        // Sempre atualiza o texto na UI com a versão de rede
        if (textPainel != null)
        {
            textPainel.text = Message.ToString();
        }
    }*/

    public void SetMessage(string newMessageS, string newMessagesM)
    {
        Debug.Log("SunController Has Authotity");
        MessageS = newMessageS;
        MessageM = newMessagesM;
    }
    

    
    /*public void SetConnectionType(string type)
    {
        connectionTypeText.text = $"Coonnection type: {type}";
    }

    public void SetRtt(string rtt)
    {
        rttText.text = rtt; 
    }

    public void OnApplyInGame()
    {
        Debug.Log("OnApplyGame clicked");

        SunController.local.ApplyUI(nameInputField.text);
    }
   
    */
}
