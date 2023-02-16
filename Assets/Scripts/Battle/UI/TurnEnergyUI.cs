using EG.Tower.Game.Battle.Behaviours;
using TMPro;
using UnityEngine;

namespace EG.Tower.Game.Battle.UI
{
    public class TurnEnergyUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;

        private BattleUnit _unit;
        private RectTransform _actionsHolder;

        public void Init(BattleUnit unit, RectTransform actionsHolder)
        {
            _unit = unit;
            _actionsHolder = actionsHolder;
            SetLabel(unit.TurnEnergy, unit.MaxTurnEnergy);
            unit.OnTurnEnergyChangedEvent += HandleTurnEnergyChangedEvent;
        }

        private void HandleTurnEnergyChangedEvent(int currentEnergy)
        {
            SetLabel(currentEnergy, _unit.MaxTurnEnergy);

            if (currentEnergy > 0)
            {
                _actionsHolder.gameObject.SetActive(true);
            }
            else
            {
                _actionsHolder.gameObject.SetActive(false);
            }
        }

        private void SetLabel(int current, int max)
        {
            _label.text = $"{current}/{max}";
        }

        private void OnDestroy()
        {
            if (_unit != null)
            {
                _unit.OnTurnEnergyChangedEvent -= HandleTurnEnergyChangedEvent;
            }
        }
    }
}
