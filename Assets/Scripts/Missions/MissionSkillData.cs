using EG.Tower.Heroes.Skills;
using System;
using UnityEngine;

namespace EG.Tower.Missions
{
    [Serializable]
    public class MissionSkillData
    {
        [field: SerializeField] public SkillData Skill { get; private set; }
        [field: SerializeField] public int Value { get; private set; }
    }
}