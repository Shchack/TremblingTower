﻿using EG.Tower.Game.Battle.Behaviours;
using UnityEngine;

namespace EG.Tower.Game.Battle.Data
{
    [CreateAssetMenu(fileName = "HealActionData", menuName = "Data/Battle/HealAction", order = 3)]
    public class HealActionData : BattleActionData
    {
        public override void Execute(HeroBattleUnit owner, BattleUnit target, string attributeName)
        {
            Debug.Log($"Executing {Name}!");

            if (owner.TryFindAttribute(attributeName, out var attribute))
            {
                target.Heal(attribute.Value);
            }
            else
            {
                Debug.LogError($"Action {Name} failed! {attributeName} not found!");
            }
        }
    }
}
