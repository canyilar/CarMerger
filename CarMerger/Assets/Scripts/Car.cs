using DG.Tweening;
using UnityEngine;

namespace CarMerger
{
    [SelectionBase]
    public class Car : MonoBehaviour, ICombineable<Car>, ICarActioner
    {
        [SerializeField] private int _carLevel;
        [SerializeField] private bool _isOnRoad;

        [Header("Grid")]
        [SerializeField] private CarGrid _assignedGrid;
        [SerializeField] private Vector3 _currentPosition;

        [Header("Detection Settings")]
        [SerializeField] private float _checkRadius = 2f;
        [SerializeField] private Vector3 _checkOffset;

        public Transform TargetRoadPoint { get; set; }
        public int CarLevel => _carLevel;
        public CarGrid AssignedGrid { get => _assignedGrid; 
            set 
            {
                if (_assignedGrid != null)
                {
                    _assignedGrid.ClearGrid();
                }
                _assignedGrid = value;
            } 
        }

        private Moveable _moveable;
        private Rigidbody _rb;

        private void Awake()
        {
            _moveable = GetComponent<Moveable>();
            _rb = GetComponent<Rigidbody>();
            _currentPosition = transform.position;
        }

        private void OnEnable()
        {
            _moveable.OnMovementEnded += CheckForAction;
            transform.DOPunchScale(Vector3.one * 0.2f, 0.6f);
        }

        private void OnDisable()
        {
            _moveable.OnMovementEnded -= CheckForAction;
        }

        public bool Combine(Car other)
        {
            if (_isOnRoad || other._carLevel != _carLevel || _carLevel == Spawner.Instance.MaxCarLevel) return false;
            
            CarGrid grid = AssignedGrid;
            other.AssignedGrid = null;
            AssignedGrid = null;

            Spawner.Instance.SpawnCarOnGrid(grid, _carLevel + 1);

            Destroy(other.gameObject);
            Destroy(gameObject);
            return true;
        }

        private void CheckForAction()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position + _checkOffset, _checkRadius);
            if (hits.Length <= 0)
            {
                SetPosition(_currentPosition);
                return;
            }

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].gameObject == gameObject)
                {
                    SetPosition(_currentPosition);
                    continue;
                }

                if (hits[i].TryGetComponent(out ICarActioner carActioner))
                {
                    if (carActioner.DoAction(this))
                        break;
                }
                else SetPosition(_currentPosition);
            }
        }

        public void SetPosition(Vector3 pos)
        {
            _currentPosition = pos;
            _rb.position = pos;
        }

        public void SetPositionSmooth(Vector3 pos)
        {
            _moveable.SetCanMove(false);
            _currentPosition = pos;
            _rb.DOMove(pos, 0.8f).SetEase(Ease.Linear).OnComplete(() => _moveable.SetCanMove(true, 0.5f));
        }

        public void SetRotation(Quaternion rot) => _rb.rotation = rot;
        public void SetRotationSmooth(Vector3 rot) => _rb.DORotate(rot, 0.8f).SetEase(Ease.Linear);

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _checkRadius);
        }

        public bool DoAction(Car car)
        {
            return Combine(car);
        }
    }
}