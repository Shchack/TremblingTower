﻿using EG.Tower.Game.Common;
using EG.Tower.Game.Rolls;
using System;
using UnityEngine;

namespace EG.Tower.Game.Battle.Behaviours
{
    public abstract class BattleUnit : MonoBehaviour
    {
        public event Action<BattleUnit> OnUnitSelectedEvent;
        public event Action<int> OnHPChangedEvent;
        public event Action<int> OnDefenceChangedEvent; 
        public event Action<int> OnTurnEnergyChangedEvent;
        public event Action<string> OnDeathEvent;

        [SerializeField] protected DiceType _combatOrderDice = DiceType.D10;
        [SerializeField] protected ObjectHighlighter _highlighter;

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
            OnDeathEvent?.Invoke(Name);
            gameObject.SetActive(false);
        }

        public virtual void AddDefence(int value)
        {
            Defence += value;
        }

        public void Heal(int value)
        {
            var newValue = HP + value;
            HP = Mathf.Clamp(newValue, 0, MaxHP);
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
