namespace EG.Tower.Game
{
    public class HeroAttributeModel
    {
        public string Name { get; private set; }
        public HeroAttributeType AttributeType { get; private set; }
        public int Value { get; private set; }

        public HeroAttributeModel(string name, HeroAttributeType convertType, int value)
        {
            Name = name;
            AttributeType = convertType;
            Value = value;
        }
    }
}