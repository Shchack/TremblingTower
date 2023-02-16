using EG.Tower.Game.Battle.Behaviours;
using UnityEngine;

namespace EG.Tower.Game.Battle.Data
{
    [CreateAssetMenu(fileName = "InstantKillActionData", menuName = "Data/Battle/InstantKillAction", order = 4)]
    public class InstantKillActionData : BattleActionData
    {
        public override void Execute(HeroBattleUnit owner, BattleUnit target, string attributeName)
        {
            Debug.Log($"Executing {Name}!");

            if (owner.TryFindAttribute(attributeName, out var attribute))
            {
                target.Kill();
                attribute.Spend(1);
            }
            else
            {
                Debug.LogError($"Action {Name} failed! {attributeName} not found!");
            }
        }
    }
}
