using Features.Inventory;
using Features.Shed.Upgrade;
using Profile;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Features.Shed
{
    internal class ShedContext : BaseContext
    {
        public ShedContext(Transform placeForUi, ProfilePlayer profilePlayer) 
            => CreateController(placeForUi, profilePlayer);

        private IShedController CreateController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            InventoryContext inventoryContext = CreateInventoryContext(placeForUi, profilePlayer.Inventory);
            IShedView shedView = LoadView(placeForUi);
            IUpgradeHandlersRepository upgradeHandlersRepository = CreateRepository();
            var shedController = new ShedController(profilePlayer, shedView, inventoryContext, upgradeHandlersRepository);
            AddController(shedController);

            return shedController;
        }
        private IShedView LoadView(Transform placeForUi)
        {
            var viewPath = new ResourcePath("Prefabs/Shed/ShedView");
            GameObject prefab = ResourcesLoader.LoadPrefab(viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<ShedView>();
        }

        private InventoryContext CreateInventoryContext(Transform placeForUi, IInventoryModel model)
        {
            var context = new InventoryContext(placeForUi, model);
            AddContext(context);

            return context;
        }

        private IUpgradeHandlersRepository CreateRepository()
        {
            var dataSourcePath = new ResourcePath("Configs/Shed/UpgradeItemConfigDataSource");
            UpgradeItemConfig[] upgradeConfigs = ContentDataSourceLoader.LoadUpgradeItemConfigs(dataSourcePath);
            var repository = new UpgradeHandlersRepository(upgradeConfigs);
            AddRepository(repository);

            return repository;
        }
    }
}