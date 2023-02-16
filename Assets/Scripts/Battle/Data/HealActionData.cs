using EG.Tower.Game.Battle.Behaviours;
using EG.Tower.Game.Battle.Models;
using UnityEngine;

namespace EG.Tower.Game.Battle.Data
{
    [CreateAssetMenu(fileName = "HealActionData", menuName = "Data/Battle/HealAction", order = 3)]
    public class HealActionData : BattleActionData
    {
        public override void Execute(BattleUnit owner, BattleUnit target, BattleActionModel model)
        {
            Debug.Log($"Executing {Name}!");
            target.Heal(model.Value);
        }
    }
}
