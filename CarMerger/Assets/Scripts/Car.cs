using UnityEngine;

namespace CarMerger
{
    public class Car : MonoBehaviour, ICombineable<Car>
    {
        [SerializeField] private float _checkRadius = 2f;

        private Moveable _moveable;
        private Rigidbody _rb;
        private Vector3 _startPosition;

        private void Awake()
        {
            _moveable = GetComponent<Moveable>();
            _rb = GetComponent<Rigidbody>();
            _startPosition = transform.position;
        }

        private void OnEnable()
        {
            _moveable.OnMovementEnded += CheckForAction;
        }

        private void OnDisable()
        {
            _moveable.OnMovementEnded -= CheckForAction;
        }

        public void Combine(Car other)
        {
            Debug.Log("I have combined with " + other.name);
        }

        private void CheckForAction()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, _checkRadius);

            if (hits.Length <= 0)
            {
                _rb.position = _startPosition;
                return;
            }

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].gameObject == gameObject)
                {
                    _rb.position = _startPosition;
                    continue;
                }

                if (hits[i].CompareTag("Road"))
                {
                    _rb.position = hits[i].ClosestPoint(transform.position);
                    break;
                }
                else if (hits[i].TryGetComponent(out ICombineable<Car> combineable))
                {
                    combineable.Combine(this);
                    break;
                }
                else
                {
                    _rb.position = _startPosition;
                }
            }
        }
    }
}