using EG.Tower.Game.Battle.Behaviours;
using UnityEngine;

namespace EG.Tower.Game.Battle.Data
{
    public abstract class BattleActionData : ScriptableObject, IBattleAction
    {
        [SerializeField] protected string _name;
        [SerializeField] protected bool _isPlayerTarget;

        public string Name => _name;
        public bool IsPlayerTarget => _isPlayerTarget;

        public abstract void Execute(HeroBattleUnit owner, BattleUnit target, string attributeName);
    }
}
