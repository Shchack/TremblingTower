using EG.Tower.Game.Utils;
using System;
using UnityEngine;

namespace EG.Tower.Game
{
    [Serializable]
    public class Trait
    {
        private const int TRAIT_MIN_VALUE = 0;

        public event Action<int> OnValueChangedEvent;

        [field: SerializeField, ReadOnlyField]
        public VirtueType VirtueType { get; private set; }

        [field: SerializeField, ReadOnlyField]
        public string Virtue { get; private set; }

        [field: SerializeField, ReadOnlyField]
        public string Vice { get; private set; }

        [field: SerializeField, ReadOnlyField]
        public int DefaultValue { get; private set; }

        [field: SerializeField, ReadOnlyField]
        public int MaxValue { get; private set; }

        [SerializeField] private int _value;
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

        public Trait(TraitData data)
        {
            VirtueType = data.VirtueType;
            Virtue = data.Virtue;
            Vice = data.Vice;
            Value = data.DefaultValue;
            DefaultValue = data.DefaultValue;
            MaxValue = data.MaxValue;
        }

        public void AddValue(double value)
        {
            if (value > 0)
            {
                IncreaseValue((int)value);
            }
            else
            {
                DecreaseValue((int)value);
            }
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
