using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EG.Tower.Missions
{
    public class MissionStepActionUI : MonoBehaviour
    {
        public event Action<SkillCheckData> OnStepActionSelectedEvent;

        [SerializeField] private Button _selectButton;
        [SerializeField] private TMP_Text _optionLabel;

        private SkillCheckData _skillCheck;

        private void Awake()
        {
            _selectButton.onClick.AddListener(OnSelectButtonClicked);
        }

        private void OnSelectButtonClicked()
        {
            if (_skillCheck != null)
            {
                OnStepActionSelectedEvent?.Invoke(_skillCheck);
            }
            else
            {
                Debug.LogError("Skill check not set in UI selector");
            }
        }

        public void Init(int i, SkillCheckData skillCheckData)
        {
            _skillCheck = skillCheckData;
            _optionLabel.text = $"{i+1}. [{skillCheckData.Skill.AltName}] {skillCheckData.Name}";
        }
    }
}