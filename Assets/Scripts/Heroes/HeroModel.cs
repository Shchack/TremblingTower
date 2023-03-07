using EG.Tower.Heroes;
using EG.Tower.Heroes.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EG.Tower.Game
{
    [Serializable]
    public class HeroModel
    {
        private const int SKILL_DEFAULT_VALUE = 0;

        [field: SerializeField]
        public string Name { get; private set; }

        [field: SerializeField]
        public Skill[] Skills { get; private set; }

        [field: SerializeField]
        public Skill StrengthSkill { get; private set; }

        [field: SerializeField]
        public Skill WeaknessSkill { get; private set; }

        [field: SerializeField]
        public int HP { get; private set; }

        [field: SerializeField]
        public int MaxHP { get; internal set; }

        [field: SerializeField]
        public int Supplies { get; private set; }

        [field: SerializeField]
        public int Money { get; private set; }

        [field: SerializeField]
        public int Inspiration { get; private set; }

        private Dictionary<string, Skill> _skillsByName;

        public HeroModel(HeroCreateModel createModel)
        {
            Name = createModel.Name;
            Skills = createModel.Skills;
            StrengthSkill = createModel.StrengthSkill;
            WeaknessSkill = createModel.WeaknessSkill;
            HP = createModel.MaxHP;
            MaxHP = createModel.MaxHP;
            Supplies = createModel.Supplies;
            Money = createModel.Money;
            Inspiration = createModel.Inspiration;

            _skillsByName = createModel.SkillsByName;
        }

        public HeroModel(string name, HeroSetupData setupData)
        {
            Name = name;
            Skills = setupData.GetSkills();
            StrengthSkill = null;
            WeaknessSkill = null;
            HP = setupData.MaxHP;
            MaxHP = setupData.MaxHP;
            Inspiration = setupData.Inspiration;
            Supplies = setupData.Supplies;
            Money = setupData.Money;

            _skillsByName = Skills.ToDictionary(s => s.Name, s => s);
        }

        public int FindSkillValue(string name)
        {
            int skillValue = SKILL_DEFAULT_VALUE;
            if (TryFindSkill(name, out Skill skill))
            {
                skillValue = skill.Value;
            }

            return skillValue;
        }

        public bool TryFindSkill(string name, out Skill skill)
        {
            bool success = _skillsByName.TryGetValue(name, out skill);

            return success;
        }
    }
}