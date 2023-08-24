using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerInput), typeof(Collider2D), typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _startLife = 1;
    [SerializeField] private float _speed = 20f;
    [SerializeField] private Gun _gun;

    [Header("Sounds")]
    [SerializeField] private AudioClip _death;

    public UnityAction<Vector2> MoveEvent;
    public UnityAction<Vector2> AimEvent;
    public UnityAction<float> GetDamageEvent;
    public UnityAction<int> GetCoinEvent;
    public UnityAction DeathEvent;

    private PlayerInput _input;
    private Collider2D _collider;
    private AudioSource _sound;
    
    private Vector2 _moveDirection;

    private float _currentLife;
    private int _currentCoins;

    #region Unity functions
    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _collider = GetComponent<Collider2D>();
        _sound = GetComponent<AudioSource>();
        _currentLife = _startLife;
    }

    private void Start()
    {
        GetDamageEvent?.Invoke(_currentLife);
        GetCoinEvent?.Invoke(_currentCoins);
    }

    private void OnEnable()
    {
        _input.MoveEvent += SetMoveDirection;
        _input.AimEvent += Aim;
        _input.FireEvent += Shoot;
    }

    private void OnDisable()
    {
        _input.MoveEvent -= SetMoveDirection;
        _input.AimEvent -= Aim;
        _input.FireEvent -= Shoot;
    }

    private void Update()
    {
        Move();
    }
    #endregion

    private void SetMoveDirection(Vector2 direction)
    {
        _moveDirection = direction;
    }

    private void Move()
    {
        transform.Translate(_moveDirection * _speed * Time.deltaTime);
        MoveEvent?.Invoke(_moveDirection);
    }

    private void Aim(Vector2 aim)
    {
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

        AimEvent?.Invoke(aim);
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

        _sound.pitch = Random.Range(1f, 1.3f);
        _sound.PlayOneShot(_death);

        DeathEvent?.Invoke();
        
        Debug.Log("Game over");
    }
}
