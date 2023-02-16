using EG.Tower.Game.Battle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EG.Tower.Game
{
    [Serializable]
    public class HeroModel
    {
        private const float TRAIT_DEFAULT_VALUE = 50f;

        [field: SerializeField]
        public string Name { get; private set; }

        [field: SerializeField]
        public Trait[] Traits { get; private set; }

        [field: SerializeField]
        public Trait MainVirtueTrait { get; private set; }

        [field: SerializeField]
        public Trait MainViceTrait { get; private set; }

        [field: SerializeField]
        public int HP { get; private set; }

        [field: SerializeField]
        public int MaxHP { get; internal set; }

        [field: SerializeField]
        public int Supplies { get; private set; }

        [field: SerializeField]
        public int Money { get; private set; }

        [field: SerializeField]
        public int TurnEnergy { get; private set; }

        [field: SerializeField]
        public HeroInspirationModel Inspiration { get; private set; }

        private Dictionary<VirtueType, Trait> _virtueTraits;

        private Dictionary<HeroAttributeType, Func<Trait, int>> _convertMethods;

        public BattleAttributeItemModel[] BattleAttributes { get; private set; }

        public HeroModel(HeroCreateModel createModel)
        {
            Name = createModel.Name;
            Traits = createModel.Traits;
            MainVirtueTrait = createModel.MainVirtueTrait;
            MainViceTrait = createModel.MainViceTrait;
            _virtueTraits = Traits.ToDictionary(t => t.VirtueType, t => t);
            HP = createModel.MaxHP;
            MaxHP = createModel.MaxHP;
            Supplies = createModel.Supplies;
            Money = createModel.Money;
            TurnEnergy = createModel.TurnEnergy;
            Inspiration = createModel.Inspiration;
            InitBattleAtributes();
        }

        public HeroModel(string name, HeroSetupData setupData)
        {
            Name = name;
            Traits = setupData.GetTraits();
            MainVirtueTrait = null;
            MainViceTrait = null;
            _virtueTraits = Traits.ToDictionary(t => t.VirtueType, t => t);
            HP = setupData.MaxHP;
            MaxHP = setupData.MaxHP;
            Inspiration = setupData.Inspiration;
            Supplies = setupData.Supplies;
            Money = setupData.Money;
            TurnEnergy = setupData.TurnEnergy;
            InitBattleAtributes();
        }

        private void InitBattleAtributes()
        {
            _convertMethods = new()
            {
                { HeroAttributeType.PercentAsIs, ConvertAsIs },
                { HeroAttributeType.Points, ConvertPoints },
                { HeroAttributeType.PercentDivision, ConvertPercentDivision }
            };

            BattleAttributes = Traits.Select(CreateBattleAttribute).ToArray();
        }

        public double FindVirtueTraitValue(string name)
        {
            double traitValue = TRAIT_DEFAULT_VALUE;
            if (TryFindVirtueTrait(name, out Trait trait))
            {
                traitValue = trait.Value;
            }

            return traitValue;
        }

        public bool TryFindVirtueTrait(string name, out Trait trait)
        {
            trait = null;
            bool success = Enum.TryParse(name, out VirtueType type) && TryFindVirtueTrait(type, out trait);

            return success;
        }

        public bool TryFindVirtueTrait(VirtueType type, out Trait trait)
        {
            bool success = _virtueTraits.TryGetValue(type, out trait);
            if (!success)
            {
                Debug.LogWarning($"Trait {type} not found!");
            }

            return success;
        }

        public BattleAttributeItemModel CreateBattleAttribute(Trait trait)
        {
            int value = 0;

            if (_convertMethods.TryGetValue(trait.AttributeType, out var method))
            {
                value = method.Invoke(trait);
            }

            return new BattleAttributeItemModel(
                trait.VirtueType,
                trait.Virtue,
                trait.Value,
                value,
                trait.AttributeType,
                trait.BattleAttribute);
        }

        private int ConvertAsIs(Trait trait)
        {
            return trait.Value;
        }

        private int ConvertPoints(Trait trait)
        {
            return trait.Divisor > 0f ? Mathf.RoundToInt(trait.Value / trait.Divisor) : 0;
        }

        private int ConvertPercentDivision(Trait trait)
        {
            return trait.Divisor > 0f ? Mathf.RoundToInt(trait.Value / trait.Divisor) : 0;
        }
    }
}