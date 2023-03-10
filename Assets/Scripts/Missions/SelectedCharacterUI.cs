using EG.Tower.Game;
using EG.Tower.Heroes.Skills;
using EG.Tower.Heroes.Traits;
using EG.Tower.Rolls;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EG.Tower.Missions
{
    public class SelectedCharacterUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _heroNameLabel;
        [SerializeField] private RectTransform _heroInfoPanel;
        [SerializeField] private RectTransform _traitsHolder;
        [SerializeField] private TraitUI _traitUiPrefab;
        [SerializeField] private RectTransform _skillsHolder;
        [SerializeField] private MissionSkillUI _skillUiPrefab;
        [SerializeField] private RectTransform _rollHolder;
        [SerializeField] private Image[] _dicesUi;

        private void Awake()
        {
            _rollHolder.gameObject.SetActive(false);
            _heroInfoPanel.gameObject.SetActive(false);
        }

        public void Init(HeroModel hero)
        {
            _heroInfoPanel.gameObject.SetActive(true);
            _heroNameLabel.text = hero.Name;
            InitTraits(hero.Traits);
            InitSkills(hero.Skills);
        }

        private void InitTraits(TraitData[] traits)
        {
            var existingItems = _traitsHolder.GetComponentsInChildren<TraitUI>();

            for (int i = 0; i < existingItems.Length; i++)
            {
                Destroy(existingItems[i].gameObject);
            }

            foreach (var trait in traits)
            {
                var traitUi = Instantiate(_traitUiPrefab, _traitsHolder);
                traitUi.Init(trait);
            }
        }

        private void InitSkills(Skill[] skills)
        {
            var existingItems = _skillsHolder.GetComponentsInChildren<MissionSkillUI>();

            for (int i = 0; i < existingItems.Length; i++)
            {
                Destroy(existingItems[i].gameObject);
            }

            foreach (var skill in skills)
            {
                var skillUi = Instantiate(_skillUiPrefab, _skillsHolder);
                skillUi.Init(skill.AltName, skill.Value);
            }
        }

        public void ShowRoll(DicesRoll roll)
        {
            if (_dicesUi.Length != roll.Dices.Length)
            {
                Debug.LogError($"No dice visual for rolls!");
                return;
            }

            for (int i = 0; i < _dicesUi.Length; i++)
            {
                _dicesUi[i].sprite = roll.Dices[i].Icon;
            }

            _rollHolder.gameObject.SetActive(true);
        }

        public void HideRoll()
        {
            _rollHolder.gameObject.SetActive(false);
        }

        public void HideInfo()
        {
            _heroInfoPanel.gameObject.SetActive(false);
        }
    }
}
