using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VOICE : MonoBehaviour
{
    [SerializeField] TextMeshPro texto;
    public void setVoiceLine(string voiceLine, bool isUnsettling)
    {
        Color col = Color.white;
        if (isUnsettling) col = Color.red;
        texto.color = col;
        texto.text = voiceLine;
    }
}
