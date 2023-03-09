using EG.Tower.Missions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EG.Tower.Game
{
    public class MissionScreen : MonoBehaviour
    {
        [Header("Side Panels")]
        [SerializeField] private SelectedCharacterUI _characterUi;
        [SerializeField] private MissionBriefUI _missionBriefUi;

        [Header("Current Step")]
        [SerializeField] private TMP_Text _missionTitleLabel;
        [SerializeField] private TMP_Text _currentStepLabel;
        [SerializeField] private Image _heroSelectorPortrait;
        [SerializeField] private MissionActionSelectorUI _actionSelectorUi;
        [SerializeField] private Button _executeStepButton;

        [Header("Progress")]
        [SerializeField] private MissionProgressUI _missionProgressUI;

        private MissionController _controller;

        private void Awake()
        {
            _controller = FindObjectOfType<MissionController>();
            _controller.OnCharacterSelectedEvent += HandleCharacterSelectedEvent;
            _controller.OnCurrentStepChangedEvent += HandleCurrentStepChangedEvent;
            _controller.OnHeroRollEvent += HandleHeroRollEvent;
            _controller.OnEnemyRollEvent += HandleEnemyRollEvent;
            _executeStepButton.onClick.AddListener(HandleExecuteButtonClick);
        }

        private void Start()
        {
            Init(_controller);
        }

        private void Init(MissionController controller)
        {
            _missionTitleLabel.text = controller.MissionName;
            _missionBriefUi.Init(controller.Region, controller.Faction, controller.MissionSkills);
            _missionProgressUI.Init(controller.Steps);
        }

        private void HandleCharacterSelectedEvent(HeroModel hero)
        {
            _heroSelectorPortrait.sprite = hero.Portrait;
            _heroSelectorPortrait.color = new Color(_heroSelectorPortrait.color.r, _heroSelectorPortrait.color.g, _heroSelectorPortrait.color.b, 1f);
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

        private void HandleHeroRollEvent(int[] rolls)
        {
            _characterUi.ShowRoll(rolls);
        }

        private void HandleEnemyRollEvent(int[] rolls)
        {
            _missionBriefUi.ShowRoll(rolls);
        }

        private void HandleExecuteButtonClick()
        {
            // TODO: Validate selectors
            _controller.ExecuteStep();
        }
    }
}
