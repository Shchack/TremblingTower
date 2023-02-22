using EG.Tower.Utils;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace EG.Tower.Game
{
    public class GameDialogueController : MonoBehaviour
    {
        [SerializeField] private Hero _hero;
        [SerializeField] private Transform _narrator;

        private void Awake()
        {
            DialogueManager.instance.conversationEnded += HandleConversationEnd;
        }

        private void Start()
        {
            StartConversation(GameHub.One.DialogueSystem.NextConversation);
        }

        public void StartConversation(string conversationTitle)
        {
            DialogueManager.StartConversation(conversationTitle, _hero.transform, _narrator);
        }

        private void HandleConversationEnd(Transform t)
        {
            SkipDialogue();
        }

        public void SkipDialogue()
        {
            DialogueManager.StopConversation();
            SceneHelper.LoadMapScene();
        }
    }
}
