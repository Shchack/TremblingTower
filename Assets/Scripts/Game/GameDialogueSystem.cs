using PixelCrushers.DialogueSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EG.Tower.Game
{
    public class GameDialogueSystem : MonoBehaviour
    {
        public event Action<Sprite> OnShowImageEvent;

        [ConversationPopup(false, true)]
        [SerializeField] private string _firstConversation;

        [SerializeField] private Sprite[] _sprites;

        private Dictionary<string, Sprite> _spriteLookup;

        private string _nextConversation = null;
        public string NextConversation
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_nextConversation))
                {
                    _nextConversation = _firstConversation;
                }

                return _nextConversation;
            }
        }

        private void Awake()
        {
            _spriteLookup = _sprites.ToDictionary(s => s.name, s => s);
        }

        public void ShowImage(string imageName)
        {
            if (_spriteLookup.TryGetValue(imageName, out var sprite))
            {
                OnShowImageEvent?.Invoke(sprite);
            }
            else
            {
                Debug.LogError($"Dialogue image {imageName} not found!");
            }
        }

        public void SetNextConversation(string title)
        {
            _nextConversation = title;
        }
    }
}
