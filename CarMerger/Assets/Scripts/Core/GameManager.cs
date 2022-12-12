using UnityEngine;
using DG.Tweening;
using TMPro;

namespace CarMerger
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private int _minSpeedMultiplier = 1;
        [SerializeField] private int _maxSpeedMultiplier = 5;

        [SerializeField] private int _currentMoney;
        //UI
        [SerializeField] private Transform _canvas;
        [SerializeField] private GameObject _moneyPrefab;
        [SerializeField] private RectTransform _moneyUITransform;
        [SerializeField] private TextMeshProUGUI _moneyText;

        public static Camera MainCamera
        {
            get 
            {
                if (_mainCamera == null)
                {
                    _mainCamera = Camera.main;
                }

                return _mainCamera;
            }
        }
        private static Camera _mainCamera;

        public int SpeedMultiplier => _speedMultiplier;
        private int _speedMultiplier;

        private void Awake()
        {
            Instance = this;
            _speedMultiplier = _minSpeedMultiplier;
        }

        private void OnEnable()
        {
            Road.OnCarLap += OnCarLap; 
        }

        private void OnDisable()
        {
            Road.OnCarLap -= OnCarLap;
        }

        private void Start()
        {
            _currentMoney = 0;
            UpdateMoneyText();
        }
        public void OnCarLap(Car car)
        {
            AddMoney(car.transform.position, car.CarLevel * 100);
        }

        public void AddMoney(Vector3 startWorldPos, int value)
        {
            _currentMoney += value;

            Vector3 uiStartPos = MainCamera.WorldToScreenPoint(startWorldPos);
            GameObject money = Instantiate(_moneyPrefab, _canvas);
            Transform moneyTransform = money.transform;
            TMP_Text moneyText = moneyTransform.GetChild(1).GetComponent<TMP_Text>();

            moneyText.text = value.ToString();
            moneyTransform.position = uiStartPos;
            moneyTransform.DOScale(0.3f, 0.5f);
            moneyTransform.DOMove(_moneyUITransform.position + Vector3.left * 200, 0.5f).OnComplete(() =>
                {
                    Destroy(money);
                    UpdateMoneyText();
                }
            );
        }

        public void UpdateMoneyText()
        {
            _moneyText.text = _currentMoney.ToString();
        }

        public void IncreaseSpeedMultiplier()
        {
            _speedMultiplier = _maxSpeedMultiplier;
        }

        public void NormalizeSpeedMultiplier() => _speedMultiplier = _minSpeedMultiplier;
    }
}