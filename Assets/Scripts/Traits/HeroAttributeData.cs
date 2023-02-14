using System;
using System.Collections.Generic;
using UnityEngine;

namespace EG.Tower.Game
{
    [CreateAssetMenu(fileName = "HeroAttributeData", menuName = "Data/Hero/AttributeData", order = 1)]
    public class HeroAttributeData : ScriptableObject
    {
        public string Name;
        public HeroAttributeType ConvertType;
        public float Divisor = 10f;

        private Dictionary<HeroAttributeType, Func<int, int>> _convertMethods;

        private void OnEnable()
        {
            _convertMethods = new Dictionary<HeroAttributeType, Func<int, int>>
            {
                { HeroAttributeType.PercentAsIs, ConvertAsIs },
                { HeroAttributeType.Points, ConvertDivision }
            };
        }

        public HeroAttributeModel GetAttribute(int traitValue)
        {
            int value = 0;

            if (_convertMethods.TryGetValue(ConvertType, out var method))
            {
                value = method.Invoke(traitValue);
            }

            return new HeroAttributeModel(Name, ConvertType, value);
        }

        private int ConvertDivision(int traitValue)
        {
            return Divisor > 0f ? Mathf.RoundToInt(traitValue / Divisor) : 0;
        }

        private int ConvertAsIs(int traitValue)
        {
            return traitValue;
        }
    }
}
