using PixelCrushers.DialogueSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EG.Tower.Game
{
    public class Hero : MonoBehaviour
    {
        private const float TRAIT_MAX_VALUE = 100f;
        private const double TRAIT_DEFAULT_VALUE = 50f;

        private Dictionary<string, Trait> _virtueTraits;

        private void Start()
        {
            _virtueTraits = GameHub.One.Session.HeroModel.Traits.ToDictionary(t => t.Virtue, t => t);
        }

        public void CheckVirtueTrait(string virtueName, double checkValue)
        {
            var randomChance = Random.Range(0f, TRAIT_MAX_VALUE);

            double traitValue = FindVirtueTraitValue(virtueName);

            bool check = randomChance <= traitValue;
            DialogueLua.SetVariable("IsSuccess", check);

            Debug.Log($"Is Success? {check}");
        }

        public void GiveTraitReward(string virtueName, double value)
        {
            Debug.Log($"Giving trait {virtueName} reward {value}");

            if (_virtueTraits.TryGetValue(virtueName, out var trait))
            {
                trait.AddValue(value);
                Debug.Log($"Trait {virtueName} Value is {trait.Value}");
            }
            else
            {
                Debug.LogWarning($"Trait {virtueName} not found!.", this);
            }
        }

        private double FindVirtueTraitValue(string name)
        {
            double traitValue = TRAIT_DEFAULT_VALUE;
            if (_virtueTraits.TryGetValue(name, out var trait))
            {
                traitValue = trait.Value;
            }
            else
            {
                Debug.LogWarning($"Trait {name} not found! Default trait value: [{TRAIT_DEFAULT_VALUE}]", this);
            }

            return traitValue;
        }

        private void OnEnable()
        {
            Lua.RegisterFunction(nameof(CheckVirtueTrait), this, SymbolExtensions.GetMethodInfo(() => CheckVirtueTrait(string.Empty, (double)0)));
            Lua.RegisterFunction(nameof(GiveTraitReward), this, SymbolExtensions.GetMethodInfo(() => GiveTraitReward(string.Empty, (double)0)));
        }

        private void OnDisable()
        {
            Lua.UnregisterFunction(nameof(CheckVirtueTrait));
            Lua.UnregisterFunction(nameof(GiveTraitReward));
        }
    }
}