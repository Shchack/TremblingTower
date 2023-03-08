using EG.Tower.Heroes.Skills;
using System;
using UnityEngine;

namespace EG.Tower.Missions
{
    [Serializable]
    public class SkillCheckData
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public SkillData Skill { get; private set; }
    }
}
