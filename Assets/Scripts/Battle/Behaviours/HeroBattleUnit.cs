using EG.Tower.Game.Battle.Data;
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
        public Dictionary<string, BattleActionModel> Actions { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            HeroModel heroModel = GameHub.One.Session.HeroModel;
            Name = heroModel.Name;
            Attributes = heroModel.GetBattleAttributes();
            HP = heroModel.HP;
            MaxHP = heroModel.MaxHP;
            Inspiration = heroModel.Inspiration;
            MaxTurnEnergy = heroModel.TurnEnergy;
            TurnEnergy = heroModel.TurnEnergy;
            Actions = CreateActions();

            var orderBonus = GetCombatOrderBonus(heroModel);
            CombatOrder = GetCombatOrder(orderBonus);
        }

        public Dictionary<string, BattleActionModel> CreateActions()
        {
            Dictionary<string, BattleActionModel> actions = new Dictionary<string, BattleActionModel>();
            var actionAttributes = Attributes.Where(i => i.AttributeType == HeroAttributeType.Points);

            foreach (var attribute in actionAttributes)
            {
                var action = new BattleActionModel(attribute, this);
                actions.Add(action.Name, action);
            }

            var inspirationAction = new BattleActionModel(Inspiration);
            actions.Add(inspirationAction.Name, inspirationAction);

            return actions;
        }

        public void Perform(IBattleAction action, BattleUnit target, string name)
        {
            TurnEnergy--;
            action.Execute(this, target, name);
        }

        public bool TryFindAttribute(string name, out BattleActionModel action)
        {
            bool success = Actions.TryGetValue(name, out action);
            return success;
        }

        private int GetCombatOrderBonus(HeroModel heroModel)
        {
            int attackValue = 0;
            if (heroModel.TryFindVirtueTrait(VirtueType.Courage, out var trait))
            {
                attackValue = trait.GetAttribute().Value;
            }

            return attackValue;
        }
    }
}
