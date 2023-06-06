using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityUI : MonoBehaviour
{
    public void SetRatio(float ratio)
    {
        RectTransform overlay = GetComponent<RectTransform>();
        overlay.localScale = new Vector2(overlay.localScale.x, ratio);
    }
}
