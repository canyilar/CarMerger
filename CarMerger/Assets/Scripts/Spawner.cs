using UnityEngine;
using System.Collections.Generic;

namespace CarMerger
{
    public class Spawner : MonoBehaviour
    {
        [Header("Car")]
        [SerializeField] private Car _carPrefab;
        [SerializeField] private Transform _carHolder;

        [Header("Grid")]
        [SerializeField] private int _width;
        [SerializeField] private int _height;
        [SerializeField] private float _distanceBtwnGrids = 1.5f;
        [SerializeField] private CarGrid _carGridPrefab;
        [SerializeField] private Transform _gridSpawnPosition;
        [SerializeField] private Transform _gridHolder;

        private List<CarGrid> _carGrids;

        private void Start()
        {
            SpawnGrids();
        }

        private void SpawnGrids()
        {
            _carGrids = new List<CarGrid>();

            for (int x = _height; x > 0; x--)
            {
                for (int y = 0; y < _width; y++)
                {
                    CarGrid grid = Instantiate(_carGridPrefab, _gridSpawnPosition.position + new Vector3(y, 0, x) * _distanceBtwnGrids, Quaternion.identity);
                    grid.transform.SetParent(_gridHolder);
                    grid.name = $"{y}:{x} grid";
                    _carGrids.Add(grid);
                }
            }
        }

        public void SpawnCar()
        {
            foreach(CarGrid grid in _carGrids)
            {
                if (grid.AssignedCar == null)
                {
                    Car newCar = Instantiate(_carPrefab, grid.transform.position, Quaternion.identity);
                    newCar.transform.SetParent(_carHolder);
                    grid.AssignCar(newCar);
                    break;
                }
            }
        }
    }
}