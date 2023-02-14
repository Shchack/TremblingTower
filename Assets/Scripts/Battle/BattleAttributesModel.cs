using System.Linq;

namespace EG.Tower.Game.Battle
{
    public class BattleAttributesModel
    {
        public string HeroName { get; internal set; }
        public BattleAttributeItemModel[] Items { get; private set; }
        public int UltUseTimes { get; private set; }
        public int CombatOrder { get; private set; }

        public BattleAttributesModel(HeroModel heroModel, int combatOrder)
        {
            HeroName = heroModel.Name;
            Items = heroModel.Traits.Select(GetModel).ToArray();
            UltUseTimes = heroModel.Inspiration;
            CombatOrder = combatOrder;
        }

        public BattleAttributeItemModel[] GetActions()
        {
            return Items.Where(i => i.AttributeType == HeroAttributeType.Points).ToArray();
        }

        private BattleAttributeItemModel GetModel(Trait trait)
        {
            var attribute = trait.GetAttribute();
            return new BattleAttributeItemModel(trait.Virtue, trait.Value, attribute);
        }
    }
}
