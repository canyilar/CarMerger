using System;
using UnityEngine;

namespace CarMerger
{
    public interface IMoveable
    {
        event Action OnMovementEnded;
        void StartMove(Vector3 pos);
        void StopMove();
    } 
}
