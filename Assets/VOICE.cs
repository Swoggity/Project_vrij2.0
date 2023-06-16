using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VOICE : MonoBehaviour
{
    [SerializeField] TextMeshPro texto;
    float disTime = 5f;
    float fade = 1.0f;

    public void setVoiceLine(string voiceLine, bool isUnsettling)
    {
        Color col = Color.white;
        if (isUnsettling) col = Color.red;
        texto.color = col;
        texto.text = voiceLine;
    }

    private void Update()
    {
        disTime = Time.deltaTime;
        if (disTime < 0) Destroy(gameObject);
        if (disTime < 2)
        {
            fade -= Time.deltaTime * 0.5f;
            texto.color = new Color(1, 1, 1, fade);
        }
    }
}
