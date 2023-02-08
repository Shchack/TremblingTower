using EG.Tower.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EG.Tower.Game.Rolls
{
    [CreateAssetMenu(fileName = "RollProbabilitiesData", menuName = "Data/Rolls/RollProbabilities", order = 0)]
    public class RollProbabilitiesData : ScriptableObject
    {
        [field: SerializeField] public RollProbability[] Probabilities { get; private set; }

        public Dictionary<string, RollProbability> AsDictionary()
        {
            return Probabilities.ToDictionary(p => p.Text, p => p);
        }

        public string FindText(double checkValue)
        {
            bool found = false;
            var result = string.Empty;
            var index = 0;

            if (Probabilities != null && Probabilities.Length > 0)
            {
                while (!found && index < Probabilities.Length)
                {
                    if (checkValue >= Probabilities[index].ChanceRange.Min && checkValue <= Probabilities[index].ChanceRange.Max)
                    {
                        found = true;
                        result = Probabilities[index].Text;
                    }
                    index++;
                }
            }

            return result;
        }
    }

    [Serializable]
    public class RollProbability
    {
        [field: SerializeField] public string Text { get; private set; }
        [field: SerializeField] public FloatRange ChanceRange { get; private set; }
    }
}
