using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityAnimator : MonoBehaviour
{
    private Animator mAnimator;
    private AbilityManager abilityManager;

    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponentInChildren<Animator>();
        abilityManager = FindObjectOfType<AbilityManager>();

        // Subscribe to the ability unlocked and cooldown events
        if (abilityManager != null)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (mAnimator != null && abilityManager != null)
        {

        }
    }

}

