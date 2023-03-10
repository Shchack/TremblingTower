using EG.Tower.Rolls;

namespace EG.Tower.Missions
{
    public class StepCompletionInfo
    {
        public DicesRoll HeroRoll { get; private set; }
        public DicesRoll EnemyRoll { get; private set; }

        public bool Success => HeroRoll.TotalValue >= EnemyRoll.TotalValue;

        public StepCompletionInfo(DicesRoll heroRoll, DicesRoll enemyRoll)
        {
            HeroRoll = heroRoll;
            EnemyRoll = enemyRoll;
        }
    }
}
