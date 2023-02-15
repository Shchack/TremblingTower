using EG.Tower.Game.Battle.Behaviours;
using EG.Tower.Game.Rolls;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EG.Tower.Game.Battle
{
    public class BattleController : MonoBehaviour
    {
        public event Action<BattleUnit> OnBattleBeginEvent;

        [SerializeField] private DiceType _combatOrderDice = DiceType.D10;

        [SerializeField] private BattleUnit _hero;
        [SerializeField] private BattleUnit[] _enemies;

        public BattleUnit Hero => _hero;

        private BattleUnit[] _unitsOrder;
        private int _turnUnitIndex;

        private void Awake()
        {
            HeroModel heroModel = GameHub.One.Session.HeroModel;
            _hero.Init(heroModel, _combatOrderDice);
            InitEnemies();
            CreateUnitsOrder();
        }

        private void InitEnemies()
        {
            foreach (var enemy in _enemies)
            {
                enemy.Init(_combatOrderDice);
            }
        }

        private void CreateUnitsOrder()
        {
            var units = new List<BattleUnit>(_enemies);
            units.Add(_hero);
            _unitsOrder = units.OrderByDescending(u => u.CombatOrder).ToArray();
            _turnUnitIndex = 0;
        }

        public void BeginBattle()
        {
            OnBattleBeginEvent?.Invoke(_unitsOrder[_turnUnitIndex]);
        }

        public BattleUnit EndTurn()
        {
            Debug.Log($"End Turn!");
            if (_turnUnitIndex + 1 < _unitsOrder.Length)
            {
                _turnUnitIndex++;
            }
            else
            {
                _turnUnitIndex = 0;
            }

            return _unitsOrder[_turnUnitIndex];
        }
    }
}
