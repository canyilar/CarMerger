using System;
using UnityEngine;

public class Moveable : MonoBehaviour, IMoveable
{
    private Rigidbody _rb;
    private bool _canMove;
    private Vector3 _moveDirection;
    private Vector3 _startPosition;

    public event Action OnMovementEnded;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _startPosition = transform.position;    
    }

    private void FixedUpdate()
    {
        if (_canMove)
        {
            _rb.MovePosition(_moveDirection);
        }
    }

    public void StartMove(Vector3 pos)
    {
        _canMove = true;
        _moveDirection = pos;
    }

    public void StopMove()
    {
        _canMove = false;
        OnMovementEnded?.Invoke();
    }
}
