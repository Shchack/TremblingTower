using System;
using UnityEngine;
using UnityEngine.UI;

namespace EG.Tower.Game
{
    public class PauseScreen : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _exitButton;

        private void Awake()
        {
            _canvas.enabled = false;
            _continueButton.onClick.AddListener(ContinueGame);
            _exitButton.onClick.AddListener(ExitGame);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ToggleCanvasView();
            }
        }

        private void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        private void ContinueGame()
        {
            _canvas.enabled = false;

            if (!_canvas.enabled)
            {
                Time.timeScale = 1f;
            }
        }

        private void ToggleCanvasView()
        {
            _canvas.enabled = !_canvas.enabled;

            Time.timeScale = !_canvas.enabled ? 0f : 1f;
        }
    }
}
