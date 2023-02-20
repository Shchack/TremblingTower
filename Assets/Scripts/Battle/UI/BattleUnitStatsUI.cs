using EG.Tower.Game.Battle.Behaviours;
using EG.Tower.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EG.Tower.Game.Battle.UI
{
    public class BattleUnitStatsUI : MonoBehaviour
    {
        [SerializeField] private Image _portrait;
        [SerializeField] private RectTransform _statPanel;
        [SerializeField] private RectTransform _deathImage;
        [SerializeField] private TMP_Text _hpLabel;
        [SerializeField] private TMP_Text _combatOrderLabel;
        [SerializeField] private TMP_Text _attackLabel;
        [SerializeField] private TMP_Text _defenceLabel;

        [SerializeField] private Sprite _unknownActionIcon;
        [SerializeField] private Image _nextActionsIcon;
        [SerializeField] private TMP_Text _nextActionsValue;

        private BattleUnit _battleUnit;
        private int _maxHp;

        public void Init(BattleUnit battleUnit, Sprite icon = null)
        {
            _battleUnit = battleUnit;
            _maxHp = battleUnit.MaxHP;
            SetHpLabel(battleUnit.HP, battleUnit.MaxHP);
            SetCombatOrderLabel(battleUnit.CombatOrder);
            battleUnit.OnHPChangedEvent += HandleHPChangedEvent;

            SetDefenceLabel(battleUnit.Defence);
            SetAttackLabel(battleUnit.AttackPoints);
            battleUnit.OnDefenceChangedEvent += SetDefenceLabel;
            battleUnit.OnAttackChangedEvent += SetAttackLabel;
            battleUnit.OnDeathEvent += HandleDeathEvent;

            if (icon != null)
            {
                _portrait.sprite = icon;
            }

            _statPanel.gameObject.SetActive(true);
            _deathImage.gameObject.SetActive(false);
        }

        private void HandleDeathEvent(string name)
        {
            _statPanel.gameObject.SetActive(false);
            _deathImage.gameObject.SetActive(true);
        }

        private void HandleHPChangedEvent(int currentHp)
        {
            SetHpLabel(currentHp, _maxHp);
        }

        private void SetHpLabel(int currentHp, int maxHp)
        {
            _hpLabel.text = $"{currentHp}/{maxHp}";
        }

        private void SetAttackLabel(int value)
        {
            _attackLabel.text = value.ToString();
        }

        private void SetDefenceLabel(int value)
        {
            _defenceLabel.text = value.ToString();
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

        public void ShowTrueVisionAction(int trueVisionValue)
        {
            if (_battleUnit == null) return;

            var actions = _battleUnit.GetNextActions();

            if (actions == null || actions.Length <= 0) return;

            var showActions = KujRandom.Chance() <= (trueVisionValue / 100f);
            var resultString = string.Empty;

            if (showActions)
            {
                foreach (var item in actions)
                {
                    _nextActionsIcon.sprite = item.Icon;
                    resultString = $"{item.Value}";
                }
            }
            else
            {
                _nextActionsIcon.sprite = _unknownActionIcon;
            }

            _nextActionsValue.text = resultString;
        }
    }
}
