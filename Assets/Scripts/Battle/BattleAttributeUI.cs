using TMPro;
using UnityEngine;

namespace EG.Tower.Game.Battle
{
    public class BattleAttributeUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _attributeLabel;
        [SerializeField] private TMP_Text _attributeValueLabel;

        public void InitMainAttribute(BattleAttributeItemModel item)
        {
            _attributeLabel.text = $"{item.VirtueValue}% {item.VirtueName}";

            if (item.AttributeType == HeroAttributeType.PercentAsIs)
            {
                _attributeValueLabel.text = $"{item.AttributeValue}% {item.AttributeName}";
            }
            else if (item.AttributeType == HeroAttributeType.Points)
            {
                _attributeValueLabel.text = $"+{item.AttributeValue} {item.AttributeName}";
            }

        }

        public void InitAdditionalAttribute(string combatOrderAttributeName, int combatOrder)
        {
            _attributeLabel.text = combatOrderAttributeName;
            _attributeValueLabel.text = combatOrder.ToString();
        }
    }
}
