using EG.Tower.Game.Battle.Behaviours;
using UnityEngine;

namespace EG.Tower.Game.Battle.Data
{
    [CreateAssetMenu(fileName = "DefendActionData", menuName = "Data/Battle/DefendAction", order = 2)]
    public class DefendActionData : BattleActionData
    {
        public override void Execute(HeroBattleUnit owner, BattleUnit target, string attributeName)
        {
            Debug.Log($"Executing {Name}!");

            if (owner.TryFindAttribute(attributeName, out var attribute))
            {
                target.AddDefence(attribute.Value);
            }
            else
            {
                Debug.LogError($"Action {Name} failed! {attributeName} not found!");
            }
        }
    }
}
