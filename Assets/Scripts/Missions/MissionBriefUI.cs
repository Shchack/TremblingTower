using EG.Tower.Heroes.Skills;
using EG.Tower.Rolls;
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

        public void Init(RegionType region, FactionType faction, SkillValueData[] skills)
        {
            _regionLabel.text = $"Region: {region}";
            _factionLabel.text = $"Faction: {faction}";
            InitMissionSkills(skills);
        }

        private void InitMissionSkills(SkillValueData[] skills)
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
    }
}
