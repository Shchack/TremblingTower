using UnityEngine;
using UnityEngine.UI;

namespace EG.Tower.Game.Battle
{
    public class BattleScreen : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private RectTransform _abilitiesHolder;
        [SerializeField] private BattleActionItemUI _battleActionUiPrefab;
        [SerializeField] private UltButtonUI _ultButton;
        [SerializeField] private Button _executeButton;

        private BattleController _battleController;

        private void Start()
        {
            _canvas.enabled = false;
            _battleController = FindObjectOfType<BattleController>();
            InitActions(_battleController.GetAttributesModel());
            _executeButton.onClick.AddListener(HandleExecuteButtonClick);
            _ultButton.OnUltButtonClick += HandleUltButtonClick;
            _battleController.OnBattleBeginEvent += HandleBattleBeginEvent;
        }

        private void HandleBattleBeginEvent()
        {
            _canvas.enabled = true;
        }

        private void HandleExecuteButtonClick()
        {
            Debug.LogError($"{_executeButton.name} clicked!");
        }

        private void HandleUltButtonClick()
        {
            Debug.LogError($"{_ultButton.name} clicked!");
        }
        private void InitActions(BattleAttributesModel attributes)
        {
            Cleanup();

            _ultButton.SetUseTimes(attributes.UltUseTimes);
            var actions = attributes.GetActions();

            foreach (var item in actions)
            {
                var itemUI = Instantiate(_battleActionUiPrefab, _abilitiesHolder);
                itemUI.Init(item.AttributeIcon, item.AttributeValue);
            }
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
            _ultButton.OnUltButtonClick -= HandleUltButtonClick;
            _battleController.OnBattleBeginEvent -= HandleBattleBeginEvent;
        }
    }
}
