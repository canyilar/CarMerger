using UnityEngine;

namespace CarMerger
{
    public class UIManager : MonoBehaviour
    {
        public void SpawnCarButton() => Spawner.Instance.SpawnCar();
    }
}