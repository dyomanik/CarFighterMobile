using Profile;
using Tool;
using UnityEngine;

namespace Ui
{
    internal class PauseController : BaseController
    {
        private readonly ResourcePath _resourcePathPause = new ResourcePath("Prefabs/Menu/Pause");
        private readonly PauseView _pauseView;
        private readonly PauseMenuController _pauseMenuController;

        public PauseController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _pauseView = LoadView(placeForUi);
            _pauseView.Init(ShowPauseMenu);
            _pauseMenuController = CreatePauseMenuController(placeForUi, profilePlayer);
        }

        private PauseView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePathPause);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<PauseView>();
        }

        private void ShowPauseMenu() => _pauseMenuController.ShowPauseMenu();

        private PauseMenuController CreatePauseMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            var pauseMenuController = new PauseMenuController(placeForUi, profilePlayer);
            AddController(pauseMenuController);

            return pauseMenuController;
        }
    }
}