using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EG.Tower.Missions
{
    public class MissionStepUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _stepNameLabel;
        [SerializeField] private Image _resultImage;
        [SerializeField] private Color _succesColor;
        [SerializeField] private Color _failColor;

        private MissionStep _missionStep;

        private void Start()
        {
            _resultImage.enabled = false;
        }

        public void Init(MissionStep step)
        {
            _missionStep = step;
            _missionStep.OnCompletedEvent += HandleStepCompletedEvent;
            _stepNameLabel.text = step.Name;
        }

        private void HandleStepCompletedEvent(StepCompletionInfo info)
        {
            _resultImage.enabled = true;
            _resultImage.color = info.Success ? _succesColor : _failColor;
        }

        private void OnDestroy()
        {
            if (_missionStep != null)
            {
                _missionStep.OnCompletedEvent -= HandleStepCompletedEvent;
            }
        }
    }
}