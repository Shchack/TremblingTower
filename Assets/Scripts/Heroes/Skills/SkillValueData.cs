using System;
using UnityEngine;

namespace EG.Tower.Heroes.Skills
{
    [Serializable]
    public class SkillValueData
    {
        [field: SerializeField] public SkillData Skill { get; private set; }
        [field: SerializeField] public int Value { get; private set; }
    }
}