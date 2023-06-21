using System.Collections.Generic;
using UnityEngine;

public class EnemyFast : Enemy
{
    private float playerPosition;
    public override void Die()
    {
        Quaternion rotation = Quaternion.Euler(-90f, 0f, 0f);
        Instantiate(DeathEffect, transform.position, rotation);
        Destroy(gameObject);
    }
    private void Update()
    {
        if (co.isGamePaused()) return;
        if (!isObstacleDetected)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            doVoiceline();
        }
        DetectObstacle();
        playerPosition = playerObject.transform.position.x;
    }

    private void DetectObstacle()
    {
        if (co.becomeAlly)
        {
            isObstacleDetected = false;
            return;
        }
        if (transform.position.x <= playerPosition + adjustedDetectionDistance)
        {
            isObstacleDetected = true;
        }
        else if (transform.position.x >= playerPosition + adjustedDetectionDistance + 0.7f)
        {
            isObstacleDetected = false;
        }
    }

    private void doVoiceline()
    {
        if (carryVoice < 2) return;
        if (transform.position.x <= playerPosition + adjustedDetectionDistance +2)
        {
            string[] voice = {""};
            Debug.Log("Doing voice!");
            switch (carryVoice)
            {
                case 2:
                    //Generic
                    voice = new string[]
                    {
                        "Regime scum!",
                        "Down with the government!",
                        "We fight for our rights!",
                        "For justice!",
                        "We will not surrender!",
                        "Our cause is true!",
                        "Don't look down on us!",
                        "Aaaaaa!"
                    };
                    break;
                case 3:
                    //Confronting
                    voice = new string[]
                    {
                        "Please stop!",
                        "Stop shooting!",
                        "Don't hurt us!",
                        "We want only peace!",
                        "Stop!",
                        "Leave us alone!"
                    };
                    break;
            }
            int rr = Mathf.FloorToInt(Random.Range(0, voice.Length));
            Instantiate(Resources.Load<VOICE>("VOICE"),transform.position+new Vector3(0,5,0),transform.rotation).setVoiceLine(voice[rr],carryVoice == 3); //Load Voice prefab

            carryVoice -= 2; //2 becomes 0, 3 becomes 1
        }
    }

}