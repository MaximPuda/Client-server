using UnityEngine;

[RequireComponent(typeof(Animator), typeof(AudioSource))]
public class Gun : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _shootDelay;
    [SerializeField] private Transform _shootPoint;
 
    private Animator _anim;
    private AudioSource _sound;
    private GameObject _bulletContainer;

    private float _timeBetweenShoots = 0;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _sound = GetComponent<AudioSource>();
        _bulletContainer = new GameObject("Bullets");
    }

    private void Update()
    {
        _timeBetweenShoots += Time.deltaTime;
    }

    public void Shoot(string shooterName)
    {
        if (_timeBetweenShoots >= _shootDelay)
        {
            Bullet bullet = Instantiate(_bullet);
            bullet.transform.parent = _bulletContainer.transform;

            if (transform.localScale.x > 0)
                bullet.Init(_bulletSpeed, transform.right, shooterName);
            else bullet.Init(_bulletSpeed, transform.right * -1, shooterName);

            bullet.transform.position = _shootPoint.position;

            _anim.SetTrigger("Shoot");

            _sound.pitch = Random.Range(1f, 1.5f);
            _sound.Play();

            _timeBetweenShoots = 0;
        }
    }

    public void Drop()
    {
        gameObject.SetActive(false);
    }
}
