using EG.Tower.Heroes;
using EG.Tower.Heroes.Skills;
using EG.Tower.Utils;
using UnityEngine;

namespace EG.Tower.Game
{
    public class HeroCreateController : MonoBehaviour
    {
        [SerializeField] private HeroSetupData _setupData;

        public Skill[] Skills => _heroCreateModel.Skills;

        public string HeroName => _heroCreateModel.Name;

        private HeroCreateModel _heroCreateModel;

        private void Awake()
        {
            Validate();
            _heroCreateModel = new HeroCreateModel(_setupData);
        }

        private void Validate()
        {
            if (_setupData == null)
            {
                Debug.LogError("Traits data not set!", this);
            }
        }

        public void SetHeroName(string newName)
        {
            _heroCreateModel.SetName(newName);
        }

        public void SetStrengthSkill(string name)
        {
            _heroCreateModel.SetStrengthSkill(name);
        }

        public void SetWeaknessSkill(string name)
        {
            _heroCreateModel.SetWeaknessSkill(name);
        }

        public void ResetMainVirtue()
        {
            _heroCreateModel.ResetStrengthSkill();
        }

        public void ResetMainVice()
        {
            _heroCreateModel.ResetWeaknessSkill();
        }

        public void CreateHero()
        {
            Debug.Log("Hero created!");
            var heroModel = new HeroModel(_heroCreateModel);
            GameHub.One.Session.SetHeroModel(heroModel);
            SceneHelper.LoadDialogueScene();
        }
    }
}
