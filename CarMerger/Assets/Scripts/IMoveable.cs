using System;
using UnityEngine;

public interface IMoveable
{
    event Action OnMovementEnded;
    void StartMove(Vector3 pos);
    void StopMove();
}
