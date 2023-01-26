using System;

namespace EG.Tower.Game
{
    public class Trait
    {
        private const int TRAIT_MIN_VALUE = 0;

        public event Action<int> OnValueChangedEvent;

        public string Virtue { get; private set; }
        public string Vice { get; private set; }
        public int DefaultValue { get; private set; }
        public int MaxValue { get; private set; }

        private int _value;
        public int Value
        {
            get
            {
                return _value;
            }
            private set
            {
                _value = value;
                OnValueChangedEvent?.Invoke(_value);
            }
        }

        public Trait(string virtue, string vice, int value, int defaultValue, int maxValue)
        {
            Virtue = virtue;
            Vice = vice;
            Value = value;
            DefaultValue = defaultValue;
            MaxValue = maxValue;
        }

        public void IncreaseValue(int virtueBoost)
        {
            var newValue = Value + virtueBoost;
            Value = newValue <= MaxValue ? newValue : MaxValue;
        }

        public void DecreaseValue(int virtueBoost)
        {
            var newValue = Value - virtueBoost;
            Value = newValue >= TRAIT_MIN_VALUE ? newValue : TRAIT_MIN_VALUE;
        }

        public void ResetValue()
        {
            Value = DefaultValue;
        }

        public bool HasVirtue(string virtue)
        {
            return Virtue.Equals(virtue, StringComparison.CurrentCultureIgnoreCase);
        }

        public bool HasVice(string vice)
        {
            return Vice.Equals(vice, StringComparison.CurrentCultureIgnoreCase);
        }

        public string AsText()
        {
            return $"{Vice} - {Value} - {Virtue}";
        }
    }
}
