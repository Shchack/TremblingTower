using EG.Tower.Game;
using EG.Tower.Rolls;
using System;
using System.Linq;
using UnityEngine;

namespace EG.Tower.Missions
{
    public class MissionController : MonoBehaviour
    {
        public event Action<HeroModel> OnCharacterSelectedEvent;
        public event Action<MissionStep> OnCurrentStepChangedEvent;
        public event Action<int[]> OnHeroRollEvent;
        public event Action<int[]> OnEnemyRollEvent;

        [SerializeField] private MissionData _data;
        [SerializeField] private DiceType _checkRollDice = DiceType.D6;
        [SerializeField] private int _checkRollsCount = 2;

        public string MissionName => _data.Name;
        public MissionRegionType Region => _data.Region;
        public FactionType Faction => _data.Faction;
        public MissionSkillData[] MissionSkills => _data.MissionSkills;

        public MissionStep[] Steps { get; private set; }

        private HeroModel _selectedCharacter => GameHub.One.Session.HeroModel;
        private SkillCheckData _selectedAction;

        private int _currentStepIndex;

        private void Awake()
        {
            Steps = _data.Steps.Select(s => new MissionStep(s)).ToArray();
        }

        private void Start()
        {
            BeginMission();
        }

        private void BeginMission()
        {
            SelectCharacter();
            _currentStepIndex = 0;
            OnCurrentStepChangedEvent?.Invoke(Steps[_currentStepIndex]);
        }

        private void SelectCharacter()
        {
            OnCharacterSelectedEvent?.Invoke(_selectedCharacter);
        }

        private void SetNextStep(bool isSuccess)
        {
            Steps[_currentStepIndex].CompleteStep(isSuccess);

            if (_currentStepIndex + 1 < Steps.Length)
            {
                _currentStepIndex++;
                var newStep = Steps[_currentStepIndex];
                OnCurrentStepChangedEvent?.Invoke(newStep);
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

        public void ExecuteStep()
        {
            if (_selectedCharacter != null && _selectedAction != null)
            {
                bool isSuccess = CheckRollsResult();
                SetNextStep(isSuccess);
            }
            else
            {
                Debug.LogError("Failed to execute step. No selections.", gameObject);
            }
        }

        private bool CheckRollsResult()
        {
            int heroRoll = CalculateHeroRoll();
            int enemyRoll = CalculateEnemyRoll();
            GameHub.One.Audio.CheckResultTrack.PlayOneShot();

            Debug.LogWarning($"Step Result: {heroRoll >= enemyRoll}. {heroRoll} agains {enemyRoll}.", gameObject);

            return heroRoll >= enemyRoll;
        }

        private int CalculateHeroRoll()
        {
            int rollValue = RollHelper.RollDices(_checkRollDice, _checkRollsCount, out int[] dices);
            int skillValue = _selectedCharacter.FindSkillValueByName(_selectedAction.Skill.Name);
            OnHeroRollEvent?.Invoke(dices);

            return rollValue + skillValue;
        }

        private int CalculateEnemyRoll()
        {
            int rollValue = RollHelper.RollDices(_checkRollDice, _checkRollsCount, out int[] dices);
            int skillValue = _data.FindSkillValueByName(_selectedAction.Skill.Name);
            OnEnemyRollEvent?.Invoke(dices);

            return rollValue + skillValue;
        }
    }
}
