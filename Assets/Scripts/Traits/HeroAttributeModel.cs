using EG.Tower.Game.Battle.Data;
using UnityEngine;

namespace EG.Tower.Game
{
    public class HeroAttributeModel
    {
        public string Name { get; private set; }
        public HeroAttributeType AttributeType { get; private set; }
        public int Value { get; private set; }
        public Sprite Icon { get; private set; }
        public IBattleAction BattleAction { get; private set; }

        public HeroAttributeModel(string name, HeroAttributeType type, int value, Sprite icon, IBattleAction action)
        {
            Name = name;
            AttributeType = type;
            Value = value;
            Icon = icon;
            BattleAction = action;
        }
    }
}