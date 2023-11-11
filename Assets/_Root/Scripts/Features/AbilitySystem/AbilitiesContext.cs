using System.Collections.Generic;
using Tool;
using UnityEngine;
using Features.AbilitySystem.Abilities;

namespace Features.AbilitySystem
{
    internal class AbilitiesContext : BaseContext
    {
        public AbilitiesContext(Transform placeForUi, IAbilityActivator abilityActivator) => 
            CreateController(placeForUi, abilityActivator);

        private IAbilitiesController CreateController(Transform placeForUi, IAbilityActivator abilityActivator)
        {
            IAbilitiesView abilitiesView = LoadAbilitiesView(placeForUi);
            IEnumerable<IAbilityItem> abilityItems = LoadAbilityItemConfigs();
            IAbilitiesRepository abilitiesRepository = CreateAbilitiesRepository(abilityItems);
            var abilitiesController = new AbilitiesController(abilitiesView, abilitiesRepository, abilityItems, abilityActivator);
            AddController(abilitiesController);

            return abilitiesController;
        }

        private IAbilityItem[] LoadAbilityItemConfigs()
        {
            var dataSourcePath = new ResourcePath("Configs/Ability/AbilityItemConfigDataSource");
            return ContentDataSourceLoader.LoadAbilityItemConfigs(dataSourcePath);
        }

        private IAbilitiesRepository CreateAbilitiesRepository(IEnumerable<IAbilityItem> abilityItems)
        {
            var repository = new AbilitiesRepository(abilityItems);
            AddRepository(repository);

            return repository;
        }

        private IAbilitiesView LoadAbilitiesView(Transform placeForUi)
        {
            var viewPath = new ResourcePath("Prefabs/Ability/AbilitiesView");
            GameObject prefab = ResourcesLoader.LoadPrefab(viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<IAbilitiesView>();
        }
    }
}