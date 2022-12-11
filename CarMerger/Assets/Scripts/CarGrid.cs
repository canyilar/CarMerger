using UnityEngine;

namespace CarMerger
{
    [SelectionBase]
    public class CarGrid : MonoBehaviour, ICarActioner
    {
        [SerializeField] private Car _assignedCar;

        public Car AssignedCar => _assignedCar;

        public bool AssignCar(Car car)
        {
            if (AssignedCar != null) return false;

            _assignedCar = car;
            _assignedCar.AssignedGrid = this;
            _assignedCar.SetPosition(transform.position);
            return true;
        }

        public void ClearGrid()
        {
            _assignedCar = null;
        }

        public bool DoAction(Car car)
        {
            return AssignCar(car);
        }
    }
}