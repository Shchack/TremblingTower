using EG.Tower.Utils;
using UnityEngine;

namespace EG.Tower.Game
{
    public class HeroCreateController : MonoBehaviour
    {
        [SerializeField] private HeroSetupData _traitsData;
        [SerializeField] private string _defaultName = "Hero";

        public HeroSetupData TraitsData => _traitsData;

        public Trait[] HeroTraits => _heroCreateModel.Traits;

        private HeroCreateModel _heroCreateModel;

        private void Awake()
        {
            Validate();
            _heroCreateModel = new HeroCreateModel(_defaultName, _traitsData);
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
            _heroCreateModel.SetName(newName);
        }

        public void SetMainVirtue(string virtue)
        {
            _heroCreateModel.BoostVirtue(virtue, _traitsData.MainVirtueBoost);
        }

        public void SetMainVice(string vice)
        {
            _heroCreateModel.BoostVice(vice, _traitsData.MainViceBoost);
        }

        public void ResetMainVirtue()
        {
            _heroCreateModel.ResetMainVirtue();
        }

        public void ResetMainVice()
        {
            _heroCreateModel.ResetMainVice();
        }

        public void CreateHero()
        {
            Debug.Log("Hero created!");
            var heroModel = new HeroModel(_heroCreateModel);
            GameHub.One.Session.SetHeroModel(heroModel);
            SceneHelper.LoadGameplayScene();
        }
    }
}
