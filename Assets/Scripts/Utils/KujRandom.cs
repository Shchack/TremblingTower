using UnityEngine;

namespace EG.Tower.Utils
{
    public static class KujRandom
    {
        public static int Int(int fromInclusive, int toExclusive) => Random.Range(fromInclusive, toExclusive);
        public static float Float(float fromInclusive, float toInclusive) => Random.Range(fromInclusive, toInclusive);

        public static int IntRange(IntRange range)
        {
            int result = 0;

            if (range.Min <= range.Max)
            {
                result = Int(range.Min, range.Max + 1);
            }
            else
            {
                Debug.LogWarning($"({nameof(KujRandom)}). Range Min value is bigger than Range Max value. Range [{range.Min}, {range.Max}]");
            }

            return result;
        }

        public static float FloatRange(FloatRange range)
        {
            float result = 0f;

            if (range.Min <= range.Max)
            {
                result = Float(range.Min, range.Max);
            }
            else
            {
                Debug.LogWarning($"({nameof(KujRandom)}). Range Min value is bigger than Range Max value. Range [{range.Min}, {range.Max}]");
            }

            return result;
        }

        public static int Index(int length) => Random.Range(0, length - 1);

        public static float Chance() => Random.Range(0f, 1f);

        public static bool Bool() => Index(2) == 1;
    }
}
