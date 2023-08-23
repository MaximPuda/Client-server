using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _damage;
    
    private float _speed;
    private Vector2 _direction;
    private string _shooter;

    public void Init(float speed, Vector2 direction, string shooterName)
    {
        _direction = direction;
        _speed = speed;
        _shooter = shooterName;
    }

    private void Update()
    {
        if (_direction != Vector2.zero) 
        { 
            transform.Translate(_direction * _speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == _shooter) return;

        if (collision.transform.TryGetComponent<PlayerController>(out PlayerController player))
            player.GetDamage(_damage);

        Destroy(gameObject);
    }
}
