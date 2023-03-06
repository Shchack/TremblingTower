using EG.Tower.Game;
using EG.Tower.Game.Rolls;
using EG.Tower.Utils;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace EG.Tower.Dialogues
{
    public class GameDialogueController : MonoBehaviour
    {
        [SerializeField] private Hero _hero;
        [SerializeField] private Transform _narrator;
        [SerializeField] private GameDialogueScreen _dialogueScreen;
        [SerializeField] private RollProbabilitiesData _probabilitiesData;
        [SerializeField] private DiceType _checkRollDice = DiceType.D6;
        [SerializeField] private int _checkRollsCount = 2;

        private const float TRAIT_MAX_VALUE = 100f;

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

        public string GetRollChance(string virtueName, double rollTypeValue)
        {
            double traitValue = _hero.GetVirtueValue(virtueName);

            var checkValue = traitValue + rollTypeValue;

            var result = _probabilitiesData.FindText(checkValue);

            return result;
        }

        public void CheckVirtue(string virtueName, double rollTypeValue)
        {
            var randomChance = Random.Range(0f, TRAIT_MAX_VALUE);
            double traitValue = _hero.GetVirtueValue(virtueName);

            int[] rolls = new int[_checkRollsCount];
            for (int i = 0; i < _checkRollsCount; i++)
            {
                rolls[i] = RollHelper.Roll(_checkRollDice);
            }

            var checkValue = traitValue + rollTypeValue;
            bool check = randomChance <= checkValue;
            DialogueLua.SetVariable("CheckResult", check);
            _dialogueScreen.ShowCheckResult(rolls, check);

            Debug.Log($"{virtueName} check result: {check}. {checkValue} agains {randomChance}");
        }

        public void GiveTraitReward(string virtueName, double value)
        {
            Debug.Log($"Giving trait {virtueName} reward {value}");
            _hero.AddVirtueValue(virtueName, value);
        }

        private void OnEnable()
        {
            Lua.RegisterFunction(nameof(GetRollChance), this, SymbolExtensions.GetMethodInfo(() => GetRollChance(string.Empty, (double)0)));
            Lua.RegisterFunction(nameof(CheckVirtue), this, SymbolExtensions.GetMethodInfo(() => CheckVirtue(string.Empty, (double)0)));
            Lua.RegisterFunction(nameof(GiveTraitReward), this, SymbolExtensions.GetMethodInfo(() => GiveTraitReward(string.Empty, (double)0)));
        }

        private void OnDisable()
        {
            Lua.UnregisterFunction(nameof(GetRollChance));
            Lua.UnregisterFunction(nameof(CheckVirtue));
            Lua.UnregisterFunction(nameof(GiveTraitReward));
        }
    }
}
