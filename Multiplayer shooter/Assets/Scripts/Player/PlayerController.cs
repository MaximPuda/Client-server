using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerInput), typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _startLife = 1;
    [SerializeField] private float _speed = 20f;
    [SerializeField] private Gun _gun;

    public UnityAction<Vector2> MoveEvent;
    public UnityAction<float> GetDamageEvent;
    public UnityAction<int> GetCoinEvent;
    public UnityAction DeathEvent;

    private PlayerInput _input;
    private Vector2 _direction;
    private Collider2D _collider;

    private float _currentLife;
    private int _currentCoins;

    #region Unity functions
    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _collider = GetComponent<Collider2D>();
        _currentLife = _startLife;
    }

    private void Start()
    {
        GetDamageEvent?.Invoke(_currentLife);
        GetCoinEvent?.Invoke(_currentCoins);
    }

    private void OnEnable()
    {
        _input.MoveEvent += SetDirection;
        _input.FireEvent += Shoot;
    }

    private void OnDisable()
    {
        _input.MoveEvent -= SetDirection;
        _input.FireEvent -= Shoot;
    }

    private void Update()
    {
        Move();
        Aim();
    }
    #endregion

    private void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    private void Move()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
        MoveEvent?.Invoke(_direction);
    }

    private void Aim()
    {
        Vector2 aim = _direction.normalized;

        if(aim.x < 0)
        {
            _gun.transform.localScale = new Vector3(-1, 1, 1);
            _gun.transform.right = new Vector2 (aim.x * -1, aim.y * -1);
        }
        else if (aim.x >= 0)
        {
            _gun.transform.right = aim;
            _gun.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void Shoot()
    {
        if(_gun != null)
            _gun.Shoot(gameObject.name);
    }

    public void GetDamage(float damage)
    {
        _currentLife -= damage;
        GetDamageEvent?.Invoke(_currentLife);

        if (_currentLife <= 0)
            Death();
    }

    public void GetCoin(int amount)
    {
        _currentCoins += amount;
        GetCoinEvent?.Invoke(_currentCoins);
    }

    private void Death()
    {
        enabled = false;
        _collider.enabled = false;

        _gun.Drop();

        DeathEvent?.Invoke();
        
        Debug.Log("Game over");
    }
}
