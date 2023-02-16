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

        public BattleActionModel(BattleAttributeItemModel model, HeroBattleUnit owner)
            : this(model.AttributeName, model.AttributeValue, model.AttributeIcon, model.BattleAction, owner)
        {
        }

        public BattleActionModel(HeroInspirationModel model, HeroBattleUnit owner)
            : this(model.CombatActionName, model.Value, model.Icon, model.BattleAction, owner)
        {
        }

        public BattleActionModel(string name, int value, Sprite icon, IBattleAction action, HeroBattleUnit owner)
        {
            Name = name;
            Value = value;
            Icon = icon;
            Action = action;
            Owner = owner;
        }

        public void Spend(int decrease)
        {
            Value -= decrease;
        }

        public void Execute(BattleUnit target)
        {
            Owner.Perform(Action, target, Name);
            OnActionExecuteEvent?.Invoke();
        }

        public bool CanExecute(BattleUnit target)
        {
            bool isSuitableTarget = Action.IsPlayerTarget == target.IsPlayer;

            return Owner.TurnEnergy > 0 && isSuitableTarget;
        }
    }
}
