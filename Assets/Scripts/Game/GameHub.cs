using EG.Tower.Audio;
using EG.Tower.Game.Battle;
using EG.Tower.Utils;
using UnityEngine;

namespace EG.Tower.Game
{
    public class GameHub : Singleton<GameHub>
    {
        [SerializeField] private HeroSetupData _defaultTraits;
        [SerializeField] private GameDialogueSystem _dialogueSystem;
        [SerializeField] private AudioSystem _audio;
        [SerializeField] private GameScreenEffectController _screenEffects;

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

        public BattleController BattleController { get; private set; }

        public void Register(BattleController battleController)
        {
            BattleController = battleController;
        }

        public void Unregister(BattleController battleController)
        {
            BattleController = null;
        }
    }
}
