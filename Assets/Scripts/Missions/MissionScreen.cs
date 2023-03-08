using EG.Tower.Missions;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EG.Tower.Game
{
    public class MissionScreen : MonoBehaviour
    {
        [Header("Mission Brief")]
        [SerializeField] private TMP_Text _missionTitleLabel;
        [SerializeField] private TMP_Text _regionLabel;
        [SerializeField] private TMP_Text _factionLabel;
        [SerializeField] private RectTransform _missionSkillsHolder;
        [SerializeField] private MissionSkillUI _missionSkillUiPrefab;

        [Header("Mission Steps")]
        [SerializeField] private RectTransform _missionStepsHolder;
        [SerializeField] private MissionStepUI _missionStepUiPrefab;

        [Header("Current Action")]
        [SerializeField] private Image _portrait;
        [SerializeField] private MissionActionSelectorUI _actionSelectorUi;
        [SerializeField] private SelectedCharacterUI _characterUi;
        [SerializeField] private TMP_Text _currentStepLabel;

        private MissionController _controller;

        private void Awake()
        {
            _controller = FindObjectOfType<MissionController>();
            _controller.OnCharacterSelectedEvent += HandleCharacterSelectedEvent;
            _controller.OnCurrentStepChangedEvent += HandleCurrentStepChangedEvent;
        }

        private void HandleCharacterSelectedEvent(HeroModel hero)
        {
            _portrait.sprite = hero.Portrait;
            _portrait.color = new Color(_portrait.color.r, _portrait.color.g, _portrait.color.b, 1f);
            _characterUi.Init(hero);
        }

        private void HandleCurrentStepChangedEvent(MissionStep step)
        {
            _currentStepLabel.text = $"Step {step.Name}";
            _actionSelectorUi.Init(step.PossibleSkillChecks);
            _actionSelectorUi.OnActionSelectedEvent += HandleActionSelectedEvent;
        }

        private void HandleActionSelectedEvent(SkillCheckData skillCheck)
        {
            _controller.SelectAction(skillCheck);
        }

        private void Start()
        {
            Init(_controller);
        }

        private void Init(MissionController controller)
        {
            _missionTitleLabel.text = controller.MissionName;
            _regionLabel.text = $"Region: {controller.Region}";
            _factionLabel.text = $"Faction: {controller.Faction}";
            InitMissionSkills(controller.MissionSkills);
            InitSteps(controller.Steps);
        }

        private void InitMissionSkills(MissionSkillData[] skills)
        {
            var existingSkills = _missionSkillsHolder.GetComponentsInChildren<MissionSkillUI>();

            for (int i = 0; i < existingSkills.Length; i++)
            {
                Destroy(existingSkills[i].gameObject);
            }

            foreach (var skill in skills)
            {
                var skillUi = Instantiate(_missionSkillUiPrefab, _missionSkillsHolder);
                skillUi.Init(skill.Skill.AltName, skill.Value);
            }
        }

        private void InitSteps(MissionStep[] steps)
        {
            var existingSteps = _missionStepsHolder.GetComponentsInChildren<MissionStepUI>();

            for (int i = 0; i < existingSteps.Length; i++)
            {
                Destroy(existingSteps[i].gameObject);
            }

            foreach (var step in steps)
            {
                var stepUi = Instantiate(_missionStepUiPrefab, _missionStepsHolder);
                stepUi.Init(step);
            }
        }
    }
}
