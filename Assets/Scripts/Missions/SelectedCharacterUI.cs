﻿using EG.Tower.Game;
using EG.Tower.Heroes.Skills;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EG.Tower.Missions
{
    public class SelectedCharacterUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _heroNameLabel;
        [SerializeField] private TMP_Text _itemNameLabel;
        [SerializeField] private RectTransform _skillsHolder;
        [SerializeField] private MissionSkillUI _skillUiPrefab;
        [SerializeField] private RectTransform _rollHolder;
        [SerializeField] private Image[] _dicesUi;

        private void Awake()
        {
            _rollHolder.gameObject.SetActive(false);
        }

        public void Init(HeroModel hero)
        {
            _heroNameLabel.text = hero.Name;
            _itemNameLabel.text = "Student Revolver";
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

        public void ShowRoll(int[] dices)
        {
            if (_dicesUi.Length != dices.Length)
            {
                Debug.LogError($"No dice visual for rolls!");
                return;
            }

            for (int i = 0; i < _dicesUi.Length; i++)
            {
                if (dices[i] - 1 >= 0 && dices[i] - 1 <= 6)
                {
                    _dicesUi[i].sprite = GameHub.One.FindDiceSixSprite(dices[i]);
                }
                else
                {
                    Debug.LogError($"Wrong dice roll: {dices[i]}!");
                }
            }

            _rollHolder.gameObject.SetActive(true);
        }
    }
}
