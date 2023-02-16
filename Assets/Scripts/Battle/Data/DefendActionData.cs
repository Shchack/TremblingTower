using EG.Tower.Game.Battle.Behaviours;
using EG.Tower.Game.Battle.Models;
using UnityEngine;

namespace EG.Tower.Game.Battle.Data
{
    [CreateAssetMenu(fileName = "DefendActionData", menuName = "Data/Battle/DefendAction", order = 2)]
    public class DefendActionData : BattleActionData
    {
        public override void Execute(BattleUnit owner, BattleUnit target, BattleActionModel model)
        {
            Debug.Log($"Executing {Name}!");
            target.AddDefence(model.Value);
        }
    }
}
