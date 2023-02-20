using EG.Tower.Game.Battle.Models;
using EG.Tower.Game.Common;
using EG.Tower.Game.Rolls;
using EG.Tower.Utils;
using System;
using UnityEngine;

namespace EG.Tower.Game.Battle.Behaviours
{
    public abstract class BattleUnit : MonoBehaviour
    {
        public event Action<BattleUnit> OnUnitSelectedEvent;
        public event Action<int> OnHPChangedEvent;
        public event Action<int> OnDefenceChangedEvent;
        public event Action<int> OnAttackChangedEvent;
        public event Action<int> OnTurnEnergyChangedEvent;
        public event Action<string> OnDeathEvent;

        [SerializeField] protected BattleUnitAnimator _animator;
        [SerializeField] protected ObjectHighlighter _highlighter;
        [SerializeField] protected DiceType _combatOrderDice = DiceType.D10;

        public abstract bool IsPlayer { get; }

        public string Name { get; protected set; }
        public int MaxHP { get; protected set; }
        public int CombatOrder { get; protected set; }
        public int MaxTurnEnergy { get; protected set; }

        private int _hp;
        public int HP
        {
            get
            {
                return _hp;
            }
            protected set
            {
                _hp = value;
                OnHPChangedEvent?.Invoke(_hp);
            }
        }

        private int _attackPoints;
        public int AttackPoints
        {
            get
            {
                return _attackPoints;
            }
            protected set
            {
                _attackPoints = value;
                OnAttackChangedEvent?.Invoke(_attackPoints);
            }
        }

        private int _defence;
        public int Defence
        {
            get
            {
                return _defence;
            }
            protected set
            {
                _defence = value;
                OnDefenceChangedEvent?.Invoke(_defence);
            }
        }

        private int _turnEnergy;
        public int TurnEnergy
        {
            get
            {
                return _turnEnergy;
            }
            protected set
            {
                _turnEnergy = value;
                OnTurnEnergyChangedEvent?.Invoke(_turnEnergy);
            }
        }

        public int CritChancePercent { get; protected set; }

        public BattleActionModel[] Actions { get; protected set; }

        protected virtual void Awake()
        {
            _highlighter.OnObjectClickEvent += HandleObjectClickEvent;
            Name = name;
        }

        public virtual void BeginTurn()
        {
            TurnEnergy = MaxTurnEnergy;
        }

        public virtual void Hit(int value)
        {
            int currentHitValue = value;
            if (Defence > 0)
            {
                var defencePoints = Mathf.Clamp(currentHitValue, 0, Defence);
                currentHitValue -= defencePoints;
                Defence -= defencePoints;
            }

            HP -= currentHitValue;

            if (HP <= 0)
            {
                Kill();
            }
        }

        public void Kill()
        {
            HP = 0;

            if (_highlighter != null)
            {
                _highlighter.Disable();
                _highlighter.OnObjectClickEvent -= HandleObjectClickEvent;
            }

            OnDeathEvent?.Invoke(Name);
            _animator.Play(BattleUnitAnimType.Death);
        }

        public virtual void Attack(BattleUnit target, int value)
        {
            _animator.Play(BattleUnitAnimType.Attack);

            if (TryToCrit(value, out int critValue))
            {
                value = critValue;
            }

            target.Hit(value);
        }

        public virtual void AddDefence(int value)
        {
            if (TryToCrit(value, out int critValue))
            {
                value = critValue;
            }

            Defence += value;
            _animator.Play(BattleUnitAnimType.Defend);
        }

        public void Heal(int value)
        {
            if (TryToCrit(value, out int critValue))
            {
                value = critValue;
            }

            var newValue = HP + value;
            HP = Mathf.Clamp(newValue, 0, MaxHP);
            _animator.Play(BattleUnitAnimType.Heal);
        }

        public virtual void Ult(BattleUnit target, BattleActionModel actionModel)
        {
            _animator.Play(BattleUnitAnimType.Ult);
            target.Kill();
            actionModel.Spend(1);
        }

        protected bool TryToCrit(int value, out int critValue)
        {
            bool result = false;
            critValue = value;
            if (CritChancePercent <= 0)
            {
                return result;
            }

            var randomChance = KujRandom.Chance();
            if (randomChance <= (CritChancePercent / 100f))
            {
                critValue = value + value;
                result = true;
            }

            return result;
        }

        public virtual BattleActionModel[] GetNextActions()
        {
            return null;
        }

        protected virtual int GetCombatOrder(int orderBonus)
        {
            var rollResult = RollHelper.Roll(_combatOrderDice);
            return rollResult + orderBonus;
        }

        protected virtual void HandleObjectClickEvent()
        {
            OnUnitSelectedEvent?.Invoke(this);
        }

        protected virtual void OnDestroy()
        {
            if (_highlighter != null)
            {
                _highlighter.OnObjectClickEvent -= HandleObjectClickEvent;
            }
        }
    }
}
