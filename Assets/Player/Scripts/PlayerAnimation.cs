using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator mAnimator;
    private PlayerShoot PlayerShoot;
    public float moveSpeedBackMod = 0.4f;
    private int randomShoot;
    public bool isMainPlayer = true;
    CO co;

    void Start()
    {
        co = FindObjectOfType<CO>();
        mAnimator = GetComponent<Animator>();
        PlayerShoot = GetComponentInParent<PlayerShoot>();
    }

    void Update()
    {
        if(mAnimator != null)
        {
            float moving = 0;
            if (!co.isGamePaused() && isMainPlayer)
            {
                if (Input.GetKey(KeyCode.LeftArrow)) moving = -moveSpeedBackMod;
                if (Input.GetKey(KeyCode.RightArrow)) moving = 1;
                if (Input.GetKeyDown(KeyCode.Alpha1) && co.abilitiesUnlocked > 0 && !co.becomeAlly && !Input.GetKey(KeyCode.Space)) mAnimator.SetTrigger("Parry");
            }
            else
            {
                moving = co.MoveOverride();
            }
            if (moving != 0)
            {
                //Running = naar voren rennen
                //RunningBack is naar achteren rennen
                mAnimator.SetBool("Running", true);
                if (moving > 0)
                {
                     mAnimator.SetBool("RunningBack", false);
                }
                else
                {
                    mAnimator.SetBool("RunningBack", true);
                }
            }
            else
            {
                mAnimator.SetBool("RunningBack", false);
                mAnimator.SetBool("Running", false);
            }
            if (!isMainPlayer) return;
            if (PlayerShoot.isFiring)
            {
                mAnimator.SetBool("Shooting", true);
                randomShoot = Random.Range(0, 2);
                mAnimator.SetInteger("ShootRandom", randomShoot);
            }
            else
            {
                mAnimator.SetBool("Shooting", false);
            }
        }
    }
}
