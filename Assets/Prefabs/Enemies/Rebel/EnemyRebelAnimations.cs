using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRebelAnimations : MonoBehaviour
{
    private Animator mAnimator;
    private EnemyRebel EnemyRebel;

    void Start()
    {
        mAnimator = GetComponent<Animator>();
        EnemyRebel = GetComponentInParent<EnemyRebel>();
    }

    void Update()
    {
        if (mAnimator != null)
        {
            if (EnemyRebel.isObstacleDetected)
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