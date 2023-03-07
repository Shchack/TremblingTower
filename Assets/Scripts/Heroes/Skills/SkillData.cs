using System;
using UnityEngine;

namespace EG.Tower.Heroes.Skills
{
    [Serializable]
    public class SkillData
    {
        [field: SerializeField]
        public string Name { get; private set; }

        [field: SerializeField]
        public string AltName { get; private set; }

        [field: SerializeField]
        public int Value { get; private set; }
    }
}
