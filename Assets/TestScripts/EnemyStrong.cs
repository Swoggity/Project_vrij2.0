using UnityEngine;

public class EnemyStrong : Enemy
{
    public override void Die()
    {
        Debug.Log("Strong defeated!");
        Destroy(gameObject);
    }
}