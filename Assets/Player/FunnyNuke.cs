using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunnyNuke : MonoBehaviour
{
    private static readonly KeyCode[] konamiCode = {
        KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.DownArrow,
        KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.RightArrow,
        KeyCode.B, KeyCode.A
    };

    private int konamiCodeIndex = 0;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(konamiCode[konamiCodeIndex]))
            {
                konamiCodeIndex++;

                if (konamiCodeIndex == konamiCode.Length)
                {
                    ActivateEasterEgg();
                    konamiCodeIndex = 0;
                }
            }
            else
            {
                konamiCodeIndex = 0;
            }
        }
    }

    private void ActivateEasterEgg()
    {
        // Get all game objects with IDamageable component
        IDamageable[] damageables = FindObjectsOfType<Enemy>();

        // Apply damage to each damageable object
        foreach (IDamageable damageable in damageables)
        {
            damageable.TakeDamage(1000);
        }
    }
}
