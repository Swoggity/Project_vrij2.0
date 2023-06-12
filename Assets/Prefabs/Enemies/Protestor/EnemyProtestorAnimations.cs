using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProtestorAnimations : MonoBehaviour
{
    private Animator mAnimator;
    private EnemyFast EnemyFast;

    void Start()
    {
        mAnimator = GetComponent<Animator>();
        EnemyFast = GetComponentInParent<EnemyFast>();
    }

    void Update()
    {
        if (mAnimator != null)
        {
            if (EnemyFast.isObstacleDetected)
            {
                mAnimator.SetBool("Attacking", true);
            }
            else
            {
                mAnimator.SetBool("Attacking", false);
            }
        }
    }
}