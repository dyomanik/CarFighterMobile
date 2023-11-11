using Ui;
using Game;
using Services.Analytics;
using Profile;
using UnityEngine;
using Services.Ads.UnityAds;
using Services.IAP;

internal class MainController : BaseController
{
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;

    private MainMenuController _mainMenuController;
    private SettingsMenuController _settingsMenuController;
    private GameController _gameController;

    public MainController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        _placeForUi = placeForUi;
        _profilePlayer = profilePlayer;

        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        OnChangeGameState(_profilePlayer.CurrentState.Value);
    }

    protected override void OnDispose()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
        _settingsMenuController?.Dispose();
        _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
    }

    private void OnAdsInitialized() => UnityAdsService.Instance.RewardedPlayer.Play();

    private void OnBuyInitialized() => IAPService.Instance.Buy("product_1");
    

    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                _gameController?.Dispose();
                _settingsMenuController?.Dispose();
                break;
            case GameState.Game:
                _gameController = new GameController(_profilePlayer);
                _mainMenuController?.Dispose();
                _settingsMenuController?.Dispose();
                AnalyticsManager.Instance.SendGameStarted();
                break;
            case GameState.Settings:
                _settingsMenuController = new SettingsMenuController(_placeForUi, _profilePlayer);
                _mainMenuController?.Dispose();
                _gameController?.Dispose();
                break;
            case GameState.RewardedAds:
                OnAdsInitialized();
                break;
            case GameState.Buying:
                OnBuyInitialized();
                break;
            default:
                _mainMenuController?.Dispose();
                _gameController?.Dispose();
                _settingsMenuController?.Dispose();
                break;
        }
    }
}