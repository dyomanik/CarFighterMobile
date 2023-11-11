using Game.Car;
using Game.InputLogic;
using Game.TapeBackground;
using Features.AbilitySystem;
using Profile;
using Tool;
using UnityEngine;

namespace Game
{
    internal class GameController : BaseController
    {
        public GameController(Transform placeForUI, ProfilePlayer profilePlayer)
        {
            var leftMoveDiff = new SubscriptionProperty<float>();
            var rightMoveDiff = new SubscriptionProperty<float>();


            var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
            AddController(tapeBackgroundController);

            var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar);
            AddController(inputGameController);

            var carController = new CarController();
            AddController(carController);

            var abilitiesController = new AbilitiesController(placeForUI, carController);
            AddController(abilitiesController);
        }
    }
}
