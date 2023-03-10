using EG.Tower.Audio;
using EG.Tower.Heroes;
using EG.Tower.Missions;
using EG.Tower.Rolls;
using EG.Tower.Utils;
using UnityEngine;

namespace EG.Tower.Game
{
    public class GameHub : Singleton<GameHub>
    {
        [SerializeField] private HeroSetupData _defaultHeroSetup;
        [SerializeField] private HeroSetupData[] _defaultTeamSetup;
        [SerializeField] private MissionData _defaultMissionData;
        [SerializeField] private GameDialogueSystem _dialogueSystem;
        [SerializeField] private AudioSystem _audio;
        [SerializeField] private GameScreenEffectController _screenEffects;

        [Header("Roll")]
        [SerializeField] private DiceType _checkRollDice = DiceType.D6;
        [SerializeField] private int _checkRollsCount = 2;
        [SerializeField] private Sprite[] _diceSixSprites;

        public int MaxRollValue => (int)_checkRollDice * _checkRollsCount;

        private PlayerSession _session;
        public PlayerSession Session
        {
            get
            {
                if (_session == null)
                {
                    _session = new PlayerSession(_defaultHeroSetup, _defaultTeamSetup);
                }

                return _session;
            }
        }

        public IAudioSystem Audio => _audio;

        public GameDialogueSystem DialogueSystem => _dialogueSystem;

        private MissionData _missionData;
        public MissionData NextMissionData
        {
            get
            {
                if (_missionData == null)
                {
                    _missionData = _defaultMissionData;
                }

                return _missionData;
            }
            set
            {
                _missionData = value;
            }
        }

        public GameScreenEffectController ScreenEffects => _screenEffects;

        public DicesRoll RollTwoDiceSix()
        {
            return RollHelper.RollDices(_checkRollDice, _checkRollsCount, _diceSixSprites);
        }
    }
}
