using System;
using UnityEngine;

namespace EG.Tower.Game
{
    public class HeroCreateController : MonoBehaviour
    {
        [SerializeField] private TraitsData _traitsData;
        [SerializeField] private string _defaultName = "Hero";

        public TraitsData TraitsData => _traitsData;

        public Trait[] HeroTraits => _heroModel.Traits;

        private HeroCreateModel _heroModel;

        private void Awake()
        {
            Validate();
            _heroModel = new HeroCreateModel(_defaultName, _traitsData.Traits);
        }

        private void Validate()
        {
            if (_traitsData == null)
            {
                Debug.LogError("Traits data not set!", this);
            }
        }

        public void SetHeroName(string newName)
        {
            _heroModel.SetName(newName);
        }

        public void SetMainVirtue(string virtue)
        {
            _heroModel.BoostVirtue(virtue, _traitsData.MainVirtueBoost);
        }

        public void SetMainVice(string vice)
        {
            _heroModel.BoostVice(vice, _traitsData.MainViceBoost);
        }

        public void ResetMainVirtue()
        {
            _heroModel.ResetMainVirtue();
        }

        internal void ResetMainVice()
        {
            _heroModel.ResetMainVice();
        }

        internal void CreateHero()
        {
            Debug.Log("Hero created!");
        }
    }
}
