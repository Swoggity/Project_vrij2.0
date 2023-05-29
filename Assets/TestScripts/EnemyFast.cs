using UnityEngine;

public class EnemyFast : Enemy
{

    public override void Die()
    {
        Debug.Log("Fast defeated!");
        Destroy(gameObject);
    }
}