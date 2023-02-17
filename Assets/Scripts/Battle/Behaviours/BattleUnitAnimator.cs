using UnityEngine;

namespace EG.Tower.Game.Battle.Behaviours
{
    public class BattleUnitAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public void Play(BattleUnitAnimType type)
        {
            _animator.SetTrigger(type.ToString());
        }
    }

    public enum BattleUnitAnimType
    {
        None = 0,
        Death = 1,
        Attack = 2,
        Defend = 3,
        Heal = 4,
        Ult = 5
    }
}
