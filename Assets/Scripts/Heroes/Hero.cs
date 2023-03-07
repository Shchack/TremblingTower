using EG.Tower.Heroes.Skills;
using UnityEngine;

namespace EG.Tower.Game
{
    public class Hero : MonoBehaviour
    {
        private HeroModel _heroModel => GameHub.One.Session.HeroModel;

        public int GetSkillValue(string name)
        {
            return _heroModel.FindSkillValue(name);
        }

        public void AddSkillValue(string name, int value)
        {
            if (_heroModel.TryFindSkill(name, out Skill skill))
            {
                skill.AddValue(value);
                Debug.Log($"Skill {skill.Name} value is {skill.Value}");
            }
        }
    }
}