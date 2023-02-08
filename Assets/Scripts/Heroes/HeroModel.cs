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

        private Dictionary<VirtueType, Trait> _virtueTraits;

        public HeroModel(HeroCreateModel createModel)
        {
            Name = createModel.Name;
            Traits = createModel.Traits;
            MainVirtueTrait = createModel.MainVirtueTrait;
            MainViceTrait = createModel.MainViceTrait;
            _virtueTraits = Traits.ToDictionary(t => t.VirtueType, t => t);
        }

        public HeroModel(string name, TraitsData defaultTraits)
        {
            Name = name;
            Traits = defaultTraits.GetTraits();
            MainVirtueTrait = null;
            MainViceTrait = null;
            _virtueTraits = Traits.ToDictionary(t => t.VirtueType, t => t);
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
            bool success = Enum.TryParse(name, out VirtueType type) && _virtueTraits.TryGetValue(type, out trait);
            if (!success)
            {
                Debug.LogWarning($"Trait {name} not found! Default value: [{TRAIT_DEFAULT_VALUE}]");
            }

            return success;
        }
    }
}