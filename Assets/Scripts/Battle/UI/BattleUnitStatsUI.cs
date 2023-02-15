using EG.Tower.Game.Battle.Behaviours;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EG.Tower.Game.Battle.UI
{
    public class BattleUnitStatsUI : MonoBehaviour
    {
        [SerializeField] private Image _portrait;
        [SerializeField] private TMP_Text _hpLabel;
        [SerializeField] private TMP_Text _combatOrderLabel;

        private BattleUnit _battleUnit;
        private int _maxHp;

        public void Init(BattleUnit battleUnit)
        {
            _battleUnit = battleUnit;
            _maxHp = battleUnit.MaxHP;
            SetHpLabel(battleUnit.HP, battleUnit.MaxHP);
            SetCombatOrderLabel(battleUnit.CombatOrder);
            battleUnit.OnHPChangedEvent += HandleHPChangedEvent;
        }

        public void Init(EnemyBattleUnit battleUnit)
        {
            _portrait.sprite = battleUnit.Icon;
            _battleUnit = battleUnit;
            _maxHp = battleUnit.MaxHP;
            SetHpLabel(battleUnit.HP, battleUnit.MaxHP);
            SetCombatOrderLabel(battleUnit.CombatOrder);
            battleUnit.OnHPChangedEvent += HandleHPChangedEvent;
        }

        private void HandleHPChangedEvent(int currentHp)
        {
            SetHpLabel(currentHp, _maxHp);
        }

        private void SetHpLabel(int currentHp, int maxHp)
        {
            _hpLabel.text = $"{currentHp}/{maxHp}";
        }

        private void SetCombatOrderLabel(int combatOrder)
        {
            _combatOrderLabel.text = combatOrder.ToString();
        }

        private void OnDestroy()
        {
            if (_battleUnit != null)
            {
                _battleUnit.OnHPChangedEvent -= HandleHPChangedEvent;
            }
        }
    }
}
