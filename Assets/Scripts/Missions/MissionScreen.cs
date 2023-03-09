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
        [SerializeField] private TMP_Text _successChanceLabel;
        [SerializeField] private Image _heroSelectorPortrait;
        [SerializeField] private MissionActionSelectorUI _actionSelectorUi;
        [SerializeField] private Button _executeStepButton;
        [SerializeField] private Button _nextStepButton;


        [Header("Progress")]
        [SerializeField] private MissionProgressUI _missionProgressUI;

        private MissionController _controller;

        private void Awake()
        {
            _controller = FindObjectOfType<MissionController>();
            _controller.OnCharacterSelectedEvent += HandleCharacterSelectedEvent;
            _controller.OnCurrentStepChangedEvent += HandleCurrentStepChangedEvent;
            _executeStepButton.onClick.AddListener(HandleExecuteButtonClick);
            _nextStepButton.onClick.AddListener(HandleNextStepButtonClick);

            _successChanceLabel.enabled = false;
            _executeStepButton.interactable = false;
            _nextStepButton.gameObject.SetActive(false);
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

            step.OnCompletedEvent += HandleCompletedEvent;
            step.OnSuccessChanceChangedEvent += HandleSuccessChanceChangedEvent;
        }

        private void HandleCompletedEvent(StepCompletionInfo info)
        {
            _characterUi.ShowRoll(info.HeroRoll);
            _missionBriefUi.ShowRoll(info.EnemyRoll);

            _actionSelectorUi.SetInteraction(false);
            _executeStepButton.gameObject.SetActive(false);
            _nextStepButton.gameObject.SetActive(true);
        }

        private void HandleSuccessChanceChangedEvent(bool shouldShow, float chance)
        {
            var intValue = Mathf.RoundToInt(chance * 100f);
            _successChanceLabel.text = $"Success chance: {intValue}%";
            _successChanceLabel.enabled = shouldShow;
            _executeStepButton.interactable = shouldShow;
        }

        private void HandleActionSelectedEvent(SkillCheckData skillCheck)
        {
            _controller.SelectAction(skillCheck);
        }

        private void HandleExecuteButtonClick()
        {
            // TODO: Validate selectors
            _controller.ExecuteStep();
        }

        private void HandleNextStepButtonClick()
        {
            _successChanceLabel.enabled = false;
            _executeStepButton.gameObject.SetActive(true);
            _executeStepButton.interactable = false;
            _nextStepButton.gameObject.SetActive(false);
            _actionSelectorUi.SetInteraction(true);
            _characterUi.HideRoll();
            _missionBriefUi.HideRoll();
            _controller.SetNextStep();
        }
    }
}
