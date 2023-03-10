using EG.Tower.Heroes.Skills;
using EG.Tower.Missions;
using System;
using UnityEngine;

namespace EG.Tower.Heroes.Traits
{
    [CreateAssetMenu(fileName = "TraitData", menuName = "Data/Hero/Trait", order = 2)]
    public class TraitData : ScriptableObject
    {
        [field: SerializeField]
        public string Name { get; private set; }

        [field: SerializeField]
        public string Description { get; private set; }

        [field: SerializeField]
        public int BonusValue { get; private set; }

        [field: SerializeField]
        public RegionType[] Regions { get; private set; }

        [field: SerializeField]
        public FactionType[] Factions { get; private set; }

        [field: SerializeField]
        public SkillData Skill { get; private set; }

        public bool TryGetBonusValue(string skillName, RegionType region, FactionType faction, out int bonus)
        {
            bool skillMatch = Skill != null && Skill.Name.Equals(skillName, StringComparison.OrdinalIgnoreCase);
            bool regionMatch = false;
            if (Regions != null && Regions.Length > 0)
            {
                foreach (var regionItem in Regions)
                {
                    if (regionItem == region)
                    {
                        regionMatch = true;
                        break;
                    }
                }
            }

            bool factionMatch = false;
            if (Factions != null && Factions.Length > 0)
            {
                foreach (var factionItem in Factions)
                {
                    if (factionItem == faction)
                    {
                        factionMatch = true;
                        break;
                    }
                }
            }

            bool found = skillMatch || regionMatch || factionMatch;
            bonus = found ? BonusValue : 0;

            return found;
        }
    }
}
