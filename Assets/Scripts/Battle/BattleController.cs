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
        public event Action OnBattleBeginEvent;
        public event Action<BattleUnit> OnTurnBeginEvent;

        [SerializeField] private HeroBattleUnit _hero;
        [field: SerializeField] public EnemyBattleUnit[] Enemies { get; private set; }

        public HeroBattleUnit Hero => _hero;
        private Dictionary<string, BattleUnit> _unitsOrder;
        private int _turnUnitIndex;

        private void Awake()
        {
            GameHub.One.Register(this);
        }

        private void OnDestroy()
        {
            GameHub.One.Unregister(this);
        }

        private void Start()
        {
            InitUnits();
        }

        private void InitUnits()
        {
            _hero.OnUnitSelectedEvent += HandleUnitSelectedEvent;
            _hero.OnDeathEvent += HandleHeroDeathEvent;

            foreach (var enemy in Enemies)
            {
                enemy.SetTarget(_hero);
                enemy.OnUnitSelectedEvent += HandleUnitSelectedEvent;
                enemy.OnTurnEndEvent += EndTurn;
                enemy.OnDeathEvent += HandleEnemyDeathEvent;
            }

            CreateUnitsOrder();
        }

        private void CreateUnitsOrder()
        {
            var units = new List<BattleUnit>(Enemies);
            units.Add(_hero);
            _unitsOrder = units.OrderByDescending(u => u.CombatOrder).ToDictionary(u => u.Name, u => u);
            _turnUnitIndex = 0;
        }

        public void BeginBattle()
        {
            OnBattleBeginEvent?.Invoke();
            BeginNextTurn();
        }

        private void BeginNextTurn()
        {
            var unit = _unitsOrder.ElementAt(_turnUnitIndex).Value;
            unit.BeginTurn();
            OnTurnBeginEvent?.Invoke(unit);
        }

        public void EndTurn()
        {
            Debug.Log($"End Turn!");
            if (_turnUnitIndex + 1 < _unitsOrder.Count)
            {
                _turnUnitIndex++;
            }
            else
            {
                _turnUnitIndex = 0;
            }

            BeginNextTurn();
        }

        private void HandleEnemyDeathEvent(string name)
        {
            if (!_unitsOrder.Remove(name))
            {
                Debug.LogError("Failed to remove enemy", this);
            }

            if (_unitsOrder.Count <= 1)
            {
                Debug.LogWarning("Win", this);
            }
        }

        private void HandleHeroDeathEvent(string name)
        {
            Debug.LogWarning("Game Over!", this);
        }

        private BattleActionModel _currentAction;

        public void TrackAction(BattleActionModel actionModel)
        {
            _currentAction = actionModel;
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
            if (_currentAction == null)
            {
                Debug.LogError("No selected action to execute", this);
                return;
            }

            if (_currentAction.CanExecute(target))
            {
                _currentAction.Execute(target);
                _currentAction = null;
            }
            else
            {
                Debug.LogError("Failed to execute battle action", this);
            }
        }
    }
}
