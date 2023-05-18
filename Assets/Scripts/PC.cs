using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.0f; //Target movement speed
    [SerializeField] private float moveAccel = 0.4f; //Acceleration to full speed in seconds
    private float curMove; //Current movement speed
    public float moveMod = 1.0f; //Used for debuffs/buffs
    public float moveDir = 0.0f;
    private CO co;
    void Start()
    {
        co = FindObjectOfType<CO>();
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        moveDir = Input.GetAxis("Horizontal");
        float targetmove = moveSpeed * moveDir * moveMod;
        float accel = (moveSpeed * Time.deltaTime) / moveAccel;
        if (curMove != targetmove)
        {
            if (Mathf.Abs(curMove - targetmove) < accel) { curMove = targetmove; }
            else if (curMove < targetmove) { curMove += accel; }
            else { curMove -= accel; }
        }
        transform.position += new Vector3(curMove*Time.deltaTime,0,0);
    }
}
