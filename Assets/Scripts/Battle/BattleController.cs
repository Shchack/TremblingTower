using EG.Tower.Game.Battle.Behaviours;
using EG.Tower.Game.Battle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EG.Tower.Game.Battle
{
    public class BattleController : MonoBehaviour, IDependencyInjectable
    {
        public event Action<BattleUnit> OnBattleBeginEvent;

        [SerializeField] private HeroBattleUnit _hero;
        [SerializeField] private EnemyBattleUnit[] _enemies;

        public HeroBattleUnit Hero => _hero;
        public EnemyBattleUnit[] Enemies => _enemies;

        private BattleUnit[] _unitsOrder;
        private int _turnUnitIndex;

        private void Awake()
        {
            GameHub.One.Register(this);
        }

        private void Start()
        {
            InitUnitEvents();
            CreateUnitsOrder();
        }

        private void InitUnitEvents()
        {
            _hero.OnUnitSelectedEvent += HandleUnitSelectedEvent;

            foreach (var enemy in _enemies)
            {
                enemy.OnUnitSelectedEvent += HandleUnitSelectedEvent;
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

        private void OnDestroy()
        {
            GameHub.One.Unregister(this);
        }

        private BattleAction _currentAction;

        public void TrackAction(BattleActionModel actionModel)
        {
            _currentAction = new BattleAction(actionModel);
        }

        public void UntrackAction(BattleActionModel actionModel)
        {
            _currentAction = null;
        }

        private void HandleUnitSelectedEvent(BattleUnit target)
        {
            ExecuteAction(target);
        }

        public void ExecuteAction(BattleUnit target)
        {
            if (_currentAction != null)
            {
                _currentAction.Execute(target);
            }

            _currentAction = null;
        }
    }
}
