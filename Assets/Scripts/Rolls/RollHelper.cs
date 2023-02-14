using EG.Tower.Utils;

namespace EG.Tower.Game.Rolls
{
    public static class RollHelper
    {
        public static int Roll(DiceType dice)
        {
            return KujRandom.Int(1, (int)dice + 1);
        }
    }
}
