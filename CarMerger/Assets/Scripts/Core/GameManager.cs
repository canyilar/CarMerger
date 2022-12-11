using UnityEngine;
using DG.Tweening;
using TMPro;

namespace CarMerger
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private int _currentMoney;
        //UI
        [SerializeField] private Transform _canvas;
        [SerializeField] private GameObject _moneyPrefab;
        [SerializeField] private RectTransform _moneyUITransform;
        [SerializeField] private TextMeshProUGUI _moneyText;

        private static Camera _mainCamera;

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

        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            Road.OnCarLap += AddMoney; 
        }

        private void OnDisable()
        {
            Road.OnCarLap -= AddMoney;
        }

        private void Start()
        {
            _currentMoney = 0;
            UpdateMoneyText();
        }

        public void AddMoney(Car car)
        {
            _currentMoney += car.CarLevel * 100;

            Vector3 uiStartPos = MainCamera.WorldToScreenPoint(car.transform.position);
            GameObject money = Instantiate(_moneyPrefab, _canvas);
            Transform moneyTransform = money.transform;
            TMP_Text moneyText = moneyTransform.GetChild(1).GetComponent<TMP_Text>();

            moneyText.text = (car.CarLevel * 100).ToString();
            moneyTransform.position = uiStartPos;
            moneyTransform.DOScale(0.3f, 0.5f);
            moneyTransform.DOMove(_moneyUITransform.position + Vector3.left * 200, 0.5f).OnComplete(() =>
                {
                    Destroy(money);
                    UpdateMoneyText();
                }
            );
        }

        public void AddMoney(Vector3 startWorldPos, int value)
        {
            _currentMoney += value;

            Vector3 uiStartPos = MainCamera.WorldToScreenPoint(startWorldPos);
            GameObject money = Instantiate(_moneyPrefab, _canvas);
            Transform moneyTransform = money.transform;
            TMP_Text moneyText = moneyTransform.GetChild(0).GetComponent<TMP_Text>();

            moneyText.text = value.ToString();
            moneyTransform.position = uiStartPos;
            moneyTransform.DOMove(_moneyUITransform.position, 0.5f).OnComplete(() =>
                {
                    Destroy(money);
                }
            );
        }

        public void UpdateMoneyText()
        {
            _moneyText.text = _currentMoney.ToString();
        }
    }
}