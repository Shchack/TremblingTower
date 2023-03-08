using TMPro;
using UnityEngine;

namespace EG.Tower.Missions
{
    public class MissionStepUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _stepNameLabel;

        public void Init(MissionStep step)
        {
            _stepNameLabel.text = step.Name;
        }
    }
}