using System;
using UnityEngine;
using System.Collections.Generic;

namespace CarMerger
{
    public class Road : MonoBehaviour, ICarActioner
    {
        [Space(15)]
        [SerializeField] private Transform[] _roadPoints;
        [SerializeField] private float _carRotateSpeed = 4;
        public static event Action<Car> OnCarLap;

        private List<Car> _carsOnRoad = new List<Car>();

        private void Update()
        {
            MoveCarsOnRoad();
        }

        public bool DoAction(Car car)
        {
            SetCarToRoad(car);
            Spawner.Instance.SpawnPlaceholder(car);
            return true;
        }

        private void MoveCarsOnRoad()
        {
            List<GameObject> moveNextRoadPoint = new List<GameObject>();
            foreach (Car car in _carsOnRoad)
            {
                Transform roadPointTarget = car.TargetRoadPoint;

                if (roadPointTarget.position.IsSameWith(car.transform.position))
                {
                    Transform nextRoadPoint = GetNextCheckPoint(roadPointTarget);
                    car.TargetRoadPoint = nextRoadPoint;

                    if (nextRoadPoint == _roadPoints[1])//new lap
                    {
                        OnCarLap?.Invoke(car);
                    }
                    continue;
                }

                float distance = Vector3.Distance(car.transform.position, roadPointTarget.position);
                float duration = distance / car.Speed;

                Vector3 dir = (roadPointTarget.position - car.transform.position).normalized;
                dir.y = 0;
                car.transform.forward = Vector3.Lerp(car.transform.forward, dir, _carRotateSpeed * Time.deltaTime);

                car.transform.position = Vector3.MoveTowards(car.transform.position, roadPointTarget.position, car.Speed * GameManager.Instance.SpeedMultiplier * Time.deltaTime);
            }
        }

        public void SetCarToRoad(Car car)
        {
            TutorialManager.Instance.ClosePutOnRoadTut();
            Transform startPoint = _roadPoints[0];
            Transform nextPoint = _roadPoints[1];
            car.transform.position = startPoint.position;
            car.TargetRoadPoint = nextPoint;
            car.AssignRoad(this);
            _carsOnRoad.Add(car);
        }

        public void RemoveCarFromRoad(Car car)
        {
            car.RemoveRoad();
            _carsOnRoad.Remove(car);
        }

        private Transform GetNextCheckPoint(Transform currentCheckpoint)
        {
            int childrenCount = _roadPoints.Length;
            for (int i = 0; i < childrenCount; i++)
            {
                if (_roadPoints[i] == currentCheckpoint)
                {
                    if (i == childrenCount - 1)
                    {
                        return _roadPoints[0];
                    }
                    else
                    {
                        return _roadPoints[i + 1];
                    }
                }
            }

            return null;
        }
    }
}