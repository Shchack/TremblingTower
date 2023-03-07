using EG.Tower.Heroes;
using EG.Tower.Heroes.Skills;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EG.Tower.Game
{
    [Serializable]
    public class HeroCreateModel
    {
        public string Name { get; private set; }
        public Skill[] Skills { get; private set; }
        public Skill StrengthSkill { get; private set; }
        public Skill WeaknessSkill { get; private set; }

        public int StrengthSkillBoost { get; private set; }
        public int WeaknessSkillBoost { get; private set; }

        public int MaxHP { get; private set; }
        public int Supplies { get; private set; }
        public int Money { get; private set; }
        public int Inspiration { get; private set; }

        public Dictionary<string, Skill> SkillsByName { get; private set; }

        public HeroCreateModel(HeroSetupData setupData)
        {
            Name = setupData.HeroName;
            Skills = setupData.GetSkills();
            StrengthSkillBoost = setupData.StrengthSkillBoost;
            WeaknessSkillBoost = setupData.WeaknessSkillBoost;
            MaxHP = setupData.MaxHP;
            Supplies = setupData.Supplies;
            Money = setupData.Money;
            Inspiration = setupData.Inspiration;

            StrengthSkill = null;
            WeaknessSkill = null;
            SkillsByName = Skills.ToDictionary(s => s.Name, s => s);
        }

        public void SetName(string newName)
        {
            Name = newName;
        }

        public void SetStrengthSkill(string name)
        {
            if (StrengthSkill != null && StrengthSkill.HasName(name))
            {
                return;
            }

            if (SkillsByName.TryGetValue(name, out Skill newSkill))
            {
                if (StrengthSkill != null)
                {
                    StrengthSkill.ResetValue();
                }

                newSkill.AddValue(StrengthSkillBoost);
                StrengthSkill = newSkill;
            }
        }

        public void SetWeaknessSkill(string name)
        {
            if (WeaknessSkill != null && WeaknessSkill.HasName(name))
            {
                return;
            }

            if (SkillsByName.TryGetValue(name, out Skill newSkill))
            {
                if (WeaknessSkill != null)
                {
                    WeaknessSkill.ResetValue();
                }

                newSkill.AddValue(WeaknessSkillBoost);
                WeaknessSkill = newSkill;
            }
        }

        public void ResetStrengthSkill()
        {
            if (StrengthSkill != null)
            {
                StrengthSkill.ResetValue();
                StrengthSkill = null;
            }
        }

        public void ResetWeaknessSkill()
        {
            if (WeaknessSkill != null)
            {
                WeaknessSkill.ResetValue();
                WeaknessSkill = null;
            }
        }
    }
}
