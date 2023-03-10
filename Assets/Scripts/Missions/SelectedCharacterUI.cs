using EG.Tower.Game;
using EG.Tower.Heroes.Skills;
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
            InitSkills(hero.Skills);
        }

        private void InitSkills(Skill[] skills)
        {
            var existingSkills = _skillsHolder.GetComponentsInChildren<MissionSkillUI>();

            for (int i = 0; i < existingSkills.Length; i++)
            {
                Destroy(existingSkills[i].gameObject);
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
