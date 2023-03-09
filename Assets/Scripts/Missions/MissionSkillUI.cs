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
            string valueStr = "0";
            if (value > 0)
            {
                valueStr = $"+{value}";
            }
            if (value < 0)
            {
                valueStr = $"-{Mathf.Abs(value)}";
            }

            _valueLabel.text = valueStr;
        }
    }
}
