using Features.AbilitySystem.Abilities;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;

namespace Features.AbilitySystem
{
    internal interface IAbilitiesController
    { }

    internal class AbilitiesController : BaseController, IAbilitiesController
    {
        private readonly IAbilitiesView _view;
        private readonly IAbilitiesRepository _repository;
        private readonly IAbilityActivator _abilityActivator;

        public AbilitiesController(
            [NotNull] IAbilitiesView abilitiesView,
            [NotNull] IAbilitiesRepository abilitiesRepository,
            [NotNull] IEnumerable<IAbilityItem> abilityItems,
            [NotNull] IAbilityActivator abilityActivator)
        {
            
            _view = abilitiesView ?? throw new ArgumentNullException(nameof(abilitiesView));
            
            _repository 
                = abilitiesRepository ?? throw new ArgumentNullException(nameof(abilitiesRepository));

            if (abilityItems == null)
                throw new ArgumentNullException(nameof(abilityItems));
            
            _abilityActivator
                = abilityActivator ?? throw new ArgumentNullException(nameof(abilityActivator));

            _view.Display(abilityItems, OnAbilityViewClicked);
        }

        protected override void OnDispose() => _view.Clear();

        private void OnAbilityViewClicked(string abilityId)
        {
            if (_repository.Items.TryGetValue(abilityId, out IAbility ability))
                ability.Apply(_abilityActivator);
        }
    }
}