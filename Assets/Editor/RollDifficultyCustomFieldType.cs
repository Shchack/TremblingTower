using EG.Tower.Rolls;
using System;
using UnityEditor;
using UnityEngine;

namespace PixelCrushers.DialogueSystem
{
    [CustomFieldTypeService.Name("RollDifficultyType")]
    public class RollDifficultyCustomFieldType : CustomFieldType
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

        private RollDifficultyType GetCurrentTraitType(string currentValue)
        {
            if (string.IsNullOrWhiteSpace(currentValue))
            {
                currentValue = RollDifficultyType.None.ToString();
            }

            try
            {
                return Enum.Parse<RollDifficultyType>(currentValue, true);
            }
            catch (Exception)
            {
                return RollDifficultyType.None;
            }
        }
    }
}
