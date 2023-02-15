using EG.Tower.Game.Battle.Behaviours;

namespace EG.Tower.Game.Battle.Data
{
    public interface IBattleAction
    {
        public string Name { get; }
        public bool IsEnemyTarget { get; }

        public void Execute(HeroBattleUnit owner, BattleUnit target, string attributeName);
    }
}
