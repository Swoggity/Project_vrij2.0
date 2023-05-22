using UnityEngine;

public abstract class BaseEnemy 
{

    public abstract void EnterState(EnemyManager Enemy);
    public abstract void UpdateState(EnemyManager Enemy, int health);
    public abstract void OnCollisionEnter(EnemyManager Enemy);
}
