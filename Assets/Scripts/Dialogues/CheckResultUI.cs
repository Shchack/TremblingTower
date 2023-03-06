using EG.Tower.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EG.Tower.Dialogues
{
    public class CheckResultUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _panel;
        [SerializeField] private TMP_Text _resultText;
        [SerializeField] private Color _successColor;
        [SerializeField] private Color _failColor;
        [SerializeField] private string _successText = "Check Success";
        [SerializeField] private string _failText = "Check Failure";
        [SerializeField] private Image[] _dices;
        [SerializeField] private Sprite[] _rollImages;

        [Header("Tween")]
        [SerializeField] private LeanTweenType _type = LeanTweenType.linear;
        [SerializeField] private float _inSeconds = 1f;
        [SerializeField] private float _outSeconds = 1f;
        [SerializeField] private float _staySeconds = 2f;

        [Header("Debug")]
        [SerializeField] private bool _checkSuccess = true;
        [SerializeField] private int[] _rolls = { 1, 1 };

        private void Start()
        {
            _panel.alpha = 0f;
        }

        public void Show(int[] rolls, bool check)
        {
            SetRolls(rolls);
            SetResult(check);

            _panel.LeanAlpha(1f, _inSeconds).setEase(_type).setOnComplete(() =>
                _panel.LeanAlpha(0f, _outSeconds).setEase(_type).setDelay(_staySeconds)
            );
        }

        private void SetRolls(int[] rolls)
        {
            if (_dices.Length != rolls.Length)
            {
                Debug.LogError($"No dice visual for rolls!");
                return;
            }

            for (int i = 0; i < _dices.Length; i++)
            {
                if (rolls[i] - 1 >= 0 && rolls[i] - 1 <= 6)
                {
                    _dices[i].sprite = _rollImages[rolls[i] - 1];
                }
                else
                {
                    Debug.LogError($"Wrong dice roll: {rolls[i]}!");
                }
            }
        }

        private void SetResult(bool check)
        {
            var color = check ? _successColor : _failColor;
            var text = check ? _successText : _failText;

            _resultText.text = text;
            _resultText.color = color;

            var duration = _inSeconds + _outSeconds + _staySeconds;
            GameHub.One.ScreenEffects.ShowFlashEffect(color, duration);
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Check Result"))
            {
                Show(_rolls, _checkSuccess);
            }
        }
    }
}
