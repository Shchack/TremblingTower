using EG.Tower.Dialogues.Data;
using EG.Tower.Game;
using EG.Tower.Rolls;
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

        private void Start()
        {
            _panel.alpha = 0f;
        }

        public void Show(DicesRoll roll, bool check)
        {
            ShowRoll(roll);
            SetResult(check);

            LeanTween.cancel(_panel.gameObject);
            _panel.alpha = 0f;

            _panel.LeanAlpha(1f, _config.TweenInSeconds).setEase(_config.TweenType).setOnComplete(() =>
                _panel.LeanAlpha(0f, _config.TweenOutSeconds).setEase(_config.TweenType).setDelay(_config.TweenStaySeconds)
            );
        }

        private void ShowRoll(DicesRoll roll)
        {
            if (_dices.Length != roll.Dices.Length)
            {
                Debug.LogError($"No dice visual for rolls!");
                return;
            }

            for (int i = 0; i < _dices.Length; i++)
            {
                _dices[i].sprite = roll.Dices[i].Icon;
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
                var roll = GameHub.One.RollTwoDiceSix();
                Show(roll, _checkSuccess);
            }
        }
    }
}
