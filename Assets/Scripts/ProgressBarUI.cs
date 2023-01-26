using TMPro;
using UnityEngine;

namespace EG.Tower.Game
{
    public class ProgressBarUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _totalBar;
        [SerializeField] private RectTransform _currentBar;
        [SerializeField] private TMP_Text _percentLabel;
        [SerializeField] private float _widthPerUnit = 10f;
        [SerializeField] private Gradient _barGradient;

        private float _total = 0f;

        public void Init(float totalValue, float currentValue)
        {
            SetTotal(totalValue);
            SetCurrent(currentValue);
        }

        public void SetCurrent(float currentValue)
        {
            float percent = (currentValue / _total) * 100;
            _currentBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _widthPerUnit * currentValue);
            _percentLabel.text = $"{percent:n0}%";

            //var color = hp != 0 ? _barGradient.Evaluate(i/hp) : _barGradient.Evaluate(1);
            //healthUnit.SetColor(color);
        }

        public void SetTotal(float newTotalvalue)
        {
            _total = newTotalvalue;
            _widthPerUnit = _totalBar.rect.width / _total;
            _totalBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _widthPerUnit * _total);
        }
    }
}