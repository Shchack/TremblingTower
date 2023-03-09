using UnityEngine;

namespace EG.Tower.Missions
{
    public class MissionProgressUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _missionStepsHolder;
        [SerializeField] private MissionStepUI _missionStepUiPrefab;

        public void Init(MissionStep[] steps)
        {
            var existingSteps = _missionStepsHolder.GetComponentsInChildren<MissionStepUI>();

            for (int i = 0; i < existingSteps.Length; i++)
            {
                Destroy(existingSteps[i].gameObject);
            }

            foreach (var step in steps)
            {
                var stepUi = Instantiate(_missionStepUiPrefab, _missionStepsHolder);
                stepUi.Init(step);
            }
        }
    }
}
