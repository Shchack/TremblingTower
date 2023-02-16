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
        public BattleUnit Owner { get; private set; }

        public BattleActionModel(BattleAttributeData attr, int value, BattleUnit owner)
        {
            Name = attr.Name;
            Icon = attr.Icon;
            Action = attr.BattleAction;
            Value = value;
            Owner = owner;
        }

        public BattleActionModel(HeroInspirationModel inspiration, BattleUnit owner)
        {
            Name = inspiration.CombatActionName;
            Icon = inspiration.Icon;
            Action = inspiration.BattleAction;
            Value = inspiration.Value;
            Owner = owner;
        }

        public void Spend(int decrease)
        {
            Value -= decrease;
        }

        public void Execute(BattleUnit target)
        {
            Action.Execute(Owner, target, this);
            OnActionExecuteEvent?.Invoke();
        }

        public bool CanExecute(BattleUnit target)
        {
            bool isSuitableTarget = Action.IsPlayerTarget == target.IsPlayer;

            return Owner.TurnEnergy > 0 && isSuitableTarget;
        }
    }
}
