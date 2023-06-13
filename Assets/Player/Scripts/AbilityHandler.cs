/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHandler : MonoBehaviour
{
    private Animator mAnimator;

    public void UseAbility(int ability)
    {
        switch (ability)
        {
            case 1:

                // Code for ability 1
                Debug.Log("ability one");
                Transform abilityChild1 = transform.Find("Ability1");
                if (abilityChild1 != null)
                {
                    mAnimator = abilityChild1.GetComponent<Animator>();
                    if (mAnimator != null)
                    {
                        mAnimator.SetBool("CdActive", true);
                    }
                }
                else
                {
                    Debug.LogError("Ability1 child GameObject not found.");
                }
                break;
            case 2:

                // Code for ability 2
                Debug.Log("ability two");
                Transform abilityChild2 = transform.Find("Ability2");
                if (abilityChild2 != null)
                {
                    mAnimator = abilityChild2.GetComponent<Animator>();
                    if (mAnimator != null)
                    {
                        mAnimator.SetBool("CdActive", true);
                    }
                }
                else
                {
                    Debug.LogError("Ability1 child GameObject not found.");
                }
                break;
            case 3:

                // Code for ability 3
                Debug.Log("ability 3");
                Transform abilityChild3 = transform.Find("Ability3");
                if (abilityChild3 != null)
                {
                    mAnimator = abilityChild3.GetComponent<Animator>();
                    if (mAnimator != null)
                    {
                        mAnimator.SetBool("CdActive", true);
                    }
                }
                else
                {
                    Debug.LogError("Ability1 child GameObject not found.");
                }
                break;
            case 4:

                // Code for ability 4
                Debug.Log("ability 4");
                Transform abilityChild4 = transform.Find("Ability4");
                if (abilityChild4 != null)
                {
                    mAnimator = abilityChild4.GetComponent<Animator>();
                    if (mAnimator != null)
                    {
                        mAnimator.SetBool("CdActive", true);
                    }
                }
                else
                {
                    Debug.LogError("Ability1 child GameObject not found.");
                }
                break;
            default:

                // Code for handling invalid ability values
                break;
        }
    }
}

//fucking hell this code stinks but my brain is fried */