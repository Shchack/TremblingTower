using EG.Tower.Game.Battle.Models;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EG.Tower.Game.Battle.UI
{
    public class BattleActionItemUI : MonoBehaviour
    {
        public event Action<BattleActionItemUI> OnSelectedEvent;

        [SerializeField] private Button _actionButton;
        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _valueLabel;
        [SerializeField] private Image _highligher;

        private BattleActionModel _action;
        private bool _isSelectingTarget = false;

        private void Start()
        {
            _actionButton.onClick.AddListener(HandleActionSelectButtonClick);
            _highligher.enabled = false;
        }

        public void Init(BattleActionModel action)
        {
            _action = action;
            action.OnActionExecuteEvent += Deselect;
            _iconImage.sprite = action.Icon;
            _valueLabel.text = action.Value.ToString();
        }

        private void HandleActionSelectButtonClick()
        {
            _isSelectingTarget = !_isSelectingTarget;
            if (_isSelectingTarget)
            {
                OnSelectedEvent?.Invoke(this);
                GameHub.One.BattleController.TrackAction(_action);
            }
            else
            {
                Deselect();
                GameHub.One.BattleController.UntrackAction(_action);
            }
        }

        public void Select()
        {
            _highligher.enabled = true;
            _isSelectingTarget = true;
        }

        public void Deselect()
        {
            _highligher.enabled = false;
            _isSelectingTarget = false;
        }
    }
}
