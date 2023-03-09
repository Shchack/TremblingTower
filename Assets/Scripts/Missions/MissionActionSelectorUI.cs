using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EG.Tower.Missions
{
    public class MissionActionSelectorUI : MonoBehaviour
    {
        public event Action<SkillCheckData> OnActionSelectedEvent;

        [SerializeField] private Button _selectButton;
        [SerializeField] private Image _plusIconImage;
        [SerializeField] private TMP_Text _selectTextLabel;
        [SerializeField] private RectTransform _optionsPanel;
        [SerializeField] private MissionStepActionUI _actionUiPrefab;

        private Sprite _plusIcon;

        private void Awake()
        {
            _plusIcon = _plusIconImage.sprite;
            _selectButton.onClick.AddListener(HandleSelectButtonClick);
            _optionsPanel.gameObject.SetActive(false);
        }

        private void HandleSelectButtonClick()
        {
            _optionsPanel.gameObject.SetActive(true);
        }

        public void Init(SkillCheckData[] possibleSkillChecks)
        {
            _plusIconImage.sprite = _plusIcon;
            _selectTextLabel.text = "Select action";

            var existingActions = _optionsPanel.GetComponentsInChildren<MissionStepActionUI>();

            for (int i = 0; i < existingActions.Length; i++)
            {
                Destroy(existingActions[i].gameObject);
                existingActions[i].OnStepActionSelectedEvent -= HandleStepActionSelectedEvent;
            }

            for (int i = 0; i < possibleSkillChecks.Length; i++)
            {
                var actionUi = Instantiate(_actionUiPrefab, _optionsPanel.transform);
                actionUi.Init(i, possibleSkillChecks[i]);
                actionUi.OnStepActionSelectedEvent += HandleStepActionSelectedEvent;
            }
        }

        private void HandleStepActionSelectedEvent(SkillCheckData skillCheck)
        {
            _optionsPanel.gameObject.SetActive(false);

            if (skillCheck != null)
            {
                _plusIconImage.sprite = skillCheck.Skill.Icon;
                _selectTextLabel.text = skillCheck.Name;
                OnActionSelectedEvent?.Invoke(skillCheck);
            }
            else
            {
                Debug.LogError("Skill check not set in UI option!");
            }
        }

        public void SetInteraction(bool isInteractable)
        {
            _selectButton.interactable = isInteractable;
        }
    }
}
