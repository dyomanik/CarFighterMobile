using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    internal class PauseView : MonoBehaviour
    {
        [field: SerializeField] public Button PauseButton { get; private set; }

        public void Init(UnityAction showPauseMenu) => PauseButton.onClick.AddListener(showPauseMenu);

        public void OnDestroy() => PauseButton.onClick.RemoveAllListeners();
    }
}