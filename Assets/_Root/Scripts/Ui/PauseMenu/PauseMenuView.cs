using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    internal class PauseMenuView : MonoBehaviour
    {
        [field: Header("Pause Menu Buttons")]
        [field: SerializeField] public Button ContinueButton { get; private set; }
        [field: SerializeField] public Button MenuButton { get; private set; }

        [Header("Settings")]
        [SerializeField] private Vector3 _showSize = Vector3.one;
        [SerializeField] private Vector3 _hideSize = Vector3.zero;
        [SerializeField] private float _duration = 0.3f;

        public void Init(UnityAction continueGame, UnityAction menu)
        {
            ContinueButton.onClick.AddListener(continueGame);
            MenuButton.onClick.AddListener(menu);
        }

        public void OnDestroy()
        {
            ContinueButton.onClick.RemoveAllListeners();
            MenuButton.onClick.RemoveAllListeners();
        }

        public void ShowMenu() =>
            PlayAnimation(_showSize, _duration,
                onStart: () => gameObject.SetActive(true));

        public void HideMenu() =>
            PlayAnimation(_hideSize, _duration,
                onFinish: () => gameObject.SetActive(false));

        private void PlayAnimation(Vector3 targetScale, float duration, Action onStart = null, Action onFinish = null)
        {
            onStart?.Invoke();

            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(targetScale, duration)).SetUpdate(true);
            sequence.OnComplete(
                () => onFinish?.Invoke());
        }
    }
}