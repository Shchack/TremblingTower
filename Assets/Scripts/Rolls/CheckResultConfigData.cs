using UnityEngine;

namespace EG.Tower.Dialogues.Data
{
    [CreateAssetMenu(fileName = "CheckResultConfigData", menuName = "Data/Rolls/CheckResultConfig", order = 1)]
    public class CheckResultConfigData : ScriptableObject
    {
        [Header("Params")]
        public Color SuccessColor;
        public Color FailColor;
        public string SuccessText = "Check Success";
        public string FailText = "Check Failure";

        [Header("Tween")]
        public LeanTweenType TweenType = LeanTweenType.linear;
        public float TweenInSeconds = 1f;
        public float TweenOutSeconds = 1f;
        public float TweenStaySeconds = 2f;

        public float Duration => TweenInSeconds + TweenOutSeconds + TweenStaySeconds;
    }
}
