using EG.Tower.Heroes.Skills;
using System;
using System.Linq;
using UnityEngine;

namespace EG.Tower.Missions
{
    [CreateAssetMenu(fileName = "MissionData", menuName = "Data/Missions/Mission", order = 0)]
    public class MissionData : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public MissionRegionType Region { get; private set; }
        [field: SerializeField] public FactionType Faction { get; private set; }
        [field: SerializeField] public MissionSkillData[] MissionSkills { get; private set; }
        [field: SerializeField] public MissionStepData[] Steps { get; private set; }

        public int FindSkillValueByName(string skillName)
        {
            int result = 0;
            var skill = MissionSkills.FirstOrDefault(s => s.Skill.Name.Equals(skillName, StringComparison.OrdinalIgnoreCase));

            if (skill != null)
            {
                result = skill.Value;
            }

            return result;
        }
    }

    public enum MissionRegionType
    {
        None = 0,
        City = 1,
        Village = 2,
        Forest = 3,
        Marsh = 4
    }

    public enum FactionType
    {
        None = 0,
        BlackEmpire = 1,
        Spirits = 2,
        Smugglers = 3
    }
}