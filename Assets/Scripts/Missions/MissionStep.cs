using System;

namespace EG.Tower.Missions
{
    public class MissionStep
    {
        public event Action<bool> OnStepCompletedEvent;

        public string Name { get; private set; }
        public SkillCheckData[] PossibleSkillChecks { get; private set; }

        public MissionStep(MissionStepData data)
        {
            Name = data.Name;
            PossibleSkillChecks = data.PossibleSkillChecks;
        }

        public void CompleteStep(bool isSuccess)
        {
            OnStepCompletedEvent?.Invoke(isSuccess);
        }
    }
}