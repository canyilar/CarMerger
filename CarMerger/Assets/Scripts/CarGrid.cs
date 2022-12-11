using UnityEngine;

namespace CarMerger
{
    [SelectionBase]
    public class CarGrid : MonoBehaviour
    {
        [SerializeField] private Car _assignedCar;

        public Car AssignedCar => _assignedCar;

        public bool AssignCar(Car car)
        {
            if (AssignedCar != null) return false;

            _assignedCar = car;
            _assignedCar.AssignedGrid = this;
            return true;
        }

        public void ClearGrid()
        {
            _assignedCar = null;
        }
    }
}