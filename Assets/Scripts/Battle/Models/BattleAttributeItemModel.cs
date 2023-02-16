namespace EG.Tower.Game.Battle.Models
{
    public class BattleAttributeItemModel
    {
        public VirtueType VirtueType { get; private set; }
        public string VirtueName { get; private set; }
        public int VirtueValue { get; private set; }
        public int AttributeValue { get; private set; }
        public HeroAttributeType AttributeType { get; private set; }
        public BattleAttributeData AttributeData { get; private set; }

        public bool HasAction => AttributeData.BattleAction != null;

        public BattleAttributeItemModel(
            VirtueType virtueType,
            string virtueName,
            int virtueValue,
            int attributeValue,
            HeroAttributeType attributeType,
            BattleAttributeData attribute)
        {
            VirtueType = virtueType;
            VirtueName = virtueName;
            VirtueValue = virtueValue;
            AttributeValue = attributeValue;
            AttributeType = attributeType;
            AttributeData = attribute;
        }
    }
}
