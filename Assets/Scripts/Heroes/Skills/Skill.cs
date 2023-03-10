using System;

namespace EG.Tower.Heroes.Skills
{
    [Serializable]
    public class Skill
    {
        public event Action<int> OnValueChangedEvent;

        public string Name { get; private set; }
        public string AltName { get; private set; }

        private int _value;

        public Skill(SkillValueData data)
        {
            Name = data.Skill.Name;
            AltName = data.Skill.AltName;
            _value = data.Value;
        }

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

        public void AddValue(int value)
        {
            Value += value;
        }

        public string FullName => $"{Name} [{AltName}]";

        public bool HasName(string name)
        {
            return Name.Equals(name, StringComparison.OrdinalIgnoreCase);
        }

        public void ResetValue()
        {
            Value = 0;
        }
    }
}
