using EG.Tower.Game.Battle.Models;
using TMPro;
using UnityEngine;

namespace EG.Tower.Game.Battle.UI
{
    public class BattleAttributeUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _attributeLabel;
        [SerializeField] private TMP_Text _attributeValueLabel;

        public void InitMainAttribute(BattleAttributeItemModel item)
        {
            _attributeLabel.text = $"{item.VirtueValue}% {item.VirtueName}";

            if (item.AttributeType == HeroAttributeType.Points)
            {
                _attributeValueLabel.text = $"{item.AttributeValue} {item.AttributeData.Name}";
            }
            else
            {
                _attributeValueLabel.text = $"{item.AttributeValue}% {item.AttributeData.Name}";
            }

        }

        public void InitAdditionalAttribute(string attributeName, float attributevalue)
        {
            _attributeLabel.text = attributeName;
            _attributeValueLabel.text = Mathf.RoundToInt(attributevalue).ToString();
        }
    }
}
