using EG.Tower.Utils;

namespace EG.Tower.Rolls
{
    public static class RollHelper
    {
        public static int Roll(DiceType dice)
        {
            return KujRandom.Int(1, (int)dice + 1);
        }

        public static int RollDices(DiceType dice, int count, out int[] rolls)
        {
            rolls = new int[count];
            int rollValue = 0;
            for (int i = 0; i < count; i++)
            {
                var roll = Roll(dice);
                rollValue += roll;
                rolls[i] = roll;
            }

            return rollValue;
        }
    }
}
