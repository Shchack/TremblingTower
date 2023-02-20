using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EG.Tower.Game
{
    [CreateAssetMenu(fileName = "HeroSetupData", menuName = "Data/Hero/HeroSetup", order = 0)]
    public class HeroSetupData : ScriptableObject
    {
        [SerializeField] private TraitData[] _traits;
        [SerializeField] private int _mainVirtueBoost = 10;
        [SerializeField] private int _mainViceBoost = 10;

        [SerializeField] private int _maxHP = 20;
        [SerializeField] private int _supplies = 5;
        [SerializeField] private int _money = 5;
        [SerializeField] private HeroInspirationData _inspirationData;

        public TraitData[] Traits => _traits;
        public int MainVirtueBoost => _mainVirtueBoost;
        public int MainViceBoost => _mainViceBoost;
        public List<string> Virtues => _traits.Select(t => t.Virtue).ToList();
        public List<string> Vices => _traits.Select(t => t.Vice).ToList();

        public int MaxHP => _maxHP;
        public int Supplies => _supplies;
        public int Money => _money;
        public HeroInspirationModel Inspiration => new HeroInspirationModel(_inspirationData);

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
        public HeroAttributeType AttributeType;
        public float Divisor;
        public BattleAttributeData BattleAttribute;
    }
}