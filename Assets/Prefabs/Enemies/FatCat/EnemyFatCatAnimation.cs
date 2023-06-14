using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFatCatAnimation : MonoBehaviour
{
    private Animator mAnimator;
    private EnemyFatCat EnemyFatCat;

    void Start()
    {
        mAnimator = GetComponent<Animator>();
        EnemyFatCat = GetComponentInParent<EnemyFatCat>();
    }

    void Update()
    {
        if (mAnimator != null)
        {
            if (EnemyFatCat.isObstacleDetected)
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