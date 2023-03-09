using EG.Tower.Game;
using System;
using System.Linq;
using UnityEngine;

namespace EG.Tower.Missions
{
    public class MissionController : MonoBehaviour
    {
        public event Action<HeroModel> OnCharacterSelectedEvent;
        public event Action<MissionStep> OnCurrentStepChangedEvent;

        [SerializeField] private MissionData _data;

        public string MissionName => _data.Name;
        public MissionRegionType Region => _data.Region;
        public FactionType Faction => _data.Faction;
        public MissionSkillData[] MissionSkills => _data.MissionSkills;

        public MissionStep[] Steps { get; private set; }

        private int _currentStepIndex;
        private MissionStep _currentStep => Steps[_currentStepIndex];

        private void Awake()
        {
            Steps = _data.Steps.Select(s => new MissionStep(s, _data.MissionSkills)).ToArray();
        }

        private void Start()
        {
            BeginMission();
        }

        private void BeginMission()
        {
            _currentStepIndex = -1;
            SetNextStep();
        }

        private void SelectCharacter()
        {
            var selectedCharacter = GameHub.One.Session.HeroModel;
            _currentStep.SelectCharacter(selectedCharacter);
            OnCharacterSelectedEvent?.Invoke(selectedCharacter);
        }

        public void SelectAction(SkillCheckData skillCheck)
        {
            _currentStep.SelectAction(skillCheck);
        }

        public void ExecuteStep()
        {
            if (_currentStep.TryExecute())
            {
            }
            else
            {
                Debug.LogError("Failed to execute step. No selections.", gameObject);
            }
        }

        public void SetNextStep()
        {
            if (_currentStepIndex + 1 < Steps.Length)
            {
                _currentStepIndex++;
                SelectCharacter();
                OnCurrentStepChangedEvent?.Invoke(_currentStep);
            }
            else
            {
                EndMission();
            }
        }

        private void EndMission()
        {
            Debug.LogWarning("Mission end!");
        }
    }
}
