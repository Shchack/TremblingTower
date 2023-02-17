using EG.Tower.Battle.Data;
using EG.Tower.Game.Battle.Models;
using EG.Tower.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EG.Tower.Game.Battle.Behaviours
{
    public class EnemyBattleUnit : BattleUnit
    {
        public event Action OnTurnEndEvent;

        [SerializeField] private EnemyData _data;

        public int DefendPoints { get; private set; }
        public Sprite Icon { get; private set; }

        public override bool IsPlayer => false;

        private BattleUnit _targetHero;
        private Dictionary<int, BattleActionModel> _actionPriority;
        private int _totalActionPoints;

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
            Defence = 0;
            CombatOrder = GetCombatOrder(AttackPoints);
            Actions = _data.Actions.Select(CreateActionModel).ToArray();

            _totalActionPoints = Actions.Sum(a => a.Value);
        }

        public override void BeginTurn()
        {
            base.BeginTurn();

            if (Actions.Length <= 0)
            {
                Debug.LogError($"Enemy {name} has no available battle actions!");
                return;
            }

            StartCoroutine(ExecuteActions());
        }

        public void SetTarget(BattleUnit target)
        {
            _targetHero = target;
        }

        private IEnumerator ExecuteActions()
        {
            while (TurnEnergy > 0)
            {
                yield return ExecuteActionByPriority();
                TurnEnergy--;
            }

            OnTurnEndEvent?.Invoke();
        }

        private IEnumerator ExecuteRandomAction()
        {
            yield return new WaitForSeconds(1.5f);

            var randomIndex = KujRandom.Index(Actions.Length);

            BattleActionModel model = Actions[randomIndex];

            var target = model.Action.IsPlayerTarget ? this : _targetHero;
            model.Action.Execute(this, target, model);

            yield return new WaitForSeconds(1.5f);
        }

        private IEnumerator ExecuteActionByPriority()
        {
            yield return new WaitForSeconds(1.5f);

            BattleActionModel model = null;
            while (model == null)
            {
                var randomChance = KujRandom.Chance();

                for (int i = 0; i < Actions.Length; i++)
                {
                    float actionPercent = (float)Actions[i].Value / (float)_totalActionPoints;

                    if (randomChance <= actionPercent)
                    {
                        model = Actions[i];
                        break;
                    }
                }
            }

            var target = model.Action.IsPlayerTarget ? this : _targetHero;
            model.Action.Execute(this, target, model);

            yield return new WaitForSeconds(1.5f);
        }

        private BattleActionModel CreateActionModel(EnemyActionModel actionModel)
        {
            return new BattleActionModel(actionModel.BattleAttribute, actionModel.Value, this);
        }
    }
}
