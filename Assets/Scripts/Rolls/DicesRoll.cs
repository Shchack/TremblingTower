namespace EG.Tower.Rolls
{
    public class DicesRoll
    {
        public int RollValue { get; private set; }
        public int BonusValue { get; private set; }
        public DiceItem[] Dices { get; private set; }

        public int TotalValue => RollValue + BonusValue;

        public DicesRoll(int value, DiceItem[] dices)
        {
            RollValue = value;
            BonusValue = 0;
            Dices = dices;
        }

        public void SetBonusValue(int bonus)
        {
            BonusValue = bonus;
        }
    }
}
