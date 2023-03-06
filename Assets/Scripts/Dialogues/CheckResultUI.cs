using EG.Tower.Dialogues.Data;
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
        [SerializeField] private Image[] _dices;
        [SerializeField] private CheckResultConfigData _config;

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

            _panel.LeanAlpha(1f, _config.TweenInSeconds).setEase(_config.TweenType).setOnComplete(() =>
                _panel.LeanAlpha(0f, _config.TweenOutSeconds).setEase(_config.TweenType).setDelay(_config.TweenStaySeconds)
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
                    _dices[i].sprite = _config.RollImages[rolls[i] - 1];
                }
                else
                {
                    Debug.LogError($"Wrong dice roll: {rolls[i]}!");
                }
            }
        }

        private void SetResult(bool check)
        {
            var color = check ? _config.SuccessColor : _config.FailColor;
            var text = check ? _config.SuccessText : _config.FailText;

            _resultText.text = text;
            _resultText.color = color;

            GameHub.One.ScreenEffects.ShowFlashEffect(color, _config.Duration);
            GameHub.One.Audio.CheckResultTrack.PlayOneShot();
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
