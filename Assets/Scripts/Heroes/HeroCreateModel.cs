using System;
using System.Linq;

namespace EG.Tower.Game
{
    [Serializable]
    public class HeroCreateModel
    {
        public string Name { get; private set; }
        public Trait[] Traits { get; private set; }
        public Trait MainVirtueTrait { get; private set; }
        public Trait MainViceTrait { get; private set; }

        public HeroCreateModel(string defaultName, TraitsData traitsData)
        {
            Name = defaultName;
            MainVirtueTrait = null;
            MainViceTrait = null;
            Traits = traitsData.GetTraits();
        }

        public void SetName(string newName)
        {
            Name = newName;
        }

        public void BoostVirtue(string virtue, int virtueBoost)
        {
            if (MainVirtueTrait != null && MainVirtueTrait.HasVirtue(virtue))
            {
                return;
            }

            var newTrait = Traits.FirstOrDefault(t => t.HasVirtue(virtue));

            if (MainVirtueTrait != null)
            {
                MainVirtueTrait.DecreaseValue(virtueBoost);
            }

            newTrait.IncreaseValue(virtueBoost);
            MainVirtueTrait = newTrait;
        }

        public void BoostVice(string vice, int viceBoost)
        {
            if (MainViceTrait != null && MainViceTrait.HasVirtue(vice))
            {
                return;
            }

            var newTrait = Traits.FirstOrDefault(t => t.HasVice(vice));
            if (MainViceTrait != null)
            {
                MainViceTrait.IncreaseValue(viceBoost);
            }

            newTrait.DecreaseValue(viceBoost);
            MainViceTrait = newTrait;
        }

        public void ResetMainVirtue()
        {
            if (MainVirtueTrait != null)
            {
                MainVirtueTrait.ResetValue();
                MainVirtueTrait = null;
            }
        }

        public void ResetMainVice()
        {
            if (MainViceTrait != null)
            {
                MainViceTrait.ResetValue();
                MainViceTrait = null;
            }
        }
    }
}
