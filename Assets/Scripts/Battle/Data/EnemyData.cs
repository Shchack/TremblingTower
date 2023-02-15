using UnityEngine;

namespace EG.Tower.Battle.Data
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Data/Battle/Enemy", order = 0)]
    public class EnemyData : ScriptableObject
    {
        public string Name;
        public int MaxHP;
        public int Attack;
        public int Defence;
        public Sprite Icon;
    }
}
