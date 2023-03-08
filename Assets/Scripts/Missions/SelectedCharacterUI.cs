using EG.Tower.Game;
using EG.Tower.Heroes.Skills;
using TMPro;
using UnityEngine;

namespace EG.Tower.Missions
{
    public class SelectedCharacterUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _heroNameLabel;
        [SerializeField] private TMP_Text _itemNameLabel;
        [SerializeField] private RectTransform _skillsHolder;
        [SerializeField] private MissionSkillUI _skillUiPrefab;

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
    }
}
