using PixelCrushers.DialogueSystem;
using System.Collections.Generic;
using UnityEngine;

namespace EG.Tower.Game
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private TraitsData _defaultTraits;

        private const float TRAIT_MAX_VALUE = 100f;
        private const float TRAIT_DEFAULT_VALUE = 50f;

        [SerializeField] private HeroModel _heroModel;

        private Dictionary<VirtueType, Trait> _virtueTraits;

        private void Start()
        {
            _heroModel = GameHub.One.Session.HeroModel;
            if (_heroModel == null)
            {
                _heroModel = new HeroModel("Hero", _defaultTraits);
                GameHub.One.Session.SetHeroModel(_heroModel);
            }
        }

        public void CheckVirtue(string virtueName, double rollTypeValue)
        {
            var randomChance = Random.Range(0f, TRAIT_MAX_VALUE);
            double traitValue = _heroModel.FindVirtueTraitValue(virtueName);

            var checkValue = traitValue + rollTypeValue;
            bool check = randomChance <= checkValue;
            DialogueLua.SetVariable("CheckResult", check);

            Debug.Log($"{virtueName} check result: {check}. {checkValue} agains {randomChance}");
        }

        public void GiveTraitReward(string virtueName, double value)
        {
            Debug.Log($"Giving trait {virtueName} reward {value}");

            if (_heroModel.TryFindVirtueTrait(virtueName, out Trait trait))
            {
                trait.AddValue(value);
                Debug.Log($"Trait {trait.Virtue} Value is {trait.Value}");
            }
        }

        private void OnEnable()
        {
            Lua.RegisterFunction(nameof(CheckVirtue), this, SymbolExtensions.GetMethodInfo(() => CheckVirtue(string.Empty, (double)0)));
            Lua.RegisterFunction(nameof(GiveTraitReward), this, SymbolExtensions.GetMethodInfo(() => GiveTraitReward(string.Empty, (double)0)));
        }

        private void OnDisable()
        {
            Lua.UnregisterFunction(nameof(CheckVirtue));
            Lua.UnregisterFunction(nameof(GiveTraitReward));
        }
    }
}