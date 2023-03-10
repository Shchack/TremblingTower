using EG.Tower.Utils;
using UnityEngine;

namespace EG.Tower.Rolls
{
    public static class RollHelper
    {
        public static int Roll(DiceType dice)
        {
            return KujRandom.Int(1, (int)dice + 1);
        }

        public static DicesRoll RollDices(DiceType dice, int count, Sprite[] diceIcons)
        {
            var diceItems = new DiceItem[count];
            int total = 0;
            for (int i = 0; i < count; i++)
            {
                var roll = Roll(dice);
                var icon = FindDiceSprite(roll, diceIcons);
                diceItems[i] = new DiceItem(dice, total, icon);

                total += roll;
            }

            return new DicesRoll(total, diceItems);
        }

        private static Sprite FindDiceSprite(int diceValue, Sprite[] diceIcons)
        {
            var result = diceIcons[0];

            if (diceValue - 1 >= 0 && diceValue - 1 <= 6)
            {
                result = diceIcons[diceValue - 1];
            }
            else
            {
                Debug.LogError($"Wrong dice roll: {diceValue}!");
            }

            return result;
        }
    }
}
