using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace EG.Tower.Game
{
    public class DropDownExtended : TMP_Dropdown
    {
        [Tooltip("Indexes that should be ignored. Indexes are 0 based.")]
        public List<int> indexesToDisable = new List<int>();

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            var dropDownList = GetComponentInChildren<Canvas>();
            if (dropDownList == null)
            {
                return;
            }

            // If the dropdown was opened find the options toggles
            var toogles = dropDownList.GetComponentsInChildren<Toggle>(true);

            // the first item will always be a template item from the dropdown we have to ignore
            // so we start at one and all options indexes have to be 1 based
            for (var i = 1; i < toogles.Length; i++)
            {
                // disable buttons if their 0-based index is in indexesToDisable
                // the first item will always be a template item from the dropdown
                // so in order to still have 0 based indexes for the options here we use i-1
                toogles[i].interactable = !indexesToDisable.Contains(i - 1);
            }
        }

        // Anytime change a value by index
        public void SetOptionEnabled(int index, bool enabled)
        {
            if (index < 1 || index > options.Count)
            {
                Debug.LogWarning("Index out of range -> ignored!", this);
                return;
            }

            if (enabled)
            {
                // remove index from disabled list
                if (indexesToDisable.Contains(index)) indexesToDisable.Remove(index);
            }
            else
            {
                // add index to disabled list
                if (!indexesToDisable.Contains(index)) indexesToDisable.Add(index);
            }

            var dropDownList = GetComponentInChildren<Canvas>();

            // If this returns null than the Dropdown was closed
            if (!dropDownList) return;

            // If the dropdown was opened find the options toggles
            var toogles = dropDownList.GetComponentsInChildren<Toggle>(true);
            toogles[index].interactable = enabled;
        }

        public void EnableAll()
        {
            indexesToDisable.Clear();
        }

        public string GetText(int index)
        {
            return options[index].text;
        }

        // Anytime change a value by string label
        public void EnableOption(string label, bool enabled)
        {
            var index = options.FindIndex(o => string.Equals(o.text, label));

            // We need a 1-based index
            SetOptionEnabled(index + 1, enabled);
        }
    }
}
