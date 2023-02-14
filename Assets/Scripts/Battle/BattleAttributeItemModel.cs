namespace EG.Tower.Game.Battle
{
    public class BattleAttributeItemModel
    {
        public HeroAttributeType AttributeType { get; private set; }
        public string VirtueName { get; private set; }
        public int VirtueValue { get; private set; }
        public string AttributeName { get; private set; }
        public int AttributeValue { get; private set; }

        public BattleAttributeItemModel(string virtueName, int virtueValue, HeroAttributeType attributeType, string attributeName, int attributeValue)
        {
            VirtueName = virtueName;
            VirtueValue = virtueValue;
            AttributeType = attributeType;
            AttributeName = attributeName;
            AttributeValue = attributeValue;
        }
    }
}
