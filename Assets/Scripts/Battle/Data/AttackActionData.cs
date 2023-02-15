﻿using EG.Tower.Game.Battle.Behaviours;
using UnityEngine;

namespace EG.Tower.Game.Battle.Data
{
    [CreateAssetMenu(fileName = "AttackActionData", menuName = "Data/Battle/AttackAction", order = 1)]
    public class AttackActionData : BattleActionData
    {
        public override void Execute(HeroBattleUnit owner, BattleUnit target, string attributeName)
        {
            Debug.Log("Executing Attack!");

            if (owner.TryFindAttribute(attributeName, out var attribute))
            {
                target.Hit(attribute.Value);
            }
            else
            {
                Debug.LogError($"Action {_name} failed! {attributeName} not found!");
            }
        }
    }
}