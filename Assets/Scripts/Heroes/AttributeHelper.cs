using System;
using System.Collections.Generic;
using UnityEngine;

namespace EG.Tower.Game.Heroes
{
    public static class AttributeHelper
    {
        private static Dictionary<HeroAttributeType, Func<float, int, int>> _convertMethods = new()
        {
            { HeroAttributeType.PercentAsIs, ConvertAsIs },
            { HeroAttributeType.Points, ConvertPoints },
            { HeroAttributeType.PercentDivision, ConvertPercentDivision }
        };

        public static int Convert(HeroAttributeType type, float divisor, int traitValue)
        {
            int value = 0;

            if (_convertMethods.TryGetValue(type, out var method))
            {
                value = method.Invoke(divisor, traitValue);
            }

            return value;
        }

        private static int ConvertAsIs(float divisor, int traitValue)
        {
            return traitValue;
        }

        private static int ConvertPoints(float divisor, int traitValue)
        {
            return divisor > 0f ? Mathf.RoundToInt(traitValue / divisor) : 0;
        }

        private static int ConvertPercentDivision(float divisor, int traitValue)
        {
            return divisor > 0f ? Mathf.RoundToInt(traitValue / divisor) : 0;
        }
    }
}
