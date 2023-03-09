using EG.Tower.Game;
using EG.Tower.Heroes.Skills;
using EG.Tower.Rolls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EG.Tower.Missions
{
    public class MissionStep
    {
        public event Action<StepCompletionInfo> OnCompletedEvent;
        public event Action<bool, float> OnSuccessChanceChangedEvent;

        public string Name { get; private set; }
        public SkillCheckData[] PossibleSkillChecks { get; private set; }

        private Dictionary<string, int> _missionSkillsByName;
        private HeroModel _selectedCharacter;
        private SkillCheckData _selectedAction;

        private bool CanExecute => _selectedCharacter != null && _selectedAction != null;

        public MissionStep(MissionStepData data, MissionSkillData[] missionSkills)
        {
            Name = data.Name;
            PossibleSkillChecks = data.PossibleSkillChecks;
            _missionSkillsByName = missionSkills.ToDictionary(s => s.Skill.Name, s => s.Value);
        }

        public bool TryExecute()
        {
            bool executed = false;

            if (CanExecute)
            {
                DicesRoll heroRoll = CalculatHeroRoll();
                DicesRoll enemyRoll = CalculateEnemyRoll();
                GameHub.One.Audio.CheckResultTrack.PlayOneShot();

                var completionInfo = new StepCompletionInfo(heroRoll, enemyRoll);
                OnCompletedEvent?.Invoke(completionInfo);
                executed = true;
            }

            return executed;
        }

        public void SelectCharacter(HeroModel heroModel)
        {
            _selectedCharacter = heroModel;
            RecalcuateChance();
        }

        public void SelectAction(SkillCheckData selectedAction)
        {
            _selectedAction = selectedAction;
            RecalcuateChance();
        }

        private void RecalcuateChance()
        {
            bool calculated = false;
            float chance = 0f;

            if (CanExecute)
            {
                var enemySkillValue = FindMissonSkillValueByName(_selectedAction.Skill.Name);
                int heroSkillValue = _selectedCharacter.FindSkillValueByName(_selectedAction.Skill.Name);

                chance = 0.5f + ((heroSkillValue - enemySkillValue) / (float)GameHub.One.MaxRollValue);

                if (chance < 0f)
                {
                    chance = 0f;
                }
                if (chance > 1f)
                {
                    chance = 1f;
                }

                calculated = true;
            }

            OnSuccessChanceChangedEvent?.Invoke(calculated, chance);
        }

        private DicesRoll CalculateEnemyRoll()
        {
            DicesRoll roll = GameHub.One.RollTwoDiceSix();
            int skillValue = FindMissonSkillValueByName(_selectedAction.Skill.Name);
            roll.SetBonusValue(skillValue);

            return roll;
        }

        private DicesRoll CalculatHeroRoll()
        {
            DicesRoll roll = GameHub.One.RollTwoDiceSix();
            int skillValue = _selectedCharacter.FindSkillValueByName(_selectedAction.Skill.Name);
            roll.SetBonusValue(skillValue);

            return roll;
        }

        private int FindMissonSkillValueByName(string name)
        {
            int result = 0;

            if (_missionSkillsByName.TryGetValue(name, out int value))
            {
                result = value;
            }

            return result;
        }
    }
}