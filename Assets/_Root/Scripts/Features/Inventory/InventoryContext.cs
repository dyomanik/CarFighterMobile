using Tool;
using UnityEngine;
using Features.Inventory;
using Features.Inventory.Items;
using Object = UnityEngine.Object;

namespace Features.Shed
{
    internal class InventoryContext : BaseContext
    {
        public InventoryContext(Transform placeForUi, IInventoryModel model) => CreateController(placeForUi, model);

        private IInventoryController CreateController(Transform placeForUi, IInventoryModel model)
        {
            IInventoryView inventoryView = LoadView(placeForUi);
            IItemsRepository itemsRepository = CreateRepository();

            var inventoryController = new InventoryController(inventoryView, model, itemsRepository);
            AddController(inventoryController);

            return inventoryController;
        }

        private IItemsRepository CreateRepository()
        {
            var dataSourcePath = new ResourcePath("Configs/Inventory/ItemConfigDataSource");
            ItemConfig[] itemConfigs = ContentDataSourceLoader.LoadItemConfigs(dataSourcePath);
            var repository = new ItemsRepository(itemConfigs);
            AddRepository(repository);

            return repository;
        }

        private IInventoryView LoadView(Transform placeForUi)
        {
            var viewPath = new ResourcePath("Prefabs/Inventory/InventoryView");
            GameObject prefab = ResourcesLoader.LoadPrefab(viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi);
            AddGameObject(objectView);

            return objectView.GetComponent<InventoryView>();
        }
    }
}