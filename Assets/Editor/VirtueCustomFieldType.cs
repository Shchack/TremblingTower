using EG.Tower.Game;
using System;
using UnityEditor;
using UnityEngine;

namespace PixelCrushers.DialogueSystem
{
    [CustomFieldTypeService.Name("VirtueType")]
    public class VirtueCustomFieldType : CustomFieldType
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

        private VirtueType GetCurrentTraitType(string currentValue)
        {
            if (string.IsNullOrWhiteSpace(currentValue))
            {
                currentValue = VirtueType.None.ToString();
            }

            try
            {
                return Enum.Parse<VirtueType>(currentValue, true);
            }
            catch (Exception)
            {
                return VirtueType.None;
            }
        }
    }
}
