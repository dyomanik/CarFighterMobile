using Profile;
using Services.Analytics;
using UnityEngine;
using Services.Ads.UnityAds;
using Services.IAP;

internal class EntryPoint : MonoBehaviour
{
    private const float SpeedCar = 15f;
    private const float JumpHeight = 5f;
    private const GameState InitialState = GameState.Start;

    [SerializeField] private Transform _placeForUi;

    private MainController _mainController;

    private void Start()
    {
        var profilePlayer = new ProfilePlayer(SpeedCar, JumpHeight, InitialState);
        _mainController = new MainController(_placeForUi, profilePlayer);
        AnalyticsManager.Instance.SendMainMenuOpened();
    }

    private void OnDestroy()
    {
        _mainController.Dispose();
    }
}