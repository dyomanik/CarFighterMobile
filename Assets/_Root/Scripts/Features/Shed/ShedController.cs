using Features.Shed;
using Features.Shed.Upgrade;
using JetBrains.Annotations;
using Profile;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Features.Inventory
{
    internal interface IShedController
    {
    }

    internal class ShedController : BaseController, IShedController
    {
        private readonly ProfilePlayer _profilePlayer;
        private readonly IShedView _view;
        private readonly InventoryContext _inventoryContext;
        private readonly IUpgradeHandlersRepository _upgradeHandlersRepository;

        public ShedController(
            [NotNull] ProfilePlayer profilePlayer,
            [NotNull] IShedView shedView,
            [NotNull] InventoryContext inventoryContext,
            [NotNull] IUpgradeHandlersRepository repository)
        {
            _profilePlayer
                = profilePlayer ?? throw new ArgumentNullException(nameof(profilePlayer));

            _view 
                = shedView ?? throw new ArgumentNullException(nameof(shedView));

            _inventoryContext
                = inventoryContext ?? throw new ArgumentNullException(nameof(inventoryContext));

            _upgradeHandlersRepository
                = repository ?? throw new ArgumentNullException(nameof(repository));

            _view.Init(Apply, Back);
        }

        private void Apply()
        {
            _profilePlayer.CurrentCar.Restore();

            UpgradeWithEquippedItems(
                _profilePlayer.CurrentCar,
                _profilePlayer.Inventory.EquippedItems,
                _upgradeHandlersRepository.Items);

            _profilePlayer.CurrentState.Value = GameState.Start;
            Log($"Apply. Current Speed: {_profilePlayer.CurrentCar.Speed}" +
                $"\nCurrent JumpHeight: {_profilePlayer.CurrentCar.JumpHeight}");
        }

        private void Back()
        {
            _profilePlayer.CurrentState.Value = GameState.Start;
            Log($"Back. Current Speed: {_profilePlayer.CurrentCar.Speed}" +
                $"\nCurrent JumpHeight: {_profilePlayer.CurrentCar.JumpHeight}");
        }

        private void UpgradeWithEquippedItems(
            IUpgradable upgradable,
            IReadOnlyList<string> equippedItems,
            IReadOnlyDictionary<string, IUpgradeHandler> upgradeHandlers)
        {
            foreach (string itemId in equippedItems)
                if (upgradeHandlers.TryGetValue(itemId, out IUpgradeHandler handler))
                    handler.Upgrade(upgradable);
        }

        private void Log(string message) =>
            Debug.Log($"[{GetType().Name}] {message}");
    }
}