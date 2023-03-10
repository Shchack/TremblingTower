using EG.Tower.Heroes.Skills;
using UnityEngine;

namespace EG.Tower.Missions
{
    [CreateAssetMenu(fileName = "MissionData", menuName = "Data/Missions/Mission", order = 0)]
    public class MissionData : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public RegionType Region { get; private set; }
        [field: SerializeField] public FactionType Faction { get; private set; }
        [field: SerializeField] public SkillValueData[] MissionSkills { get; private set; }
        [field: SerializeField] public MissionStepData[] Steps { get; private set; }
    }
}