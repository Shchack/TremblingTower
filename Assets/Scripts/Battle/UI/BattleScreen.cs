using EG.Tower.Game.Battle.Behaviours;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EG.Tower.Game.Battle.UI
{
    public class BattleScreen : MonoBehaviour
    {
        [Header("Main")]
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Button _endTurnButton;

        [Header("Hero")]
        [SerializeField] private BattleUnitStatsUI _heroStatsUI;
        [SerializeField] private TurnEnergyUI _turnEnergyUI;
        [SerializeField] private RectTransform _actionsHolder;
        [SerializeField] private BattleActionItemUI _battleActionUiPrefab;

        [Header("Enemies")]
        [SerializeField] private RectTransform _enemiesStatsHolder;
        [SerializeField] private BattleUnitStatsUI _statsUIPrefab;

        private BattleController _battleController;
        private List<BattleActionItemUI> _items;

        private void Start()
        {
            _canvas.enabled = false;
            _battleController = GameHub.One.BattleController;
            InitHero(_battleController.Hero);
            InitEnemies(_battleController.Enemies);
            _endTurnButton.onClick.AddListener(HandleEndTurnButtonClick);
            _battleController.OnBattleBeginEvent += HandleBattleBeginEvent;
            _battleController.OnTurnBeginEvent += HandleTurnBeginEvent;
        }

        private void HandleBattleBeginEvent()
        {
            _canvas.enabled = true;
        }

        private void HandleTurnBeginEvent(BattleUnit unit)
        {
            SetTurnUI(unit);
        }

        private void HandleEndTurnButtonClick()
        {
            _battleController.EndTurn();
        }

        private void InitHero(HeroBattleUnit heroUnit)
        {
            _heroStatsUI.Init(heroUnit);
            _turnEnergyUI.Init(heroUnit, _actionsHolder);
            InitActions(heroUnit);
        }

        private void InitEnemies(EnemyBattleUnit[] enemies)
        {
            var items = _enemiesStatsHolder.GetComponentsInChildren<BattleUnitStatsUI>();

            for (int i = 0; i < items.Length; i++)
            {
                Destroy(items[i].gameObject);
            }

            foreach (var enemy in enemies)
            {
                var itemUI = Instantiate(_statsUIPrefab, _enemiesStatsHolder);
                itemUI.Init(enemy, enemy.Icon);
            }
        }

        private void InitActions(HeroBattleUnit battleUnit)
        {
            Cleanup();

            _items = new List<BattleActionItemUI>();
            var actions = battleUnit.Actions;
            foreach (var action in actions)
            {
                var itemUI = Instantiate(_battleActionUiPrefab, _actionsHolder);
                itemUI.Init(action.Value);
                itemUI.OnSelectedEvent += HandleItemSelectedEvent;
                _items.Add(itemUI);
            }
        }

        private void HandleItemSelectedEvent(BattleActionItemUI selectedItem)
        {
            foreach (var item in _items)
            {
                item.Deselect();
            }

            selectedItem.Select();
        }

        private void SetTurnUI(BattleUnit turnUnit)
        {
            var label = _endTurnButton.GetComponentInChildren<TMP_Text>();
            if (turnUnit.IsPlayer)
            {
                _actionsHolder.gameObject.SetActive(true);
                label.text = "End Turn";
                _endTurnButton.interactable = true;
            }
            else
            {
                _actionsHolder.gameObject.SetActive(false);
                label.text = "Enemy Turn";
                _endTurnButton.interactable = false;
            }
        }

        private void Cleanup()
        {
            var items = _actionsHolder.GetComponentsInChildren<BattleActionItemUI>();

            for (int i = 0; i < items.Length; i++)
            {
                Destroy(items[i].gameObject);
            }
        }

        private void OnDestroy()
        {
            if (_battleController != null)
            {
                _battleController.OnBattleBeginEvent -= HandleBattleBeginEvent;
                _battleController.OnTurnBeginEvent -= HandleTurnBeginEvent;
            }
        }
    }
}
