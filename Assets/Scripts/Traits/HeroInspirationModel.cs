using System;
using UnityEngine;

namespace EG.Tower.Game
{
    [Serializable]
    public class HeroInspirationModel
    {
        public string Name { get; private set; }
        public string CombatActionName { get; private set; }
        public int Value { get; private set; }
        public Sprite Icon { get; private set; }

        public HeroInspirationModel(HeroInspirationData data)
        {
            Name = data.Name;
            CombatActionName = data.CombatActionName;
            Value = data.Value;
            Icon = data.Icon;
        }
    }
}
