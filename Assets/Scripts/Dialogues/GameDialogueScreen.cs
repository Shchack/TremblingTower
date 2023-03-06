using EG.Tower.Game;
using UnityEngine;
using UnityEngine.UI;

namespace EG.Tower.Dialogues
{
    public class GameDialogueScreen : MonoBehaviour
    {
        [SerializeField] private Button _skipButton;
        [SerializeField] private CanvasGroup _maskGroup;
        [SerializeField] private Image _dialogueImage;
        [SerializeField] private CheckResultUI _checkResultUI;

        private GameDialogueController _controller;

        private void Awake()
        {
            _controller = FindObjectOfType<GameDialogueController>();
            _skipButton.onClick.AddListener(HandleSkipButtonClick);
            _dialogueImage.enabled = false;
            _maskGroup.alpha = 1f;
        }

        private void Start()
        {
            GameHub.One.DialogueSystem.OnShowImageEvent += SetDialogueImage;
        }

        public void ShowCheckResult(int[] rolls, bool check)
        {
            _checkResultUI.Show(rolls, check);
        }

        private void HandleSkipButtonClick()
        {
            _controller.SkipDialogue();
        }

        private void SetDialogueImage(Sprite sprite)
        {
            _dialogueImage.sprite = sprite;

            if (!_dialogueImage.enabled)
            {
                _dialogueImage.enabled = true;
                _maskGroup.alpha = 0f;
            }
        }

        private void OnDestroy()
        {
            if (GameHub.One.DialogueSystem != null)
            {
                GameHub.One.DialogueSystem.OnShowImageEvent -= SetDialogueImage;
            }
        }
    }
}
