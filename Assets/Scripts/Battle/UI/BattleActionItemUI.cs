using EG.Tower.Game.Battle.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EG.Tower.Game.Battle.UI
{
    public class BattleActionItemUI : MonoBehaviour
    {
        [SerializeField] private Button _actionButton;
        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _valueLabel;
        [SerializeField] private Image _highligher;

        private BattleActionModel _action;

        private void Start()
        {
            _actionButton.onClick.AddListener(HandleActionSelectButtonClick);
            _highligher.enabled = false;
        }

        public void Init(BattleActionModel action)
        {
            _action = action;
            _iconImage.sprite = action.Icon;
            _valueLabel.text = action.Value.ToString();
        }

        private void HandleActionSelectButtonClick()
        {
            _action.TryExecute();
        }

        private void ToggleHighlight()
        {
            _highligher.enabled = !_highligher.enabled;
        }
    }
}
