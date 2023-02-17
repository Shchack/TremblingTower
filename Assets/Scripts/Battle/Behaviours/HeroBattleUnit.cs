using EG.Tower.Game.Battle.Models;
using System.Collections.Generic;
using System.Linq;

namespace EG.Tower.Game.Battle.Behaviours
{
    public class HeroBattleUnit : BattleUnit
    {
        public override bool IsPlayer => true;

        public BattleAttributeItemModel[] Attributes { get; private set; }
        public HeroInspirationModel Inspiration { get; private set; }

        private Dictionary<VirtueType, BattleAttributeItemModel> _attributes;

        protected override void Awake()
        {
            base.Awake();
            HeroModel heroModel = GameHub.One.Session.HeroModel;
            Name = heroModel.Name;
            HP = heroModel.HP;
            MaxHP = heroModel.MaxHP;
            Inspiration = heroModel.Inspiration;
            MaxTurnEnergy = heroModel.TurnEnergy;
            TurnEnergy = heroModel.TurnEnergy;
            Attributes = heroModel.BattleAttributes;
            _attributes = Attributes.ToDictionary(a => a.VirtueType, a => a);
            Defence = 0;
            Actions = CreateActions();

            var attackPoints = GetAttackPoints();
            AttackPoints = attackPoints;
            CombatOrder = GetCombatOrder(attackPoints);
        }

        public BattleActionModel[] CreateActions()
        {
            var actions = new List<BattleActionModel>();
            var actionAttributes = Attributes.Where(i => i.HasAction);

            foreach (var attribute in actionAttributes)
            {
                var action = new BattleActionModel(attribute.AttributeData, attribute.AttributeValue, this);
                action.OnActionExecuteEvent += HandleActionExecuteEvent;
                actions.Add(action);
            }

            var inspirationAction = new BattleActionModel(Inspiration, this);
            inspirationAction.OnActionExecuteEvent += HandleActionExecuteEvent;
            actions.Add(inspirationAction);

            return actions.ToArray();
        }

        private void HandleActionExecuteEvent()
        {
            TurnEnergy--;
        }

        private int GetAttackPoints()
        {
            int attackValue = 0;
            if (_attributes.TryGetValue(VirtueType.Courage, out var attribute))
            {
                attackValue = attribute.AttributeValue;
            }

            return attackValue;
        }
    }
}
