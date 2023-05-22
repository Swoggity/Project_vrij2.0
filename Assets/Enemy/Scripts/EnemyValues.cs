using UnityEngine;

public interface EnemyValues 
{
    int Health { get; set; }

    void TakeDamage(int damage);
}
