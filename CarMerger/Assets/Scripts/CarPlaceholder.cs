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
            //GameManager.Instance.RemoveCarFromRoad(_placeholdingCar.gameObject);
            _placeholdingCar.SetPositionSmooth(transform.position);
            _placeholdingCar.SetRotationSmooth(transform.eulerAngles);
            Destroy(gameObject);
        }
    }
}