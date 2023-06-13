using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private float speed = 10f;
    private int damage;
    private GameObject hitMarker;

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            Instantiate(hitMarker, new Vector3(transform.position.x + 0.1f, transform.position.y, 0), Quaternion.identity);
            damageable.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
    public void SetDamage(int damageValue, float speedValue, GameObject hitMarkerGameObject)
    {
        damage = damageValue;
        speed = speedValue;
        hitMarker = hitMarkerGameObject;
    }
}