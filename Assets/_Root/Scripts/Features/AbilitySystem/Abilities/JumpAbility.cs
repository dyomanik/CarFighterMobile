using System;
using UnityEngine;
using JetBrains.Annotations;

namespace Features.AbilitySystem.Abilities
{
    internal class JumpAbility : IAbility
    {
        private readonly IAbilityItem _config;

        public JumpAbility([NotNull] IAbilityItem config) =>
            _config = config ?? throw new ArgumentNullException(nameof(config));

        public void Apply(IAbilityActivator activator)
        {
            var carRigidbody = activator.ViewGameObject.gameObject.GetComponent<Rigidbody2D>();
            Vector3 force = activator.ViewGameObject.transform.up * _config.Value;
            carRigidbody.AddForce(force, ForceMode2D.Force);
        }
    }
}