using EG.Tower.Heroes.Skills;
using TMPro;
using UnityEngine;

namespace EG.Tower.Game
{
    public class SkillUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _fullNameLabel;
        [SerializeField] private TMP_Text _valueLabel;

        private Skill _skill;

        public void Init(Skill skill)
        {
            _skill = skill;
            _fullNameLabel.text = $"{skill.Name} [<i>{skill.AltName}</i>]";
            _valueLabel.text = skill.Value.ToString();
            _skill.OnValueChangedEvent += HandleValueChangedEvent;
        }

        private void HandleValueChangedEvent(int value)
        {
            _valueLabel.text = value.ToString();
        }

        private void OnDestroy()
        {
            if (_skill != null)
            {
                _skill.OnValueChangedEvent -= HandleValueChangedEvent;
            }
        }
    }
}