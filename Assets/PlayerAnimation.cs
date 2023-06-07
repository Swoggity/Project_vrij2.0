using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private Animator mAnimator;

    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if(mAnimator != null)
        {
            if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.C))
            {
                //Running = naar voren rennen
                //RunningBack is naar achteren rennen
                mAnimator.SetBool("Running", true);
               if (Input.GetKey(KeyCode.C))
                {
                     mAnimator.SetBool("RunningBack", false);
                }
               if (Input.GetKey(KeyCode.Z))
                {
                    mAnimator.SetBool("RunningBack", true);
                }
            }
            else
            {
                mAnimator.SetBool("RunningBack", false);
                mAnimator.SetBool("Running", false);
            }
        }
    }
}
