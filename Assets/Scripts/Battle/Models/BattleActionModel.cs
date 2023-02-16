using EG.Tower.Game.Battle.Behaviours;
using EG.Tower.Game.Battle.Data;
using System;
using UnityEngine;

namespace EG.Tower.Game.Battle.Models
{
    public class BattleActionModel
    {
        public event Action OnActionExecuteEvent;

        public string Name { get; private set; }
        public int Value { get; private set; }
        public Sprite Icon { get; private set; }
        public IBattleAction Action { get; private set; }
        public HeroBattleUnit Owner { get; private set; }

        public BattleActionModel(BattleAttributeItemModel item, HeroBattleUnit owner)
        {
            Name = item.AttributeName;
            Value = item.AttributeValue;
            Icon = item.AttributeIcon;
            Action = item.BattleAction;
            Owner = owner;
        }

        public BattleActionModel(HeroInspirationModel inspiration)
        {
            Name = inspiration.CombatActionName;
            Value = inspiration.Value;
            Icon = inspiration.Icon;
            Action = inspiration.BattleAction;
        }

        public void Execute(BattleUnit target)
        {
            OnActionExecuteEvent?.Invoke();
            Owner.Perform(Action, target, Name);
        }

        public bool CanExecute(BattleUnit target)
        {
            bool isSuitableTarget = Action.IsPlayerTarget == target.IsPlayer;

            return Owner.TurnEnergy > 0 && isSuitableTarget;
        }
    }
}
