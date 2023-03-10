using EG.Tower.Heroes.Skills;
using System;
using UnityEditor;
using UnityEngine;

namespace PixelCrushers.DialogueSystem
{
    [CustomFieldTypeService.Name("SkillType")]
    public class SkillCustomFieldType : CustomFieldType
    {
        public override string Draw(string currentValue, DialogueDatabase database)
        {
            var enumValue = GetCurrentTraitType(currentValue);
            return EditorGUILayout.EnumPopup(enumValue).ToString();
        }

        public override string Draw(Rect rect, string currentValue, DialogueDatabase database)
        {
            var enumValue = GetCurrentTraitType(currentValue);
            return EditorGUI.EnumPopup(rect, enumValue).ToString();
        }

        private SkillType GetCurrentTraitType(string currentValue)
        {
            if (string.IsNullOrWhiteSpace(currentValue))
            {
                currentValue = SkillType.None.ToString();
            }

            try
            {
                return Enum.Parse<SkillType>(currentValue, true);
            }
            catch (Exception)
            {
                return SkillType.None;
            }
        }
    }
}
