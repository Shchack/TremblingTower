﻿using EG.Tower.Heroes.Skills;
using System.Linq;
using UnityEngine;

namespace EG.Tower.Heroes
{
    [CreateAssetMenu(fileName = "HeroSetupData", menuName = "Data/Hero/HeroSetup", order = 0)]
    public class HeroSetupData : ScriptableObject
    {
        [SerializeField] private string _heroName;
        [SerializeField] private SkillData[] _skills;
        [SerializeField] private int _strengthSkillBoost = 1;
        [SerializeField] private int _weaknessSkillBoost = -1;

        [SerializeField] private int _maxHP = 20;
        [SerializeField] private int _supplies = 5;
        [SerializeField] private int _money = 5;
        [SerializeField] private int _inspiration = 2;

        public string HeroName => _heroName;
        public int StrengthSkillBoost => _strengthSkillBoost;
        public int WeaknessSkillBoost => _weaknessSkillBoost;
        public int MaxHP => _maxHP;
        public int Supplies => _supplies;
        public int Money => _money;
        public int Inspiration => _inspiration;

        public Skill[] GetSkills()
        {
            return _skills.Select(s => new Skill(s)).ToArray();
        }
    }
}
