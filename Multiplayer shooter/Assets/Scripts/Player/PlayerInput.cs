using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float _deadZone;

    public UnityAction<Vector2> MoveEvent;
    public UnityAction FireEvent;

    private Vector2 _direction;


    private void Update()
    {
        _direction.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (_direction.magnitude >= _deadZone)
            MoveEvent?.Invoke(_direction);

        if (Input.GetKeyDown(KeyCode.Space))
            FireEvent?.Invoke();
    }
        
}
