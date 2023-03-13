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
            EndDialogue();
        }

        public void EndDialogue()
        {
            DialogueManager.StopConversation();
            SceneHelper.LoadMapScene();
        }

        public void CheckSkill(string skillName, string difficultyTypeName)
        {
            RollDifficulty difficulty = _difficultiesData.FindDifficulty(difficultyTypeName);
            int skillValue = _hero.GetSkillValue(skillName);
            DicesRoll roll = GameHub.One.RollTwoDiceSix();
            roll.SetBonusValue(skillValue);

            bool check = roll.TotalValue >= difficulty.Value;

            DialogueLua.SetVariable("CheckResult", check);
            _dialogueScreen.ShowCheckResult(roll, check);

            Debug.Log($"{skillName} check result: {check}. {roll.TotalValue} agains {difficulty.Value}");
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
