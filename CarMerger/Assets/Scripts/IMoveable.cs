using System;
using UnityEngine;

namespace CarMerger
{
    public interface IMoveable
    {
        /// <summary>
        /// Can we move this object currently?
        /// </summary>
        public bool CanMove { get; }
        void StartMove(Vector3 pos);
        void StopMove();

        event Action OnMovementEnded;
    } 
}
