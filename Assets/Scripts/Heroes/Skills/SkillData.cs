using UnityEngine;

namespace EG.Tower.Heroes.Skills
{
    [CreateAssetMenu(fileName = "SkillData", menuName = "Data/Hero/Skill", order = 1)]
    public class SkillData : ScriptableObject
    {
        [field: SerializeField]
        public string Name { get; private set; }

        [field: SerializeField]
        public string AltName { get; private set; }

        [field: SerializeField]
        public int DefaultValue { get; private set; }

        [field: SerializeField]
        public Sprite Icon { get; private set; }
    }
}
