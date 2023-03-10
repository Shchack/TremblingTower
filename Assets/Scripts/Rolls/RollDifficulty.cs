using System;
using UnityEngine;

namespace EG.Tower.Rolls
{
    [Serializable]
    public class RollDifficulty
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public RollDifficultyType Type { get; private set; }
        [field: SerializeField] public int Value { get; private set; }
    }
}
