using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using CarMerger;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _roadManager;
    private List<GameObject > _carsOnRoad = new List<GameObject>();// car,target

    private Transform _roadPoints;
    [SerializeField] private float _speed = 2.5f;

    public static GameManager Instance { get; private set; }    


    public event Action<Car> OnCarLap;


    private void Start()
    {
        Instance = this;
        _roadPoints = _roadManager.transform.Find("RoadMap").Find("RoadPoints");
    }

    private void Update()
    {
        MoveCarsOnRoad();
    }


    private void MoveCarsOnRoad()
    {
        List<GameObject> moveNextRoadPoint = new List<GameObject>();
        foreach (GameObject carObject in _carsOnRoad)
        {
            Car car = carObject.GetComponent<Car>();
            Transform roadPointTarget = car.TargetRoadPoint;

            if (IsVector3Same(roadPointTarget.position, carObject.transform.position)) 
            {
                //if car is on roadpoint set next target
                // moveNextRoadPoint.Add(car);
               
                Transform nextRoadPoint = GetNextCheckPoint(roadPointTarget);
                car.TargetRoadPoint = nextRoadPoint;
                
                if (nextRoadPoint == _roadPoints.GetChild(1))
                {
                    OnCarLap?.Invoke(car);
                }
                continue;
            }

            float distance = Vector3.Distance(car.transform.position, roadPointTarget.position);
            float duration = distance / _speed;

            Vector3 dir = (roadPointTarget.position - car.transform.position).normalized;
            dir.y = 0;
            car.transform.forward = Vector3.Lerp(car.transform.forward,dir , 0.1f);

            carObject.transform.DOMove(roadPointTarget.position, duration);
        }
    }

    public void SetCarToRoad(GameObject carObject)
    {
        Transform startPoint = _roadPoints.GetChild(0);
        Transform nextPoint = _roadPoints.GetChild(1);
        carObject.transform.position = startPoint.position;
        Car car = carObject.GetComponent<Car>();
        car.GetComponent<Collider>().enabled=false;
        car.TargetRoadPoint = nextPoint;
        _carsOnRoad.Add(carObject);
    }

    public void RemoveCarFromRoad(GameObject car)
    {
        _carsOnRoad.Remove(car);
    }

    private Transform GetNextCheckPoint(Transform currentCheckpoint)
    {
        int childrenCount = _roadPoints.childCount;
        for (int i = 0; i < childrenCount; i++)
        {
            if (_roadPoints.GetChild(i)== currentCheckpoint)
            {
                if (i== childrenCount-1)
                {
                   
                    return _roadPoints.GetChild(0);
                    
                }
                else
                {
                   
                    return _roadPoints.GetChild(i+1);
                }

            }
        }

        return null;
        
    }

    private bool IsVector3Same(Vector3 a, Vector3 b)
    {
        if (Mathf.Approximately(a.x, b.x)==false)
        {
            return false;
        }
        if (Mathf.Approximately(a.y, b.y) == false)
        {
            return false;
        }
        if (Mathf.Approximately(a.z, b.z) == false)
        {
            return false;
        }
        return true;
    }



}
