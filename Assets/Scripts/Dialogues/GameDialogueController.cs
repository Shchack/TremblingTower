using EG.Tower.Game;
using EG.Tower.Rolls;
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
        [SerializeField] private RollDifficultiesData _difficultiesData;
        [SerializeField] private DiceType _checkRollDice = DiceType.D6;
        [SerializeField] private int _checkRollsCount = 2;

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

        public void CheckSkill(string skillName, string difficultyTypeName)
        {
            int[] rolls = new int[_checkRollsCount];
            int rollValue = 0;
            for (int i = 0; i < _checkRollsCount; i++)
            {
                var roll = RollHelper.Roll(_checkRollDice);
                rollValue += roll;
                rolls[i] = roll;
            }

            RollDifficulty difficulty = _difficultiesData.FindDifficulty(difficultyTypeName);
            int skillValue = _hero.GetSkillValue(skillName);
            var checkValue = skillValue + rollValue;

            bool check = checkValue >= difficulty.Value;

            DialogueLua.SetVariable("CheckResult", check);
            _dialogueScreen.ShowCheckResult(rolls, check);

            Debug.Log($"{skillName} check result: {check}. {checkValue} agains {difficultyTypeName}");
        }

        public void GiveTraitReward(string virtueName, double value)
        {
            Debug.Log($"Giving trait {virtueName} reward {value}");

            try
            {
                int intValue = System.Convert.ToInt32(value);
                _hero.AddSkillValue(virtueName, intValue);
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Change skill value failed: {ex.Message}");
            }
        }

        private void OnEnable()
        {
            Lua.RegisterFunction(nameof(CheckSkill), this, SymbolExtensions.GetMethodInfo(() => CheckSkill(string.Empty, string.Empty)));
            Lua.RegisterFunction(nameof(GiveTraitReward), this, SymbolExtensions.GetMethodInfo(() => GiveTraitReward(string.Empty, (double)0)));
        }

        private void OnDisable()
        {
            Lua.UnregisterFunction(nameof(CheckSkill));
            Lua.UnregisterFunction(nameof(GiveTraitReward));
        }
    }
}
