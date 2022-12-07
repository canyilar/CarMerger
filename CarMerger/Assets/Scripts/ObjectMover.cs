using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] private float _distanceFromCamera = 10f;
    [SerializeField] private LayerMask _hitLayers;

    private Camera _mainCam;
    private IMoveable _currentMoveable;

    private void Awake()
    {
        _mainCam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
            Vector3 worldPos = _mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _distanceFromCamera));

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _hitLayers))
            {
                if (hit.collider == null) return;

                if (hit.collider.TryGetComponent(out IMoveable moveable))
                {
                    _currentMoveable = moveable;
                    moveable.StartMove(worldPos);
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (_currentMoveable != null) _currentMoveable.StopMove();
        }

    }
}
