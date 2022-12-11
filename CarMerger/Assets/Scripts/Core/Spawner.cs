using UnityEngine;
using System.Collections.Generic;

namespace CarMerger
{
    public class Spawner : MonoBehaviour
    {
        public static Spawner Instance { get; private set; }

        [Header("Car")]
        [SerializeField] private Car[] _carPrefabs;
        [SerializeField] private Transform _carHolder;
        [SerializeField] private bool _spawnCarsOnStart;
        [SerializeField] private CarPlaceholder[] _carPlaceholderPrefabs;

        [Header("Grid")]
        [SerializeField] private int _width;
        [SerializeField] private int _height;
        [SerializeField] private float _distanceBtwnGrids = 1.5f;
        [SerializeField] private CarGrid _carGridPrefab;
        [SerializeField] private Transform _gridSpawnPosition;
        [SerializeField] private Transform _gridHolder;

        public int MaxCarLevel { get; private set; }

        private List<CarGrid> _carGrids;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            MaxCarLevel = _carPrefabs.Length;
        }

        private void Start()
        {
            SpawnGrids();

            if (_spawnCarsOnStart)
            {
                for (int i = 0; i < _carGrids.Count; i++)
                {
                    SpawnCar();
                }
            }
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

        /// <summary>
        /// Spawn Level 1 car.
        /// </summary>
        public void SpawnCar()
        {
            foreach (CarGrid grid in _carGrids)
            {
                if (grid.AssignedCar == null)
                {
                    Car newCar = Instantiate(_carPrefabs[0], grid.transform.position, Quaternion.identity);
                    newCar.transform.SetParent(_carHolder);
                    grid.AssignCar(newCar);
                    break;
                }
            }
        }

        public Car SpawnCar(int level, Vector3 posititon)
        {
            foreach (CarGrid grid in _carGrids)
            {
                if (grid.AssignedCar == null)
                {
                    Car newCar = Instantiate(_carPrefabs[level - 1], posititon, Quaternion.identity);
                    newCar.transform.SetParent(_carHolder);
                    grid.AssignCar(newCar);
                    return newCar;
                }
            }

            return null;
        }

        public Car SpawnCarOnGrid(CarGrid grid, int level)
        {
            Car newCar = Instantiate(_carPrefabs[level - 1], grid.transform.position, Quaternion.identity);
            newCar.transform.SetParent(_carHolder);

            if (grid.AssignCar(newCar))
            {
                return newCar;
            }

            Destroy(newCar.gameObject);
            return null;
        }

        public void SpawnPlaceholder(Car car)
        {
            CarPlaceholder holder = Instantiate(_carPlaceholderPrefabs[car.CarLevel - 1], car.AssignedGrid.transform.position, car.AssignedGrid.transform.rotation);
            holder.transform.SetParent(_carHolder);
            holder.PlaceholdCar(car);
        }
    }
}