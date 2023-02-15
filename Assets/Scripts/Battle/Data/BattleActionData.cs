using EG.Tower.Game.Battle.Behaviours;
using UnityEngine;

namespace EG.Tower.Game.Battle.Data
{
    public abstract class BattleActionData : ScriptableObject, IBattleAction
    {
        [SerializeField] protected string _name;
        [SerializeField] protected bool _isEnemyTarget;

        public string Name => _name;
        public bool IsEnemyTarget => _isEnemyTarget;

        public abstract void Execute(HeroBattleUnit owner, BattleUnit target, string attributeName);
    }
}
