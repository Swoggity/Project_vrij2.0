using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAbility : MonoBehaviour
{
    [SerializeField] private KeyCode key;
    [SerializeField] private float cooldown;
    [SerializeField] private PlayerAbilityUI icon;

    private float currentCooldown = 0f;

    void Update()
    {
        if(Input.GetKeyDown(key) && currentCooldown <= 0f)
        {
            StartCoroutine(Activate());

            currentCooldown = cooldown;
        }

        currentCooldown -= Time.deltaTime;
        if (currentCooldown < 0) currentCooldown = 0;

        icon.SetRatio(currentCooldown / cooldown);
    }

    protected abstract IEnumerator Activate();
}
