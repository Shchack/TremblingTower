using EG.Tower.Game;
using EG.Tower.Heroes.Skills;
using EG.Tower.Rolls;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EG.Tower.Missions
{
    public class MissionStep
    {
        public event Action<StepCompletionInfo> OnCompletedEvent;
        public event Action<bool, float> OnSuccessChanceChangedEvent;

        public string Name { get; private set; }
        public SkillCheckData[] PossibleSkillChecks { get; private set; }

        private Dictionary<string, int> _missionSkillsByName;
        private RegionType _missionRegion;
        private FactionType _missionFaction;
        private HeroModel _selectedCharacter;
        private SkillCheckData _selectedAction;

        private bool CanExecute => _selectedCharacter != null && _selectedAction != null;

        public MissionStep(MissionStepData data, MissionData missionData)
        {
            Name = data.Name;
            _missionRegion = missionData.Region;
            _missionFaction = missionData.Faction;
            PossibleSkillChecks = data.PossibleSkillChecks;
            _missionSkillsByName = missionData.MissionSkills.ToDictionary(s => s.Skill.Name, s => s.Value);
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

                int skillBonus = _selectedCharacter.FindSkillValueByName(_selectedAction.Skill.Name);
                int traitsBonus = _selectedCharacter.CalculateTraitBonusValue(_selectedAction.Skill.Name, _missionRegion, _missionFaction);
                int heroBonusValue = skillBonus + traitsBonus;

                chance = 0.5f + ((heroBonusValue - enemySkillValue) / (float)GameHub.One.MaxRollValue);

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
            int skillBonus = _selectedCharacter.FindSkillValueByName(_selectedAction.Skill.Name);
            int traitsBonus = _selectedCharacter.CalculateTraitBonusValue(_selectedAction.Skill.Name, _missionRegion, _missionFaction);
            int bonusValue = skillBonus + traitsBonus;
            roll.SetBonusValue(bonusValue);

            Debug.Log($"Roll: [{roll.RollValue}]. Skill: [{skillBonus}]. Traits: [{traitsBonus}]. Total: [{roll.TotalValue}]");

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