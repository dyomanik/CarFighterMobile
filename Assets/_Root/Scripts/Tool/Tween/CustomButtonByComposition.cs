using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Tool.Tween
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(RectTransform))]
    public class CustomButtonByComposition : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Button _button;
        [SerializeField] private RectTransform _rectTransform;

        [Header("General settings of Tween Animation")]
        [SerializeField] private AnimationButtonType _animationButtonType = AnimationButtonType.ChangePosition;
        [SerializeField] private Ease _curveEase = Ease.Linear;
        [SerializeField] private float _duration = 0.6f;
        [SerializeField] private float _strength = 30f;
        [SerializeField] private int _vibrato = 10;
        [Header("Settings for change rotation/position animations")]
        [SerializeField] private float _randomness = 90f;
        [SerializeField] private bool _snapping = false;
        [SerializeField] private bool _fadeOut = true;
        

        private void OnValidate() => InitComponents();
        private void Awake() => InitComponents();

        private void Start() => _button.onClick.AddListener(OnButtonClick);
        private void OnDestroy() => _button.onClick.RemoveAllListeners();

        private void InitComponents()
        {
            _button ??= GetComponent<Button>();
            _rectTransform ??= GetComponent<RectTransform>();
        }


        private void OnButtonClick() =>
            ActivateAnimation();

        private void ActivateAnimation()
        {
            _rectTransform.DOComplete();
            switch (_animationButtonType)
            {
                case AnimationButtonType.ChangeRotation:
                    _rectTransform.DOShakeRotation(_duration, Vector3.forward * _strength, _vibrato, _randomness, _fadeOut).SetEase(_curveEase);
                    break;

                case AnimationButtonType.ChangePosition:
                    _rectTransform.DOShakeAnchorPos(_duration, Vector2.one * _strength, _vibrato, _randomness, _snapping, _fadeOut).SetEase(_curveEase);
                    break;
                case AnimationButtonType.DoPunch:
                    _rectTransform.DOPunchAnchorPos(Vector3.one * _strength, _duration, _vibrato).SetEase(_curveEase);
                    break;
            }
        }

        [ContextMenu(nameof(PlayAnimation))]
        public void PlayAnimation()
        {
            ActivateAnimation();
        }

        [ContextMenu(nameof(StopAnimation))]
        public void StopAnimation()
        {
            DOTween.Kill(_rectTransform);
        }
    }
}
