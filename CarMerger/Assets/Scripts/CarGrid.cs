using UnityEngine;

namespace CarMerger
{
    public class CarGrid : MonoBehaviour
    {
        [SerializeField] private Car _assignedCar;

        public Car AssignedCar => _assignedCar;

        public void AssignCar(Car car) => _assignedCar = car;
        public void ClearGrid() => _assignedCar = null;
    }
}