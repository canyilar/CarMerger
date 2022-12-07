using UnityEngine;

namespace CarMerger
{
    public class MoneyManager : MonoBehaviour
    {
        public static MoneyManager Instance { get; private set; }

        [SerializeField] private int _money;

        private void Awake()
        {
            if (Instance == null) Instance = this;
        }

        public void AddMoney(int amount) => _money += amount;
        public void RemoveMoney(int amount) => _money -= amount;
        public int GetMoney() => _money;
    }
}