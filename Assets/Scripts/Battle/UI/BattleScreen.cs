using EG.Tower.Game.Battle.Models;
using UnityEngine;
using UnityEngine.UI;

namespace EG.Tower.Game.Battle.UI
{
    public class BattleScreen : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private BattleUnitStatsUI _heroStatsUI;
        [SerializeField] private RectTransform _abilitiesHolder;
        [SerializeField] private BattleActionItemUI _battleActionUiPrefab;
        [SerializeField] private Button _executeButton;

        private BattleController _battleController;

        private void Start()
        {
            _canvas.enabled = false;
            _battleController = FindObjectOfType<BattleController>();
            Init(_battleController.Attributes);
            _executeButton.onClick.AddListener(HandleExecuteButtonClick);
            _battleController.OnBattleBeginEvent += HandleBattleBeginEvent;
        }

        private void HandleBattleBeginEvent()
        {
            _canvas.enabled = true;
        }

        private void HandleExecuteButtonClick()
        {
            _battleController.ExecuteActions();
        }

        private void Init(BattleAttributesModel attributes)
        {
            _heroStatsUI.Init(attributes.HP, attributes.MaxHP);
            InitActions(attributes);
        }

        private void InitActions(BattleAttributesModel attributes)
        {
            Cleanup();

            var actions = attributes.GetActions();
            foreach (var item in actions)
            {
                var itemUI = Instantiate(_battleActionUiPrefab, _abilitiesHolder);
                itemUI.Init(item.AttributeName, item.AttributeIcon, item.AttributeValue);
            }

            var ultItemUI = Instantiate(_battleActionUiPrefab, _abilitiesHolder);
            ultItemUI.Init(attributes.Inspiration.CombatActionName, attributes.Inspiration.Icon, attributes.Inspiration.Value);
        }

        private void Cleanup()
        {
            var items = _abilitiesHolder.GetComponentsInChildren<BattleActionItemUI>();

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
