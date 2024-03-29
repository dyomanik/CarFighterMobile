using Features.Fight;
using Features.Rewards;
using Features.Shed;
using Game;
using Profile;
using Ui;
using UnityEngine;

internal class MainController : BaseController
{
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;

    private MainMenuController _mainMenuController;
    private SettingsMenuController _settingsMenuController;
    private GameController _gameController;
    private BaseContext _shedContext;
    private RewardController _rewardController;
    private StartFightController _startFightController;
    private FightController _fightController;
    private PauseController _pauseController;

    public MainController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        _placeForUi = placeForUi;
        _profilePlayer = profilePlayer;

        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        OnChangeGameState(_profilePlayer.CurrentState.Value);
    }

    private void OnChangeGameState(GameState state)
    {
        DisposeSubControllers();
        DisposeContexts();
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                break;
            case GameState.Game:
                _gameController = new GameController(_placeForUi, _profilePlayer);
                _startFightController = new StartFightController(_placeForUi, _profilePlayer);
                _pauseController = new PauseController(_placeForUi, _profilePlayer);
                break;
            case GameState.Shed:
                _shedContext = CreateShedContext(_placeForUi, _profilePlayer);
                break;
            case GameState.Settings:
                _settingsMenuController = new SettingsMenuController(_placeForUi, _profilePlayer);
                break;
            case GameState.DailyReward:
                _rewardController = new RewardController(_placeForUi, _profilePlayer);
                break;
            case GameState.Fight:
                _fightController = new FightController(_placeForUi, _profilePlayer);
                break;
        }
    }

    private void DisposeSubControllers()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
        _settingsMenuController?.Dispose();
        _rewardController?.Dispose();
        _startFightController?.Dispose();
        _fightController?.Dispose();
        _pauseController?.Dispose();
    }

    private void DisposeContexts()
    {
        _shedContext?.Dispose();
    }

    private ShedContext CreateShedContext(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        var context = new ShedContext(placeForUi, profilePlayer);
        AddContext(context);
        return context;
    }

    protected override void OnDispose()
    {
        DisposeSubControllers();
        DisposeContexts();
        _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
    }
}