using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float _deadZone;

    public UnityAction<Vector2> MoveEvent;
    public UnityAction<Vector2> AimEvent;
    public UnityAction FireEvent;

    private Camera _camera;

    private Vector2 _moveDirection;
    private Vector2 _aim;
    private Vector2 _mousePosition;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        _moveDirection.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (_moveDirection.magnitude >= _deadZone)
            MoveEvent?.Invoke(_moveDirection);

        if (Input.GetMouseButton(0))
            FireEvent?.Invoke();

        Aim();
    }

    private void Aim()
    {
        _mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        _aim = _mousePosition - new Vector2 (transform.position.x, transform.position.y);
        _aim.Normalize();

        AimEvent?.Invoke(_aim);
    }
        
}
