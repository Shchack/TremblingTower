using EG.Tower.Game.Battle.Data;
using UnityEngine;

namespace EG.Tower.Game
{
    [CreateAssetMenu(fileName = "BattleAttributeData", menuName = "Data/Hero/AttributeData", order = 1)]
    public class BattleAttributeData : ScriptableObject
    {
        public string Name;
        public Sprite Icon;
        public BattleActionData BattleAction;
    }
}
