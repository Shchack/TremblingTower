using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EG.Tower.Rolls
{
    [CreateAssetMenu(fileName = "RollDifficultiesData", menuName = "Data/Rolls/RollDifficulties", order = 0)]
    public class RollDifficultiesData : ScriptableObject
    {
        [field: SerializeField] public RollDifficulty[] Difficulties { get; private set; }

        private Dictionary<string, RollDifficulty> _difficultiesByType;
        private RollDifficulty _defaultDifficulty => Difficulties[0];

        private void OnEnable()
        {
            _difficultiesByType = Difficulties.ToDictionary(p => p.Type.ToString(), p => p);
        }

        public RollDifficulty FindDifficulty(string typeName)
        {
            RollDifficulty result = _defaultDifficulty;

            if (_difficultiesByType.TryGetValue(typeName, out RollDifficulty rollDifficulty))
            {
                result = rollDifficulty;
            }

            return result;
        }
    }
}
