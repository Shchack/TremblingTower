using EG.Tower.Game;
using EG.Tower.Rolls;
using UnityEngine;
using UnityEngine.UI;

namespace EG.Tower.Dialogues
{
    public class GameDialogueScreen : MonoBehaviour
    {
        [SerializeField] private Button _skipButton;
        [SerializeField] private Image _dialogueImage;
        [SerializeField] private CheckResultUI _checkResultUI;

        private GameDialogueController _controller;

        private void Awake()
        {
            _controller = FindObjectOfType<GameDialogueController>();
            _skipButton.onClick.AddListener(HandleSkipButtonClick);
            _dialogueImage.enabled = false;
        }

        private void Start()
        {
            GameHub.One.DialogueSystem.OnShowImageEvent += SetDialogueImage;
        }

        public void ShowCheckResult(DicesRoll roll, bool check)
        {
            _checkResultUI.Show(roll, check);
        }

        private void HandleSkipButtonClick()
        {
            _controller.EndDialogue();
        }

        private void SetDialogueImage(Sprite sprite)
        {
            _dialogueImage.sprite = sprite;

            if (!_dialogueImage.enabled)
            {
                _dialogueImage.enabled = true;
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
