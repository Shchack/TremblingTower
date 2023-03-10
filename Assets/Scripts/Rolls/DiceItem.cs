using UnityEngine;

namespace EG.Tower.Rolls
{
    public class DiceItem
    {
        public DiceType DiceType { get; private set; }
        public int Value { get; private set; }
        public Sprite Icon { get; private set; }

        public DiceItem(DiceType diceType, int value, Sprite icon)
        {
            DiceType = diceType;
            Value = value;
            Icon = icon;
        }
    }
}
