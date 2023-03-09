using EG.Tower.Game;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EG.Tower.Missions
{
    public class MissionBriefUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _regionLabel;
        [SerializeField] private TMP_Text _factionLabel;
        [SerializeField] private RectTransform _missionSkillsHolder;
        [SerializeField] private MissionSkillUI _missionSkillUiPrefab;
        [SerializeField] private RectTransform _rollHolder;
        [SerializeField] private Image[] _dicesUi;

        private void Awake()
        {
            _rollHolder.gameObject.SetActive(false);
        }

        public void Init(MissionRegionType region, FactionType faction, MissionSkillData[] skills)
        {
            _regionLabel.text = $"Region: {region}";
            _factionLabel.text = $"Faction: {faction}";
            InitMissionSkills(skills);
        }

        private void InitMissionSkills(MissionSkillData[] skills)
        {
            var existingSkills = _missionSkillsHolder.GetComponentsInChildren<MissionSkillUI>();

            for (int i = 0; i < existingSkills.Length; i++)
            {
                Destroy(existingSkills[i].gameObject);
            }

            foreach (var skill in skills)
            {
                var skillUi = Instantiate(_missionSkillUiPrefab, _missionSkillsHolder);
                skillUi.Init(skill.Skill.AltName, skill.Value);
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
