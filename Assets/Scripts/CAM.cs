using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAM : MonoBehaviour
{
    PC player;
    [SerializeField] private float LeadAhead = 4f;
    [SerializeField] private float accelMove = 6f; //Acceleration per second
    private float curMove = 0.0f;
    void Start()
    {
        player = FindObjectOfType<PC>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        float targetX = Mathf.Clamp(player.transform.position.x+(player.moveDir*LeadAhead),0f,100f);
        float accel = accelMove * Time.deltaTime;
        int accelMod = 0;
        float dist = Mathf.Abs(transform.position.x - targetX);
        if (targetX > transform.position.x) { 
            accelMod = 1;
            float deaccelTime = curMove / accelMove; //Time taken to deaccelerate to zero
            float deaccelDist = deaccelTime * (curMove/2); //Distance taken while deaccelerating
            if (dist < deaccelDist && curMove > 0) { //Check if deacceleration should begin
                //If deaccelerating now would cause the camera to exactly reach its destination
                accelMod *= -1; //Reverse direction
            }
        }
        else if (targetX < transform.position.x) { 
            accelMod = -1;
            float deaccelTime = curMove / accelMove; //Time taken to deaccelerate to zero
            float deaccelDist = deaccelTime * (curMove / 2); //Distance taken while deaccelerating
            if (dist < deaccelDist && curMove < 0)
            { //Check if deacceleration should begin
                //If deaccelerating now would cause the camera to exactly reach its destination
                accelMod *= -1; //Reverse direction
            }
        }
        else { curMove = 0.0f; return; }
        curMove += accel * accelMod;
        float move = curMove * Time.deltaTime;
        if (dist < move*2) { transform.position = new Vector3(targetX, transform.position.y, transform.position.z); }
        else { transform.position += new Vector3(move, 0, 0); }
    }
}
