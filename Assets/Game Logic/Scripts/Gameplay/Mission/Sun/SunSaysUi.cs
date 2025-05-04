using Fusion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SunSaysUi : NetworkBehaviour
{
    [Networked] public string Message { get; set; }
    [SerializeField] private TextMeshProUGUI textPainel;

    public override void Render()
    {
        // Sempre atualiza o texto na UI com a versão de rede
        if (textPainel != null)
        {
            textPainel.text = Message;
        }
    }

    public void SetMessage(string newMessage)
    {
        if (HasStateAuthority)
        {
            Debug.Log("SunController Has Authotity");
            Message = newMessage;
        }
    }
}
