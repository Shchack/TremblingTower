using EG.Tower.Game.Battle.Behaviours;
using EG.Tower.Game.Battle.Models;

namespace EG.Tower.Game.Battle
{
    public class BattleAction
    {
        private BattleActionModel _actionModel;

        public BattleAction(BattleActionModel actionModel)
        {
            _actionModel = actionModel;
        }

        public void Execute(BattleUnit target)
        {
            _actionModel.Execute(target);
        }
    }
}