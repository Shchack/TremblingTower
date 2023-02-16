using EG.Tower.Game.Battle.Behaviours;
using EG.Tower.Game.Battle.Models;
using UnityEngine;

namespace EG.Tower.Game.Battle.Data
{
    public interface IBattleAction
    {
        public string Name { get; }
        public bool IsPlayerTarget { get; }

        public void Execute(BattleUnit owner, BattleUnit target, BattleActionModel model);
    }
}
