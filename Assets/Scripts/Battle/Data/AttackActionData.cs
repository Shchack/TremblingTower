using EG.Tower.Game.Battle.Behaviours;
using EG.Tower.Game.Battle.Models;
using UnityEngine;

namespace EG.Tower.Game.Battle.Data
{
    [CreateAssetMenu(fileName = "AttackActionData", menuName = "Data/Battle/AttackAction", order = 1)]
    public class AttackActionData : BattleActionData
    {
        public override void Execute(BattleUnit owner, BattleUnit target, BattleActionModel model)
        {
            Debug.Log($"Executing {Name}!");
            owner.Attack(target, model.Value);
        }
    }
}
