using EG.Tower.Game.Battle.Behaviours;
using EG.Tower.Game.Battle.Models;
using UnityEngine;

namespace EG.Tower.Game.Battle.Data
{
    [CreateAssetMenu(fileName = "InstantKillActionData", menuName = "Data/Battle/InstantKillAction", order = 4)]
    public class InstantKillActionData : BattleActionData
    {
        public override void Execute(BattleUnit owner, BattleUnit target, BattleActionModel model)
        {
            Debug.Log($"Executing {Name}!");
            owner.Ult();
            target.Kill();
            model.Spend(1);
        }
    }
}
