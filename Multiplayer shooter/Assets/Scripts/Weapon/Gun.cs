using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Gun : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private Transform _shootPoint;
 
    private Animator _anim;
    private GameObject _bulletContainer;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _bulletContainer = new GameObject("Bullets");
    }
    public void Shoot(string shooterName)
    {
        Bullet bullet = Instantiate(_bullet);
        bullet.transform.parent = _bulletContainer.transform;

        if (transform.localScale.x > 0) 
            bullet.Init(_bulletSpeed, transform.right, shooterName);
        else bullet.Init(_bulletSpeed, transform.right * -1, shooterName);

        bullet.transform.position = _shootPoint.position;

        _anim.SetTrigger("Shoot");
    }

    public void Drop()
    {
        gameObject.SetActive(false);
    }
}
