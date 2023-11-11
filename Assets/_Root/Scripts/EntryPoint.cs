using Profile;
using UnityEngine;

internal class EntryPoint : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _placeForUi;

    [SerializeField] private EntryPointConfig _config;

    private MainController _mainController;

    private void Start()
    {
        var profilePlayer = new ProfilePlayer(_config.SpeedCar, _config.JumpHeight, _config.InitialState);
        _mainController = new MainController(_placeForUi, profilePlayer);
    }

    private void OnDestroy()
    {
        _mainController.Dispose();
    }
}