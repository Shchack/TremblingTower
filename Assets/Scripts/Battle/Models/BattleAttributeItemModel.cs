using EG.Tower.Game.Battle.Data;
using UnityEngine;

namespace EG.Tower.Game.Battle.Models
{
    public class BattleAttributeItemModel
    {
        public string VirtueName { get; private set; }
        public int VirtueValue { get; private set; }
        public string AttributeName { get; private set; }
        public int AttributeValue { get; private set; }
        public HeroAttributeType AttributeType { get; private set; }
        public Sprite AttributeIcon { get; private set; }
        public IBattleAction BattleAction { get; private set; }

        public BattleAttributeItemModel(string virtueName, int virtueValue, HeroAttributeModel attribute)
        {
            VirtueName = virtueName;
            VirtueValue = virtueValue;
            AttributeName = attribute.Name;
            AttributeValue = attribute.Value;
            AttributeType = attribute.AttributeType;
            AttributeIcon = attribute.Icon;
            BattleAction = attribute.BattleAction;
        }
    }
}
