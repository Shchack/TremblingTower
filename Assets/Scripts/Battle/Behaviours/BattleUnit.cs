using EG.Tower.Game.Battle.Models;
using EG.Tower.Game.Rolls;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EG.Tower.Game.Battle.Behaviours
{
    public class BattleUnit : MonoBehaviour
    {
        public event Action<int> OnHPChangedEvent;

        public bool IsPlayer { get; private set; }
        public string HeroName { get; private set; }
        public BattleAttributeItemModel[] Attributes { get; private set; }
        public int MaxHP { get; private set; }
        public HeroInspirationModel Inspiration { get; private set; }
        public List<BattleActionModel> Actions { get; private set; }
        public int CombatOrder { get; private set; }

        private int _hp;
        public int HP
        {
            get
            {
                return _hp;
            }
            private set
            {
                _hp = value;
                OnHPChangedEvent?.Invoke(_hp);
            }
        }


        public void Init(HeroModel heroModel, DiceType combatOrderDice)
        {
            IsPlayer = true;
            HeroName = heroModel.Name;
            Attributes = heroModel.GetBattleAttributes();
            HP = heroModel.HP;
            MaxHP = heroModel.MaxHP;
            Inspiration = heroModel.Inspiration;
            CombatOrder = GetCombatOrder(heroModel, combatOrderDice);
            Actions = CreateActions();
        }

        public void Init(DiceType combatOrderDice)
        {
            IsPlayer = false;
            HeroName = "Enemy";
            HP = 15;
            MaxHP = 15;
            CombatOrder = RollHelper.Roll(combatOrderDice);
        }

        public List<BattleActionModel> CreateActions()
        {
            List<BattleActionModel> actions = new List<BattleActionModel>();
            var actionAttributes = Attributes.Where(i => i.AttributeType == HeroAttributeType.Points);

            foreach (var attribute in actionAttributes)
            {
                actions.Add(new BattleActionModel(attribute));
            }

            actions.Add(new BattleActionModel(Inspiration));

            return actions;
        }

        private int GetCombatOrder(HeroModel heroModel, DiceType combatOrderDice)
        {
            int attackValue = 0;
            if (heroModel.TryFindVirtueTrait(VirtueType.Courage, out var trait))
            {
                attackValue = trait.GetAttribute().Value;
            }

            var rollResult = RollHelper.Roll(combatOrderDice);

            return rollResult + attackValue;
        }
    }
}
