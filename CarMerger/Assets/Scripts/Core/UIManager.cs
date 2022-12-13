using UnityEngine;

namespace CarMerger
{
    public class UIManager : MonoBehaviour
    {
        public void SpawnCarButton()
        {
            if (GameManager.Instance.CurrentMoney >= 100)
            {
                Spawner.Instance.SpawnCar();
                GameManager.Instance.RemoveMoney(100);
            }
        }
    }
}