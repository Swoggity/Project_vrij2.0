using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{

    BaseEnemy currentState;
    public EnemyRoaming RoamingState = new EnemyRoaming();
    public EnemyChasing ChasingState = new EnemyChasing();
    public EnemyRetreating RetreatingState = new EnemyRetreating();
    public EnemyDying DyingState = new EnemyDying();

    // The amount of health that the object has
    public int health = 100;
    // The Slider object
    private Slider slider;

    private WaveSpawner waveSpawner;

    private bool dead = false;

    // Start is called before the first frame update
    void Start()
    { 
        slider = GetComponentInChildren(typeof(Slider)) as Slider;

        currentState = RoamingState;

        currentState.EnterState(this);

        waveSpawner = GameObject.FindObjectOfType<WaveSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this, health);
    }

    public void SwitchState(BaseEnemy state)
    {
        currentState = state;
        state.EnterState(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the other collider has the "bullet" tag
        if (other.tag == "PlayerBullet")
        {
            // Decrease the object's health by 10
            health -= 10;
            slider.value = health;
        }
        if (health <= 0 && dead == false)
        {
            waveSpawner.EnemyDestroyed();
            dead = true;
        }
    }

}
