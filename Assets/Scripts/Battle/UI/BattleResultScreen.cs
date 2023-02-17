using EG.Tower.Utils;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace EG.Tower.Game.Battle.UI
{
    public class BattleResultScreen : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private RectTransform _winPanel;
        [SerializeField] private Button _winButton;
        [SerializeField] private RectTransform _losePanel;
        [SerializeField] private Button _loseButton;

        private BattleController _battleController;

        private void Start()
        {
            _canvas.enabled = false;
            _winButton.onClick.AddListener(ContinueGame);
            _loseButton.onClick.AddListener(GoToMainMenu);

            _battleController = GameHub.One.BattleController;
            _battleController.OnBattleWinEvent += HandleBattleWinEvent;
            _battleController.OnBattleLostEvent += HandleBattleLostEvent;
        }

        private void ContinueGame()
        {
            SceneHelper.LoadGameplayScene();
        }

        private void GoToMainMenu()
        {
            SceneHelper.LoadCreateHeroScene();
        }

        private void HandleBattleWinEvent()
        {
            _canvas.enabled = true;
            _winPanel.gameObject.SetActive(true);
            _losePanel.gameObject.SetActive(false);
        }

        private void HandleBattleLostEvent()
        {
            _canvas.enabled = true;
            _losePanel.gameObject.SetActive(true);
            _winPanel.gameObject.SetActive(false);
        }
    }
}
