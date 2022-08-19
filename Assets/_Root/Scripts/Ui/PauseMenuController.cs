using Profile;
using Tool;
using UnityEngine;

namespace Ui
{
    internal class PauseMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/Menu/PauseMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly PauseMenuView _view;

        public PauseMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(ContinueGame,Menu);
            _view.gameObject.SetActive(false);
        }

        private PauseMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<PauseMenuView>();
        }

        private void Menu() =>
            _profilePlayer.CurrentState.Value = GameState.Start;

        private void ContinueGame() => _view.HideMenu();

        public void ShowPauseMenu() => _view.ShowMenu();

        public void HidePauseMenu() => _view.HideMenu();
    }
}