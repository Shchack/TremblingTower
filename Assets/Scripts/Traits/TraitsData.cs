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


        private List<string> _virtues = null;
        public List<string> Virtues
        {
            get
            {
                if (_virtues == null || _virtues.Count == 0)
                {
                    _virtues = _traits.Select(t => t.Virtue).ToList();
                }

                return _virtues;
            }
        }

        private List<string> _vices = null;
        public List<string> Vices
        {
            get
            {
                if (_vices == null || _vices.Count == 0)
                {
                    _vices = _traits.Select(t => t.Vice).ToList();
                }

                return _vices;
            }
        }

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
    }
}