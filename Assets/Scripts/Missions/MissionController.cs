using EG.Tower.Game;
using System;
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
        public MissionStep[] Steps => _data.Steps;

        public MissionSkillData[] MissionSkills => _data.MissionSkills;

        private HeroModel _selectedCharacter => GameHub.One.Session.HeroModel;
        private SkillCheckData _selectedAction;

        private int _nextStepIndex = 0;

        private void Start()
        {
            BeginMission();
        }

        private void BeginMission()
        {
            SelectCharacter();
            SetNextStep();
        }

        private void SelectCharacter()
        {
            OnCharacterSelectedEvent?.Invoke(_selectedCharacter);
        }

        private void SetNextStep()
        {
            if (_nextStepIndex < Steps.Length)
            {
                var currentStep = Steps[_nextStepIndex];
                OnCurrentStepChangedEvent?.Invoke(currentStep);
                _nextStepIndex++;
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

        public void SelectAction(SkillCheckData skillCheck)
        {
            _selectedAction = skillCheck;
        }
    }
}
