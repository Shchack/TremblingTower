using EG.Tower.Game;
using EG.Tower.Heroes.Skills;
using EG.Tower.Utils;
using System;
using System.Linq;
using UnityEngine;

namespace EG.Tower.Missions
{
    public class MissionController : MonoBehaviour
    {
        public event Action<MissionStep> OnCurrentStepChangedEvent;
        public event Action OnMissionEndEvent;

        private MissionData _data => GameHub.One.NextMissionData;
        public HeroModel[] Heroes => GameHub.One.Session.Team;

        public string MissionName => _data.Name;
        public MissionRegionType Region => _data.Region;
        public FactionType Faction => _data.Faction;
        public SkillValueData[] MissionSkills => _data.MissionSkills;

        public MissionStep[] Steps { get; private set; }
        public bool IsMissionEnd => _currentStepIndex >= (Steps.Length - 1);


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

        public void SelectHero(HeroModel hero)
        {
            _currentStep.SelectCharacter(hero);
        }

        public void SelectAction(SkillCheckData skillCheck)
        {
            _currentStep.SelectAction(skillCheck);
        }

        public void ExecuteStep()
        {
            if (!_currentStep.TryExecute())
            {
                Debug.LogError("Failed to execute step. No selections.", gameObject);
            }
        }

        public void SetNextStep()
        {
            if (_currentStepIndex + 1 < Steps.Length)
            {
                _currentStepIndex++;
                OnCurrentStepChangedEvent?.Invoke(_currentStep);
            }
            else
            {
                Debug.LogWarning("Ending mission!");
                OnMissionEndEvent?.Invoke();
            }
        }

        public void EndMission()
        {
            SceneHelper.LoadMapScene();
        }
    }
}
