using EG.Tower.Game.Rolls;
using UnityEngine;

namespace EG.Tower.Game.Battle
{
    public class BattleController : MonoBehaviour
    {
        [SerializeField] private DiceType _combatOrderDice = DiceType.D10;

        private BattleAttributesModel _attributes;

        private void Awake()
        {
            var heroModel = GameHub.One.Session.HeroModel;
            var heroCombatOrder = GetCombatOrder(heroModel);
            _attributes = new BattleAttributesModel(heroModel, heroCombatOrder);
        }

        public BattleAttributesModel GetAttributesModel()
        {
            return _attributes;
        }

        private int GetCombatOrder(HeroModel heroModel)
        {
            int attackValue = 0;
            if (heroModel.TryFindVirtueTrait(VirtueType.Courage, out var trait))
            {
                attackValue = trait.GetAttribute().Value;
            }

            var rollResult = RollHelper.Roll(_combatOrderDice);

            return rollResult + attackValue;
        }
    }
}
