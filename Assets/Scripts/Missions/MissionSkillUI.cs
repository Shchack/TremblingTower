using TMPro;
using UnityEngine;

namespace EG.Tower.Missions
{
    public class MissionSkillUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameLabel;
        [SerializeField] private TMP_Text _valueLabel;

        public void Init(string name, int value)
        {
            _nameLabel.text = name;
            string valueStr = value >= 0 ? $"+{value}" : $"-{value}";
            _valueLabel.text = valueStr;
        }
    }
}
