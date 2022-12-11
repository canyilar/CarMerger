using UnityEngine;

namespace CarMerger
{
    public class CarPlaceholder : MonoBehaviour
    {
        [SerializeField] private Car _placeholdingCar;

        public void PlaceholdCar(Car car)
        {
            _placeholdingCar = car;
        }

        private void OnMouseDown()
        {
            _placeholdingCar.AssignedRoad.RemoveCarFromRoad(_placeholdingCar);
            _placeholdingCar.SetPositionSmooth(transform.position);
            _placeholdingCar.SetRotationSmooth(transform.eulerAngles);
            Destroy(gameObject);
        }
    }
}