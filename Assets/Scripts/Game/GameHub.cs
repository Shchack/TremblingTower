using EG.Tower.Audio;
using EG.Tower.Heroes;
using EG.Tower.Utils;
using System;
using UnityEngine;

namespace EG.Tower.Game
{
    public class GameHub : Singleton<GameHub>
    {
        [SerializeField] private HeroSetupData _defaultTraits;
        [SerializeField] private GameDialogueSystem _dialogueSystem;
        [SerializeField] private AudioSystem _audio;
        [SerializeField] private GameScreenEffectController _screenEffects;
        [SerializeField] private Sprite[] _diceSixSprites;

        private PlayerSession _session;
        public PlayerSession Session
        {
            get
            {
                if (_session == null)
                {
                    _session = new PlayerSession(_defaultTraits);
                }

                return _session;
            }
        }

        public IAudioSystem Audio => _audio;

        public GameDialogueSystem DialogueSystem => _dialogueSystem;

        public GameScreenEffectController ScreenEffects => _screenEffects;

        public Sprite FindDiceSixSprite(int diceValue)
        {
            var result = _diceSixSprites[0];

            if (diceValue - 1 >= 0 && diceValue - 1 <= 6)
            {
                result = _diceSixSprites[diceValue - 1];
            }
            else
            {
                Debug.LogError($"Wrong dice roll: {diceValue}!");
            }

            return result;
        }
    }
}
