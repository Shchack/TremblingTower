using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EG.Tower.Game.Battle
{
    [RequireComponent(typeof(Button))]
    public class UltButtonUI : MonoBehaviour
    {
        public event Action OnUltButtonClick;

        [SerializeField] private TMP_Text _nameLabel;
        [SerializeField] private TMP_Text _useTimesLabel;

        private void Start()
        {
            if (TryGetComponent<Button>(out var button))
            {
                button.onClick.AddListener(HandleButtonClick);
            }
        }

        private void HandleButtonClick()
        {
            OnUltButtonClick?.Invoke();
        }

        public void SetUseTimes(int useTimes)
        {
            _useTimesLabel.text = useTimes.ToString();
        }
    }
}
