using UnityEngine;

public class Car : MonoBehaviour, ICombineable<Car>
{
    private Moveable _moveable;
    private float _checkRadius = 2f;
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

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].CompareTag("Road"))
            {
                print(hits[i].ClosestPoint(transform.position));
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

public interface ICombineable<T>
{
    void Combine(T other);
}
