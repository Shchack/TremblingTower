using EG.Tower.Game.Battle.Models;
using EG.Tower.Game.Rolls;
using System;
using UnityEngine;

namespace EG.Tower.Game.Battle
{
    public class BattleController : MonoBehaviour
    {
        public event Action OnBattleBeginEvent;

        [SerializeField] private DiceType _combatOrderDice = DiceType.D10;

        public BattleAttributesModel Attributes { get; private set; }

        private void Awake()
        {
            var heroModel = GameHub.One.Session.HeroModel;
            var heroCombatOrder = GetCombatOrder(heroModel);
            Attributes = new BattleAttributesModel(heroModel, heroCombatOrder);
        }
        public void BeginBattle()
        {
            OnBattleBeginEvent?.Invoke();
        }

        public void ExecuteActions()
        {
            Debug.LogError($"Execution!");
        }

        private int GetCombatOrder(HeroModel heroModel)
        {
            int attackValue = 0;
            if (heroModel.TryFindVirtueTrait(VirtueType.Courage, out var trait))
            {
                attackValue = trait.GetAttribute().Value;
            }

            var rollResult = RollHelper.Roll(_combatOrderDice);

            return rollResult + attackValue;
        }
    }
}
