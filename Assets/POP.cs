using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class POP : MonoBehaviour
{
    int ScoreWorth;
    [SerializeField] float fadeTime = 1f;
    float startSize;
    float fade = 1.0f;
    [SerializeField] float targetSize = 1f;
    [SerializeField] float shrinkSpeed = 2f;
    [SerializeField] float fontSize = 14f;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] TextMeshPro text;
    public void InitPop(int Score)
    {
        ScoreWorth = Score;
        float largeFactor = 0.5f+(Mathf.Sqrt(Mathf.Abs(Score))/20f); //Will be 1.0f at 100 Score

        fadeTime += largeFactor;
        fontSize *= largeFactor * Random.Range(0.8f,1.2f);
        moveSpeed *= Random.Range(0.8f, 1.2f);

        if (Score > 0)
        {
            text.color = new Color(largeFactor+0.1f, 0, largeFactor + 0.1f); //Purple color if S C O R E
        } else
        {
            text.color = new Color(largeFactor + 0.2f, 0, 0); //Red color if damage
        }
        text.fontSize = fontSize;
        text.text = Score.ToString();
        startSize = targetSize * 2.2f;
    }
    private void Update()
    {
        fadeTime -= Time.deltaTime;
        transform.position += new Vector3(0, moveSpeed, 0)*Time.deltaTime;
        if (startSize > targetSize)
        {
            startSize -= Time.deltaTime*shrinkSpeed;
            transform.localScale = new Vector3(startSize, startSize, 1.0f);
        }
        if ( fadeTime < 0f )
        {
            Destroy(gameObject);
        }
        else if (fadeTime < 1f)
        {
            fade -= Time.deltaTime;
            text.color = new Color(text.color.r,text.color.g,text.color.b,fade);
        }
    }
}
