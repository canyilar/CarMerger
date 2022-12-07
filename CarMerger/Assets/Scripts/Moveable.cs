using System;
using UnityEngine;

public class Moveable : MonoBehaviour, IMoveable
{
    public event Action OnMovementEnded;

    private Rigidbody _rb;
    private bool _isMoving;
    private Vector3 _moveDirection;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            _rb.MovePosition(_moveDirection);
        }
    }

    public void StartMove(Vector3 pos)
    {
        _isMoving = true;
        _moveDirection = pos;
    }

    public void StopMove()
    {
        _isMoving = false;
        OnMovementEnded?.Invoke();
    }
}
