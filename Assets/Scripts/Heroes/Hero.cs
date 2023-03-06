using UnityEngine;

namespace EG.Tower.Game
{
    public class Hero : MonoBehaviour
    {
        private HeroModel _heroModel => GameHub.One.Session.HeroModel;

        public double GetVirtueValue(string virtueName)
        {
            return _heroModel.FindVirtueTraitValue(virtueName);
        }

        public void AddVirtueValue(string virtueName, double value)
        {
            if (_heroModel.TryFindVirtueTrait(virtueName, out Trait trait))
            {
                trait.AddValue(value);
                Debug.Log($"Trait {trait.Virtue} Value is {trait.Value}");
            }
        }
    }
}