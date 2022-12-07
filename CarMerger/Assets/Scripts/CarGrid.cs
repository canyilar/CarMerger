using UnityEngine;

namespace CarMerger
{
    public class CarGrid : MonoBehaviour
    {
        public Car AssignedCar => _assignedCar;

        private Car _assignedCar;

        public void AssignCar(Car car) => _assignedCar = car;
        public void ClearGrid() => _assignedCar = null;
    }
}