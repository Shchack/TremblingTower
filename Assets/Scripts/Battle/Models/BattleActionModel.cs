using UnityEngine;

namespace EG.Tower.Game.Battle.Models
{
    public class BattleActionModel
    {
        public string Name { get; private set; }
        public int Value { get; private set; }
        public Sprite Icon { get; private set; }

        public BattleActionModel(BattleAttributeItemModel item)
        {
            Name = item.AttributeName;
            Value = item.AttributeValue;
            Icon = item.AttributeIcon;
        }

        public BattleActionModel(HeroInspirationModel inspiration)
        {
            Name = inspiration.CombatActionName;
            Value = inspiration.Value;
            Icon = inspiration.Icon;
        }

        public void TryExecute()
        {
            Debug.Log($"Trying to execute action {Name}");
        }
    }
}
