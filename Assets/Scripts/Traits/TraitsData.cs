using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EG.Tower.Game
{
    [CreateAssetMenu(fileName = "TraitsData", menuName = "Data/Hero/Traits", order = 0)]
    public class TraitsData : ScriptableObject
    {
        [SerializeField] private TraitData[] _traits;
        [SerializeField] private int _mainVirtueBoost = 10;
        [SerializeField] private int _mainViceBoost = 10;

        public TraitData[] Traits => _traits;
        public int MainVirtueBoost => _mainVirtueBoost;
        public int MainViceBoost => _mainViceBoost;
        public List<string> Virtues => _traits.Select(t => t.Virtue).ToList();
        public List<string> Vices => _traits.Select(t => t.Vice).ToList();


        public Trait[] GetTraits()
        {
            return Traits
                .Select(t => new Trait(t))
                .ToArray();
        }
    }

    [Serializable]
    public class TraitData
    {
        public VirtueType VirtueType;
        public string Virtue;
        public string Vice;
        public int DefaultValue = 50;
        public int MaxValue = 100;
        public HeroAttributeData AttributeData;
    }
}