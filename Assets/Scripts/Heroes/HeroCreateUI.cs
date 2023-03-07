using EG.Tower.Heroes.Skills;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EG.Tower.Game
{
    public class HeroCreateUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameLabel;
        [SerializeField] private DropDownExtended _strengthSkillDropdown;
        [SerializeField] private DropDownExtended _weaknessSkillDropdown;
        [SerializeField] private RectTransform _skillsPanel;
        [SerializeField] private SkillUI _skillUiPrefab;
        [SerializeField] private Button _startGameButton;

        [SerializeField] private Color _errorColor = Color.red;
        [SerializeField] private float _errorTime = 1f;

        private HeroCreateController _controller;
        private List<SkillUI> _skillUiItems;

        private void Awake()
        {
            _controller = FindObjectOfType<HeroCreateController>();
            _skillUiItems = new List<SkillUI>();
        }

        private void Start()
        {
            _startGameButton.onClick.AddListener(HandleCreateHeroButtonClick);
            _nameLabel.text = _controller.HeroName;
            InitDropdowns(_controller.Skills);
            InitTraitsUI(_controller);
        }

        private void InitDropdowns(Skill[] skills)
        {
            var options = skills.Select(s => s.Name).ToList();
            _strengthSkillDropdown.onValueChanged.AddListener(HandleMainVirtueChanged);
            _weaknessSkillDropdown.onValueChanged.AddListener(HandleMainViceChanged);
            _strengthSkillDropdown.AddOptions(options);
            _weaknessSkillDropdown.AddOptions(options);
        }

        private void InitTraitsUI(HeroCreateController controller)
        {
            ClearTraits();
            foreach (var skill in controller.Skills)
            {
                var skillUI = Instantiate(_skillUiPrefab, _skillsPanel);
                skillUI.Init(skill);
                _skillUiItems.Add(skillUI);
            }
        }

        private void HandleMainVirtueChanged(int index)
        {
            _weaknessSkillDropdown.EnableAll();

            if (index > 0)
            {
                var virtue = _strengthSkillDropdown.GetText(index);
                _controller.SetStrengthSkill(virtue);
                _weaknessSkillDropdown.SetOptionEnabled(index, false);
            }
            else
            {
                _controller.ResetMainVirtue();
            }
        }

        private void HandleMainViceChanged(int index)
        {
            _strengthSkillDropdown.EnableAll();

            if (index > 0)
            {
                var vice = _weaknessSkillDropdown.GetText(index);
                _controller.SetWeaknessSkill(vice);
                _strengthSkillDropdown.SetOptionEnabled(index, false);
            }
            else
            {
                _controller.ResetMainVice();
            }
        }

        private void ClearTraits()
        {
            var traitsUI = _skillsPanel.GetComponentsInChildren<SkillUI>();

            for (int i = 0; i < traitsUI.Length; i++)
            {
                Destroy(traitsUI[i].gameObject);
            }

            _skillUiItems.Clear();
        }

        private void HandleCreateHeroButtonClick()
        {
            bool isValid = IsStrengthSkillSelected() & IsWeaknessSkillSelected();
            if (isValid)
            {
                _controller.CreateHero();
            }
        }

        private bool IsStrengthSkillSelected()
        {
            bool valid = _strengthSkillDropdown.value != 0;
            if (!valid)
            {
                StartCoroutine(ShowValidationError(_strengthSkillDropdown));
            }

            return valid;
        }

        private bool IsWeaknessSkillSelected()
        {
            bool valid = _weaknessSkillDropdown.value != 0;
            if (!valid)
            {
                StartCoroutine(ShowValidationError(_weaknessSkillDropdown));
            }

            return _weaknessSkillDropdown.value != 0;
        }

        private System.Collections.IEnumerator ShowValidationError(Selectable uiSelectable)
        {
            var originalColor = uiSelectable.targetGraphic.color;
            uiSelectable.targetGraphic.color = _errorColor;
            float blendTime = 0f;

            while (blendTime <= _errorTime)
            {
                blendTime += Time.deltaTime;
                uiSelectable.targetGraphic.color = Color.Lerp(_errorColor, originalColor, blendTime / _errorTime);
                yield return null;
            }

            uiSelectable.targetGraphic.color = originalColor;
        }
    }
}