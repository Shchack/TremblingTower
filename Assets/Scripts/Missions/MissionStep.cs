using System;
using UnityEngine;

namespace EG.Tower.Missions
{
    [Serializable]
    public class MissionStep
    {
        [field: SerializeField]
        public string Name { get; private set; }

        [field: SerializeField]
        public SkillCheckData[] PossibleSkillChecks { get; private set; }
    }
}