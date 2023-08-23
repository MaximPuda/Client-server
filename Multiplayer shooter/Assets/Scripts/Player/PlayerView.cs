using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class PlayerView : MonoBehaviour
{
    [SerializeField] private PlayerController _controller;
    [SerializeField] private ParticleSystem _blood;
    
    private Animator _anim;
    private SpriteRenderer _renderer;

    private bool _needFlip;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        if (_controller != null)
        {
            _controller.MoveEvent += OnMove;
            _controller.DeathEvent += OnDeath;
            _controller.GetDamageEvent += OnGetDamage;

        }
    }

    private void OnDisable()
    {
        if (_controller != null)
        {
            _controller.MoveEvent -= OnMove;
            _controller.DeathEvent -= OnDeath;
            _controller.GetDamageEvent -= OnGetDamage;
        }
    }

    private void OnMove(Vector2 direction)
    {

        _anim.SetFloat("Speed", direction.magnitude);

        //Flip sprites
        if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (direction.x > 0) { transform.localScale = new Vector3(1, 1, 1); }
    }

    private void OnDeath()
    {
        _anim.SetTrigger("Death");
    }

    private void OnGetDamage(float value)
    {
        if (value < 1)
        _blood.Play();
    }
}
