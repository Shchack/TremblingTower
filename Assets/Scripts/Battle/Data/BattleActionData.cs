using EG.Tower.Game.Battle.Behaviours;
using EG.Tower.Game.Battle.Models;
using UnityEngine;

namespace EG.Tower.Game.Battle.Data
{
    public abstract class BattleActionData : ScriptableObject, IBattleAction
    {
        [SerializeField] protected string _name;
        [SerializeField] protected bool _isPlayerTarget;
        [SerializeField] protected Sprite _icon;

        public string Name => _name;
        public bool IsPlayerTarget => _isPlayerTarget;
        public Sprite Icon => _icon;

        public abstract void Execute(BattleUnit owner, BattleUnit target, BattleActionModel model);
    }
}
