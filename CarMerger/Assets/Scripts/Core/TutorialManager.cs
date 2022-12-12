using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set; }

    [SerializeField] private Image _fingerImage;

    public Vector2 CombineTutStartPos;
    public Vector2 CombineTutEndPos;

    public Vector2 PutOnRoadTutStartPos;
    public Vector2 PutOnRoadTutEndPos;

    public Vector2 SpeedButtonTutStartPos;

    private TweenerCore<Vector2, Vector2, VectorOptions> _combineTutTween;
    private TweenerCore<Quaternion, Vector3, QuaternionOptions> _putOnRoadTutTween;

    private bool _combineTutFinished;
    private bool _putOnRoadTutFinished;
    private bool _speedButtonTutFinished;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        OpenCombineTut();
    }

    public void OpenCombineTut()
    {
        if (_combineTutFinished) return;

        _fingerImage.gameObject.SetActive(true);

        _fingerImage.rectTransform.anchoredPosition = CombineTutStartPos;
        _combineTutTween = _fingerImage.rectTransform.DOAnchorPos(CombineTutEndPos, 1).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

    public void CloseCombineTut()
    {
        if (_combineTutFinished) return;

        _fingerImage.gameObject.SetActive(false);
        _combineTutTween.Kill();
        _combineTutFinished = true;

        OpenPutOnRoadTut();
    }

    public void OpenPutOnRoadTut()
    {
        if (_putOnRoadTutFinished) return;
        _fingerImage.gameObject.SetActive(true);

        _fingerImage.rectTransform.anchoredPosition = PutOnRoadTutStartPos;
        _combineTutTween = _fingerImage.rectTransform.DOAnchorPos(PutOnRoadTutEndPos, 1).SetLoops(-1).SetEase(Ease.Linear);
    }

    public void ClosePutOnRoadTut()
    {
        if (_putOnRoadTutFinished) return;
        _fingerImage.gameObject.SetActive(false);

        _combineTutTween.Kill();
        _putOnRoadTutFinished = true;

        OpenButtonTut();
    }

    public void OpenButtonTut()
    {
        if (_speedButtonTutFinished) return;

        _fingerImage.gameObject.SetActive(true);
        _fingerImage.rectTransform.anchoredPosition = SpeedButtonTutStartPos;
        _putOnRoadTutTween = _fingerImage.rectTransform.DORotate(Vector3.forward * 20, 1).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
    }

    public void CloseButtonTut()
    {
        if (_speedButtonTutFinished) return;

        _fingerImage.gameObject.SetActive(false);
        _putOnRoadTutTween = null;
        _combineTutTween = null;
    }
}
