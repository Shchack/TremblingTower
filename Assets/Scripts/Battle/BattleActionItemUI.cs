using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EG.Tower.Game.Battle
{
    public class BattleActionItemUI : MonoBehaviour
    {
        [SerializeField] private Button _actionButton;
        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _valueLabel;
        [SerializeField] private Image _highligher;

        private void Start()
        {
            _actionButton.onClick.AddListener(HandleActionSelectButtonClick);
            _highligher.enabled = false;
        }

        private void HandleActionSelectButtonClick()
        {
            Debug.LogError($"{name} action button clicked!");
            ToggleHighlight();
        }

        private void ToggleHighlight()
        {
            _highligher.enabled = !_highligher.enabled;
        }

        public void Init(Sprite icon, int value)
        {
            _iconImage.sprite = icon;
            _valueLabel.text = value.ToString();
        }
    }
}
