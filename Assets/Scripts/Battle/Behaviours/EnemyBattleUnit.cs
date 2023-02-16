using EG.Tower.Battle.Data;
using UnityEngine;

namespace EG.Tower.Game.Battle.Behaviours
{
    public class EnemyBattleUnit : BattleUnit
    {
        [SerializeField] private EnemyData _data;

        public int AttackPoints { get; private set; }
        public int DefendPoints { get; private set; }
        public Sprite Icon { get; private set; }

        public override bool IsPlayer => false;

        protected override void Awake()
        {
            base.Awake();
            Name = _data.Name;
            HP = _data.MaxHP;
            MaxHP = _data.MaxHP;
            AttackPoints = _data.Attack;
            DefendPoints = _data.Defence;
            Icon = _data.Icon;
            MaxTurnEnergy = _data.TurnEnergy;
            TurnEnergy = _data.TurnEnergy;
            CombatOrder = GetCombatOrder(AttackPoints);
        }
    }
}
