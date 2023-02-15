using EG.Tower.Battle.Data;
using UnityEngine;

namespace EG.Tower.Game.Battle.Behaviours
{
    public class EnemyBattleUnit : BattleUnit
    {
        [SerializeField] private EnemyData _data;

        public int Attack { get; private set; }
        public int Defence { get; private set; }
        public Sprite Icon { get; private set; }

        public override bool IsPlayer => false;

        protected override void Awake()
        {
            base.Awake();
            Name = _data.Name;
            HP = _data.MaxHP;
            MaxHP = _data.MaxHP;
            Attack = _data.Attack;
            Defence = _data.Defence;
            Icon = _data.Icon;
            CombatOrder = GetCombatOrder(Attack);
        }
    }
}
