using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Tool.Tween
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(RectTransform))]
    public class CustomButton_Obsolete : Button
    {
        public static string AnimationTypeName => nameof(_animationButtonType);
        public static string CurveEaseName => nameof(_curveEase);
        public static string DurationName => nameof(_duration);
        public static string VibratoName => nameof(_vibrato);
        public static string RandomnessName => nameof(_randomness);
        public static string SnappingName => nameof(_snapping);
        public static string FadeOutName => nameof(_fadeOut);

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private RectTransform _rectTransform;

        [SerializeField] private AnimationButtonType _animationButtonType = AnimationButtonType.ChangePosition;
        [SerializeField] private Ease _curveEase = Ease.Linear;
        [SerializeField] private float _duration = 0.6f;
        [SerializeField] private float _strength = 30f;
        [SerializeField] private int _vibrato = 10;
        [SerializeField] private float _randomness = 90f;
        [SerializeField] private bool _snapping = false;
        [SerializeField] private bool _fadeOut = true;


        protected override void Awake()
        {
            base.Awake();
            InitComponents();
        }

        protected new void OnValidate() =>
            InitComponents();

        private void InitComponents()
        {
            _audioSource ??= GetComponent<AudioSource>();
            _rectTransform ??= GetComponent<RectTransform>();
        }


        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            ActivateAnimation();
            ActivateSound();
        }

        private void ActivateAnimation()
        {
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

        private void ActivateSound()
        {
            _audioSource.Play();
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
