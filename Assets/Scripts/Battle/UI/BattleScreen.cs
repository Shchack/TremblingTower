using EG.Tower.Game.Battle.Behaviours;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EG.Tower.Game.Battle.UI
{
    public class BattleScreen : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private BattleUnitStatsUI _heroStatsUI;
        [SerializeField] private RectTransform _actionsHolder;
        [SerializeField] private BattleActionItemUI _battleActionUiPrefab;
        [SerializeField] private Button _endTurnButton;

        private BattleController _battleController;

        private void Start()
        {
            _canvas.enabled = false;
            _battleController = FindObjectOfType<BattleController>();
            Init(_battleController.Hero);
            _endTurnButton.onClick.AddListener(HandleEndTurnButtonClick);
            _battleController.OnBattleBeginEvent += HandleBattleBeginEvent;
        }

        private void HandleBattleBeginEvent(BattleUnit turnUnit)
        {
            SetTurnUI(turnUnit);
            _canvas.enabled = true;
        }

        private void HandleEndTurnButtonClick()
        {
            var turnUnit = _battleController.EndTurn();
            SetTurnUI(turnUnit);
        }

        private void Init(BattleUnit heroUnit)
        {
            _heroStatsUI.Init(heroUnit);
            InitActions(heroUnit);
        }

        private void InitActions(BattleUnit battleUnit)
        {
            Cleanup();

            var actions = battleUnit.Actions;
            foreach (var action in actions)
            {
                var itemUI = Instantiate(_battleActionUiPrefab, _actionsHolder);
                itemUI.Init(action);
            }
        }

        private void SetTurnUI(BattleUnit turnUnit)
        {
            var label = _endTurnButton.GetComponentInChildren<TMP_Text>();
            if (turnUnit.IsPlayer)
            {
                _actionsHolder.gameObject.SetActive(true);
                label.text = "End Turn";
            }
            else
            {
                _actionsHolder.gameObject.SetActive(false);
                label.text = "Enemy Turn";
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
            _battleController.OnBattleBeginEvent -= HandleBattleBeginEvent;
        }
    }
}
