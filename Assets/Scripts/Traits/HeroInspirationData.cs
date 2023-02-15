using EG.Tower.Game.Battle.Data;
using UnityEngine;

namespace EG.Tower.Game
{
    [CreateAssetMenu(fileName = "HeroInspirationData", menuName = "Data/Hero/HeroInspirationData", order = 2)]
    public class HeroInspirationData : ScriptableObject
    {
        public string Name = "Inspiration";
        public string CombatActionName = "Special";
        public int Value = 2;
        public Sprite Icon;
        public BattleActionData BattleAction;
    }
}
