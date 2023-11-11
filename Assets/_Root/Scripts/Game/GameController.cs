using Game.Car;
using Game.InputLogic;
using Game.TapeBackground;
using Features.AbilitySystem;
using Profile;
using Tool;
using UnityEngine;
using Services;

namespace Game
{
    internal class GameController : BaseController
    {
        private readonly SubscriptionProperty<float> _leftMoveDiff;
        private readonly SubscriptionProperty<float> _rightMoveDiff;

        private readonly CarController _carController;
        private readonly InputGameController _inputGameController;
        private readonly TapeBackgroundController _tapeBackgroundController;
        private readonly AbilitiesContext _abilitiesContext;

        public GameController(Transform placeForUI, ProfilePlayer profilePlayer)
        {
            _leftMoveDiff = new SubscriptionProperty<float>();
            _rightMoveDiff = new SubscriptionProperty<float>();

            _tapeBackgroundController = CreateTapeBackground(_leftMoveDiff, _rightMoveDiff);

            _inputGameController = CreateInputGameController(_leftMoveDiff, _rightMoveDiff, profilePlayer);

            _carController = CreateCarController();

            _abilitiesContext = CreateAbilitiesContext(placeForUI,_carController);

            ServiceRoster.Analytics.SendGameStarted();
        }

        private TapeBackgroundController CreateTapeBackground(SubscriptionProperty<float> leftMoveDiff, SubscriptionProperty<float> rightMoveDiff)
        {
            var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
            AddController(tapeBackgroundController);

            return tapeBackgroundController;
        }

        private InputGameController CreateInputGameController(SubscriptionProperty<float> leftMoveDiff, 
            SubscriptionProperty<float> rightMoveDiff, ProfilePlayer profilePlayer)
        {
            var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar);
            AddController(inputGameController);

            return inputGameController;
        }

        private CarController CreateCarController()
        {
            var carController = new CarController();
            AddController(carController);

            return carController;
        }

        private AbilitiesContext CreateAbilitiesContext(Transform placeForUi, IAbilityActivator abilityActivator)
        {
            var abilitiesContext = new AbilitiesContext(placeForUi, abilityActivator);
            AddContext(abilitiesContext);

            return abilitiesContext;
        }
    }
}