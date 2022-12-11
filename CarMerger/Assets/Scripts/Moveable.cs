using System;
using System.Threading.Tasks;
using UnityEngine;

namespace CarMerger
{
    public class Moveable : MonoBehaviour, IMoveable
    {
        public bool CanMove { get; private set; }
        public event Action OnMovementEnded;
        
        private Rigidbody _rb;
        private Vector3 _moveDirection;
        private bool _isMoving;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            SetCanMove(true);
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
            if (!CanMove) return;

            _isMoving = true;
            _moveDirection = pos;
        }

        public void StopMove()
        {
            _isMoving = false;
            OnMovementEnded?.Invoke();
        }

        public async void SetCanMove(bool state, float delay = 0)
        {
            await Task.Delay(TimeSpan.FromSeconds(delay));
            CanMove = state;
        }
    } 
}
