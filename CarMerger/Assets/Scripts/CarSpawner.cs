using UnityEngine;
using System.Collections.Generic;

namespace CarMerger
{
    public class CarSpawner : MonoBehaviour
    {
        [SerializeField] private List<CarGrid> _carGrids;
        [SerializeField] private Car _carPrefab;

        public void SpawnCar()
        {
            foreach(CarGrid grid in _carGrids)
            {
                if (grid.AssignedCar == null)
                {
                    Car newCar = Instantiate(_carPrefab, grid.transform.position, Quaternion.identity);
                    grid.AssignCar(newCar);
                }
            }
        }
    }
}